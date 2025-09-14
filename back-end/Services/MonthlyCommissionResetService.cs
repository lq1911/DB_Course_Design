using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BackEnd.Data; // 引入 AppDbContext
using Microsoft.EntityFrameworkCore; // 引入 EF Core 的扩展方法
using Microsoft.Extensions.DependencyInjection; // 引入 IServiceScopeFactory
using Microsoft.Extensions.Hosting; // 引入 IHostedService
using Microsoft.Extensions.Logging; // 引入日志记录

namespace BackEnd.Services
{
    public class MonthlyCommissionResetService : IHostedService, IDisposable
    {
        private readonly ILogger<MonthlyCommissionResetService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer? _timer;

        public MonthlyCommissionResetService(ILogger<MonthlyCommissionResetService> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("月度提成重置后台服务已启动。");
            // 启动时，立即调度第一次任务
            ScheduleNextRun();
            return Task.CompletedTask;
        }

        private void ScheduleNextRun()
        {
            var now = DateTime.UtcNow;
            // 计算下一个月1号的凌晨0点5分 (UTC时间)
            var firstDayOfNextMonth = new DateTime(now.Year, now.Month, 1, 0, 5, 0, DateTimeKind.Utc).AddMonths(1);
            var initialDelay = firstDayOfNextMonth - now;

            if (initialDelay.TotalMilliseconds <= 0)
            {
                // 如果计算出的时间已经过去（例如，在月初启动服务），则计算再下一个月的
                firstDayOfNextMonth = firstDayOfNextMonth.AddMonths(1);
                initialDelay = firstDayOfNextMonth - now;
            }

            _logger.LogInformation("下一次月度提成重置任务将在 {resetTime} (UTC) 执行。", firstDayOfNextMonth);

            // 设置定时器：在指定的延迟后执行一次 DoWork
            _timer = new Timer(DoWork, null, initialDelay, Timeout.InfiniteTimeSpan);
        }

        private async void DoWork(object? state)
        {
            _logger.LogInformation("正在执行月度提成重置任务...");
            try
            {
                // IHostedService 是单例的，但 DbContext 是 Scoped 的。
                // 必须创建一个新的作用域来安全地获取 DbContext 实例。
                using (var scope = _scopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                    // 使用 EF Core 7+ 的 ExecuteUpdateAsync 进行高效的批量更新
                    // 这会生成一条单一的 SQL UPDATE 语句，无需查询数据到内存。
                    var rowsAffected = await dbContext.Couriers
                                 .ExecuteUpdateAsync(s => s.SetProperty(c => c.CommissionThisMonth, 0.00m));

                    _logger.LogInformation("所有骑手的本月提成已成功重置为 0。受影响行数: {rows}", rowsAffected);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "执行月度提成重置任务时发生严重错误。");
            }
            finally
            {
                // 无论成功还是失败，都重新调度下一次（再下一个月）的任务
                ScheduleNextRun();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("月度提成重置后台服务正在停止。");
            _timer?.Change(Timeout.Infinite, 0); // 停止定时器
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
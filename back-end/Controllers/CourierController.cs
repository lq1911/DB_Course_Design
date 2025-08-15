using Microsoft.AspNetCore.Mvc;
using BackEnd.Services.Interfaces;
using BackEnd.DTOs.Courier; // 已更新为新的 DTO 路径
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize(Roles = "Courier")] // 将来启用认证后，可以取消此行注释
    public class CourierController : ControllerBase
    {
        private readonly ICourierService _courierService;

        // 构造函数，用于注入骑手相关的业务服务层
        public CourierController(ICourierService courierService)
        {
            _courierService = courierService;
        }

        /// <summary>
        /// 获取当前登录骑手的个人资料
        /// </summary>
        /// <returns>包含骑手资料的响应</returns>
        [HttpGet("profile")] // 对应路由: GET /api/courier/profile
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                // 1. 调用辅助方法，从认证 Token 中安全地获取当前骑手的ID
                var courierId = GetCurrentCourierId();

                // 2. 将骑手ID传递给业务服务层，调用相应的业务逻辑方法
                var profileDto = await _courierService.GetProfileAsync(courierId);

                // 3. 检查服务层的返回结果
                if (profileDto == null)
                {
                    // 如果返回 null，说明在数据库中没有找到该骑手的资料，
                    // 按照 HTTP 规范，返回 404 Not Found 状态码。
                    return NotFound(new { code = 404, message = "骑手资料未找到" });
                }

                // 4. 如果成功获取到数据，返回 200 OK 状态码，
                //    并将数据封装在前端需要的格式中。
                return Ok(new { code = 200, message = "获取成功", data = profileDto });
            }
            catch (UnauthorizedAccessException ex)
            {
                // 如果 GetCurrentCourierId() 因 Token 无效或缺失而抛出异常，
                // 捕获它并返回 401 Unauthorized 状态码。
                return Unauthorized(new { code = 401, message = ex.Message });
            }
            catch (Exception ex)
            {
                // 捕获所有其他在服务层或仓储层可能发生的未知异常，
                // 返回 500 Internal Server Error 状态码，防止敏感信息泄露给前端。
                // 在生产环境中，应该在这里记录详细的错误日志。
                return StatusCode(500, new { code = 500, message = $"服务器内部错误: {ex.Message}" });
            }
        }

        /// <summary>
        /// 获取当前登录骑手的工作状态和今日绩效
        /// </summary>
        [HttpGet("status")] // 对应路由: GET /api/courier/status
        public async Task<IActionResult> GetWorkStatus()
        {
            try
            {
                var courierId = GetCurrentCourierId(); // 复用辅助方法获取ID

                var statusDto = await _courierService.GetWorkStatusAsync(courierId);

                if (statusDto == null)
                {
                    return NotFound(new { code = 404, message = "骑手资料未找到，无法获取状态" });
                }

                return Ok(new { code = 200, message = "获取成功", data = statusDto });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { code = 401, message = ex.Message });
            }
            catch (Exception ex)
            {
                // 记录日志...
                return StatusCode(500, new { code = 500, message = $"服务器内部错误: {ex.Message}" });
            }
        }


        /// <summary>
        /// 获取当前登录骑手的订单列表，可根据状态筛选
        /// </summary>
        /// <param name="status">订单状态 (pending, delivering, completed)</param>
        [HttpGet("orders")] // 对应路由: GET /api/courier/orders?status=...
        public async Task<IActionResult> GetOrders([FromQuery] string status)
        {
            // 增加一个简单的参数验证
            if (string.IsNullOrWhiteSpace(status))
            {
                return BadRequest(new { code = 400, message = "必须提供 status 参数" });
            }

            try
            {
                var courierId = GetCurrentCourierId();
                var orders = await _courierService.GetOrdersAsync(courierId, status);
                return Ok(new { code = 200, message = "获取成功", data = orders });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { code = 401, message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { code = 500, message = $"服务器内部错误: {ex.Message}" });
            }
        }








        #region 辅助方法 (Helper Methods)

        // 这个私有方法用于从认证信息中获取当前登录用户的ID
        // 你的所有Action方法都可以调用它来确定操作对象是谁
        private int GetCurrentCourierId()
        {
            // HttpContext.User 是一个 ClaimsPrincipal 对象，包含了通过认证（如JWT Token）解析出的用户信息
            // 我们在这里查找类型为 ClaimTypes.NameIdentifier 的 Claim。
            // ClaimTypes.NameIdentifier 是 .NET 中用于表示用户唯一标识符的标准 Claim 类型。
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }

            // 如果出于任何原因无法获取ID（比如Token无效或未提供），
            // 抛出一个异常是比较规范的做法。
            // 在生产环境中，这通常会被全局异常处理中间件捕获，并返回一个 401 Unauthorized 响应。
            throw new UnauthorizedAccessException("无法从认证信息中解析有效的用户ID。");
        }

        #endregion
    }
}
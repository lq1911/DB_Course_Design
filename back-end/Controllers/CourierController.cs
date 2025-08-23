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
        [HttpGet("profile")]
        public async Task<ActionResult<CourierProfileDto>> GetProfile() // 返回类型可以写得更具体
        {
            try
            {
                var courierId = GetCurrentCourierId();
                var profileDto = await _courierService.GetProfileAsync(courierId);

                if (profileDto == null)
                {
                    // 对于 API，返回一个标准的 Problem Details 或简单的错误对象更好
                    return NotFound("骑手资料未找到");
                }

                // 新的返回方式：直接返回 DTO 对象！
                // ASP.NET Core 会自动将其序列化为 JSON 并设置 200 OK 状态码
                return Ok(profileDto);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(); // 返回标准的 401
            }
            catch (Exception ex)
            {
                // 在开发环境中可以返回错误信息，生产环境应记录日志并返回通用错误
                return StatusCode(500, $"服务器内部错误: {ex.Message}");
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


        /// <summary>
        /// 获取当前登录骑手的实时位置文本（模拟版）
        /// </summary>
        [HttpGet("location")] // 对应路由: GET /api/courier/location
        public async Task<IActionResult> GetLocation()
        {
            try
            {
                var courierId = GetCurrentCourierId(); // 获取当前骑手ID
                var area = await _courierService.GetCurrentLocationAsync(courierId); // 调用服务

                // 使用匿名对象构造返回给前端的 JSON 结构
                return Ok(new { code = 200, message = "获取成功", data = new { area = area } });
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


        /// <summary>
        /// 切换当前登录骑手的工作状态（开工/收工）
        /// </summary>
        [HttpPost("status/toggle")] // 对应路由: POST /api/courier/status/toggle
        public async Task<IActionResult> ToggleStatus([FromBody] ToggleStatusRequestDto request)
        {
            try
            {
                var courierId = GetCurrentCourierId();

                // 注意：我们从可空的 DTO 属性中获取值
                var success = await _courierService.ToggleWorkStatusAsync(courierId, request.IsOnline.Value);

                if (!success)
                {
                    // 如果 service 返回 false，意味着骑手不存在
                    return NotFound(new { code = 404, message = "骑手不存在，无法更新状态" });
                }

                // 根据新需求，返回一个包含 success 字段的 JSON 对象
                return Ok(new { code = 200, message = "状态更新成功", data = new { success = true } });
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



        /// <summary>
        /// 根据通知ID获取新订单的详细信息
        /// </summary>
        /// <param name="notificationId">新订单的推送唯一ID</param>
        [HttpGet("orders/new/{notificationId}")] // 对应路由: GET /api/courier/orders/new/123
        public async Task<IActionResult> GetNewOrderDetails(string notificationId)
        {
            // 将 string 类型的路由参数安全地转换为 int
            if (!int.TryParse(notificationId, out int taskId))
            {
                return BadRequest("无效的订单ID格式，必须是数字。");
            }

            try
            {
                var orderDetails = await _courierService.GetNewOrderDetailsAsync(taskId);

                if (orderDetails == null)
                {
                    return NotFound($"找不到ID为 {taskId} 的订单详情。");
                }

                // 直接返回详情 DTO 对象
                return Ok(orderDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"服务器内部错误: {ex.Message}");
            }
        }


        /// <summary>
        /// 接受一个新订单任务
        /// </summary>
        /// <param name="orderId">要接受的订单ID</param>
        [HttpPost("orders/{orderId}/accept")] // 对应路由: POST /api/courier/orders/101/accept
        public async Task<IActionResult> AcceptOrder(string orderId)
        {
            if (!int.TryParse(orderId, out int taskId))
            {
                return BadRequest("无效的订单ID格式。");
            }

            try
            {
                var courierId = GetCurrentCourierId();
                var success = await _courierService.AcceptOrderAsync(courierId, taskId);

                if (!success)
                {
                    // 返回 400 Bad Request 表示这是一个客户端错误（比如尝试接受一个不存在或状态不正确的订单）
                    return BadRequest(new { success = false, message = "无法接受该订单，它可能已被处理或不存在。" });
                }

                return Ok(new { success = true }); // 成功时直接返回 success: true
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"服务器内部错误: {ex.Message}");
            }
        }


        /// <summary>
        /// 拒绝一个新订单任务
        /// </summary>
        /// <param name="orderId">要拒绝的订单ID</param>
        [HttpPost("orders/{orderId}/reject")] // 对应路由: POST /api/courier/orders/101/reject
        public async Task<IActionResult> RejectOrder(string orderId)
        {
            if (!int.TryParse(orderId, out int taskId))
            {
                return BadRequest("无效的订单ID格式。");
            }

            try
            {
                // 注意：这里我们没有使用 GetCurrentCourierId()，因为拒绝操作可能不与特定骑手绑定
                var success = await _courierService.RejectOrderAsync(taskId);

                if (!success)
                {
                    return BadRequest(new { success = false, message = "无法拒绝该订单，它可能已被处理或不存在。" });
                }

                return Ok(new { success = true }); // 成功时直接返回 success: true
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"服务器内部错误: {ex.Message}");
            }
        }



        /// <summary>
        /// 获取当前登录骑手的当月收入（模拟数据）
        /// </summary>
        [HttpGet("income/monthly")] // 对应路由: GET /api/courier/income/monthly
        public async Task<IActionResult> GetMonthlyIncome()
        {
            try
            {
                var courierId = GetCurrentCourierId();
                var monthlyIncome = await _courierService.GetMonthlyIncomeAsync(courierId);

                // 核心：直接返回纯数字
                // Content() 方法允许我们返回一个自定义内容和 Content-Type 的响应。
                // 我们将 decimal 转换为字符串，并告诉浏览器这是一个纯文本。
                return Content(monthlyIncome.ToString(), "text/plain");
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"服务器内部错误: {ex.Message}");
            }
        }
    }

    
    




    
}
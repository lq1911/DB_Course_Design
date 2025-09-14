using BackEnd.Dtos.Courier;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourierController : ControllerBase
    {
        private readonly ICourierService _courierService;

        public CourierController(ICourierService courierService)
        {
            _courierService = courierService;
        }

        [HttpGet("profile")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<ActionResult<CourierProfileDto>> GetProfile()
        {
            try
            {
                var courierId = GetCurrentCourierId();
                var profileDto = await _courierService.GetProfileAsync(courierId);
                if (profileDto == null) return NotFound("骑手资料未找到");
                return Ok(profileDto);
            }
            catch (UnauthorizedAccessException) { return Unauthorized(); }
            catch (Exception ex) { return StatusCode(500, $"服务器内部错误: {ex.Message}"); }
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetWorkStatus()
        {
            try
            {
                var courierId = GetCurrentCourierId();
                var statusDto = await _courierService.GetWorkStatusAsync(courierId);
                if (statusDto == null) return NotFound("骑手资料未找到，无法获取状态");
                // 【已修正】直接返回只包含 isOnline 的对象
                return Ok(new { isOnline = statusDto.IsOnline });
            }
            catch (UnauthorizedAccessException) { return Unauthorized(); }
            catch (Exception ex) { return StatusCode(500, $"服务器内部错误: {ex.Message}"); }
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetOrders([FromQuery] string status)
        {
            if (string.IsNullOrWhiteSpace(status))
            {
                return BadRequest("必须提供 status 查询参数");
            }
            try
            {
                var courierId = GetCurrentCourierId();
                var orders = await _courierService.GetOrdersAsync(courierId, status);
                // 【已修正】直接返回订单数组
                return Ok(orders);
            }
            catch (UnauthorizedAccessException) { return Unauthorized(); }
            catch (Exception ex) { return StatusCode(500, $"服务器内部错误: {ex.Message}"); }
        }


        // CourierController.cs

        /// <summary>
        /// 获取当前登录骑手的实时位置文本（模拟版）
        /// </summary>
        [HttpGet("location")]
        public async Task<IActionResult> GetLocation()
        {
            try
            {
                var courierId = GetCurrentCourierId();
                var area = await _courierService.GetCurrentLocationAsync(courierId);

                // 恢复为返回包含 area 字段的匿名对象
                // 注意：这里我们保留了不带 {code, message} 的新风格
                return Ok(new { area = area });
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
        [HttpPost("status/toggle")]
        public async Task<IActionResult> ToggleStatus([FromBody] ToggleStatusRequestDto request)
        {
            try
            {
                var courierId = GetCurrentCourierId();
                var success = await _courierService.ToggleWorkStatusAsync(courierId, request.IsOnline);
                if (!success) return NotFound("骑手不存在，无法更新状态");
                // 【已修正】直接返回只包含 success 的对象
                return Ok(new { success = true });
            }
            catch (UnauthorizedAccessException) { return Unauthorized(); }
            catch (Exception ex) { return StatusCode(500, $"服务器内部错误: {ex.Message}"); }
        }

        [HttpGet("orders/new/{notificationId}")]
        public async Task<IActionResult> GetNewOrderDetails(string notificationId)
        {
            if (!int.TryParse(notificationId, out int taskId))
            {
                return BadRequest("无效的订单ID格式，必须是数字。");
            }
            try
            {
                var orderDetails = await _courierService.GetNewOrderDetailsAsync(taskId);
                if (orderDetails == null) return NotFound($"找不到ID为 {taskId} 的订单详情。");
                return Ok(orderDetails);
            }
            catch (Exception ex) { return StatusCode(500, $"服务器内部错误: {ex.Message}"); }
        }

        [HttpPost("orders/{orderId}/accept")]
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
                if (!success) return BadRequest(new { success = false, message = "无法接受该订单，它可能已被处理或不存在。" });
                return Ok(new { success = true });
            }
            catch (UnauthorizedAccessException) { return Unauthorized(); }
            catch (Exception ex) { return StatusCode(500, $"服务器内部错误: {ex.Message}"); }
        }

        [HttpPost("orders/{orderId}/reject")]
        public async Task<IActionResult> RejectOrder(string orderId)
        {
            if (!int.TryParse(orderId, out int taskId))
            {
                return BadRequest("无效的订单ID格式。");
            }
            try
            {
                var success = await _courierService.RejectOrderAsync(taskId);
                if (!success) return BadRequest(new { success = false, message = "无法拒绝该订单，它可能已被处理或不存在。" });
                return Ok(new { success = true });
            }
            catch (Exception ex) { return StatusCode(500, $"服务器内部错误: {ex.Message}"); }
        }

        [HttpGet("income/monthly")]
        public async Task<IActionResult> GetMonthlyIncome()
        {
            try
            {
                var courierId = GetCurrentCourierId();
                var monthlyIncome = await _courierService.GetMonthlyIncomeAsync(courierId);
                return Content(monthlyIncome.ToString("F2"), "text/plain");
            }
            catch (UnauthorizedAccessException) { return Unauthorized(); }
            catch (Exception ex) { return StatusCode(500, $"服务器内部错误: {ex.Message}"); }
        }

        #region 辅助方法 (Helper Methods)
        private int GetCurrentCourierId()
        {
            var userIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                return userId;
            }
            throw new UnauthorizedAccessException("无法从认证信息中解析有效的用户ID。");
        }
        #endregion


        /// <summary>
        /// 将指定的配送任务标记为已完成
        /// </summary>
        /// <param name="taskId">要完成的任务ID</param>
        [HttpPost("tasks/{taskId}/complete")] // 对应路由: POST /api/courier/tasks/102/complete
        public async Task<IActionResult> CompleteTask(int taskId)
        {
            try
            {
                // 从 Token 中获取当前操作的骑手ID
                var courierId = GetCurrentCourierId();

                // 调用 Service 层的业务逻辑
                await _courierService.MarkTaskAsCompletedAsync(taskId, courierId);

                // 操作成功，返回一个标准的成功响应
                return Ok(new { success = true, message = "订单已成功标记为完成" });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                // 如果 Service 层抛出异常，在这里捕获并返回 500 错误
                return StatusCode(500, $"操作失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 骑手确认已取货
        /// </summary>
        [HttpPost("orders/{orderId}/pickup")]
        public async Task<IActionResult> PickupOrder(string orderId)
        {
            if (!int.TryParse(orderId, out int taskId))
            {
                return BadRequest("无效的订单ID格式。");
            }

            try
            {
                var courierId = GetCurrentCourierId();
                var success = await _courierService.PickupOrderAsync(taskId, courierId);

                if (!success)
                {
                    return BadRequest(new { success = false, message = "操作失败，请检查订单状态或权限。" });
                }
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"服务器内部错误: {ex.Message}");
            }
        }

        /// <summary>
        /// 骑手确认已送达
        /// </summary>
        [HttpPost("orders/{orderId}/deliver")]
        public async Task<IActionResult> DeliverOrder(string orderId)
        {
            if (!int.TryParse(orderId, out int taskId))
            {
                return BadRequest("无效的订单ID格式。");
            }

            try
            {
                var courierId = GetCurrentCourierId();
                var success = await _courierService.DeliverOrderAsync(taskId, courierId);

                if (!success)
                {
                    return BadRequest(new { success = false, message = "操作失败，请检查订单状态或权限。" });
                }
                return Ok(new { success = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"服务器内部错误: {ex.Message}");
            }
        }


        [HttpGet("orders/available")]
        public async Task<ActionResult<IEnumerable<AvailableOrderDto>>> GetAvailableOrders(
            [FromQuery] decimal latitude,
            [FromQuery] decimal longitude,
            [FromQuery] decimal maxDistance = 100000.0m)
        {
            try
            {
                var courierId = GetCurrentCourierId();
                var availableOrders = await _courierService.GetAvailableOrdersAsync(courierId, latitude, longitude, maxDistance);
                return Ok(availableOrders);
            }
            catch (UnauthorizedAccessException) { return Unauthorized(); }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
            catch (Exception ex) { return StatusCode(500, $"服务器内部错误: {ex.Message}"); }
        }


        [HttpGet("complaints")] // 对应前端 API: GET /api/courier/complaints
        public async Task<ActionResult<IEnumerable<ComplaintDto>>> GetComplaints()
        {
            try
            {
                var courierId = GetCurrentCourierId();
                var complaints = await _courierService.GetComplaintsAsync(courierId);
                return Ok(complaints);
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


        [HttpPost("location/update")] // 定义路由为 POST /api/courier/location/update
        public async Task<IActionResult> UpdateLocation([FromBody] UpdateLocationDto locationDto)
        {
            try
            {
                var courierId = GetCurrentCourierId();
                var success = await _courierService.UpdateCourierLocationAsync(courierId, locationDto.Latitude, locationDto.Longitude);

                if (!success)
                {
                    return NotFound(new { message = "骑手未找到，无法更新位置。" });
                }

                return Ok(new { message = "位置更新成功。" });
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



        [HttpPut("profile")] // 对应 PUT /api/courier/profile
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto profileDto)
        {
            // 检查模型验证是否通过 (例如 DTO 中的 [Required] 属性)
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var courierId = GetCurrentCourierId();
                var success = await _courierService.UpdateProfileAsync(courierId, profileDto);

                if (!success)
                {
                    return NotFound(new { success = false, message = "用户未找到，更新失败。" });
                }

                return Ok(new { success = true, message = "用户信息更新成功" });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            // 添加对数据库更新异常的捕获，这能提供更明确的错误信息
            catch (DbUpdateException ex)
            {
                // 记录内部异常，以便调试
                Console.WriteLine(ex.InnerException?.Message);
                return StatusCode(500, new { success = false, message = "数据库更新失败，请检查提交的数据是否符合约束。" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"服务器内部错误: {ex.Message}" });
            }
        }


        [HttpGet("profile/for-edit")]
        public async Task<ActionResult<UpdateProfileDto>> GetProfileForEdit()
        {
            try
            {
                var courierId = GetCurrentCourierId();
                // 正确调用 Service 层的方法
                var profileData = await _courierService.GetProfileForEditAsync(courierId);
                if (profileData == null)
                {
                    return NotFound("无法获取用于编辑的用户资料。");
                }
                return Ok(profileData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"服务器内部错误: {ex.Message}");
            }
        }



    }
}
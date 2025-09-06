using BackEnd.DTOs.Courier;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

        [HttpGet("location")]
        public async Task<IActionResult> GetLocation()
        {
            try
            {
                var courierId = GetCurrentCourierId();
                var area = await _courierService.GetCurrentLocationAsync(courierId);
                // 【已修正】直接返回只包含 area 的对象
                return Ok(new { area = area });
            }
            catch (UnauthorizedAccessException) { return Unauthorized(); }
            catch (Exception ex) { return StatusCode(500, $"服务器内部错误: {ex.Message}"); }
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
    }
}
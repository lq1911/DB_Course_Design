using Microsoft.AspNetCore.Mvc;
using BackEnd.Dtos.Delivery;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/delivery-tasks")]
    [Authorize]
    public class DeliveryTasksController : ControllerBase
    {
        private readonly IDeliveryTaskService _deliveryService;

        public DeliveryTasksController(IDeliveryTaskService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        [HttpPost("publish")]
        public async Task<IActionResult> PublishDeliveryTask([FromBody] PublishDeliveryTaskDto dto)
        {
            try
            {
                // 从 Token 中解析商家 ID
                var sellerIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!int.TryParse(sellerIdString, out int sellerId))
                {
                    return Unauthorized(new { code = 401, message = "无效的Token，无法获取商家ID" });
                }

                var result = await _deliveryService.PublishDeliveryTaskAsync(dto, sellerId);
                return Ok(new { deliveryTask = result.DeliveryTask, publish = result.Publish });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { code = 404, message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, new
                {
                    code = 403,
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { code = 400, message = ex.Message });
            }
        }
    }
}
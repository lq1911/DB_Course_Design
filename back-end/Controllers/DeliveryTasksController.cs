using Microsoft.AspNetCore.Mvc;
using BackEnd.Dtos.Delivery;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/delivery-tasks")]
    [Authorize(Roles = "Seller")]
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
                var sellerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
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
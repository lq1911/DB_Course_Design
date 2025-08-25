using Microsoft.AspNetCore.Mvc;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/carts")]
    [Authorize(Roles = "Seller")]
    public class CartsController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public CartsController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{cartId}/items")]
        public async Task<IActionResult> GetCartItems(int cartId)
        {
            try
            {
                var items = await _orderService.GetCartItemsAsync(cartId);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(new { code = 400, message = ex.Message });
            }
        }
    }
}
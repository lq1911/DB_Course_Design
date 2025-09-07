using Microsoft.AspNetCore.Mvc;
using BackEnd.Dtos.Order;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IDeliveryTaskService _deliveryService;

        public OrdersController(IOrderService orderService, IDeliveryTaskService deliveryService)
        {
            _orderService = orderService;
            _deliveryService = deliveryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders([FromQuery] int? sellerId, [FromQuery] int? storeId)
        {
            try
            {
                var orders = await _orderService.GetOrdersAsync(sellerId, storeId);
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(new { code = 400, message = ex.Message });
            }
        }

        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(orderId);
                return order == null ? NotFound(new { code = 404, message = "订单不存在" }) : Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(new { code = 400, message = ex.Message });
            }
        }

        [HttpPost("{orderId}/accept")]
        public async Task<IActionResult> AcceptOrder(int orderId)
        {
            try
            {
                var result = await _orderService.AcceptOrderAsync(orderId);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { code = 404, message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { code = 400, message = ex.Message });
            }
        }

        [HttpPost("{orderId}/reject")]
        public async Task<IActionResult> RejectOrder(int orderId, [FromBody] RejectOrderDto dto)
        {
            try
            {
                var result = await _orderService.RejectOrderAsync(orderId, dto.Reason);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { code = 404, message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { code = 400, message = ex.Message });
            }
        }

        [HttpGet("{orderId}/coupons")]
        public async Task<IActionResult> GetOrderCoupons(int orderId)
        {
            try
            {
                var coupons = await _orderService.GetOrderCouponsAsync(orderId);
                return Ok(coupons);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { code = 404, message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { code = 400, message = ex.Message });
            }
        }

        [HttpGet("{orderId}/delivery-info")]
        public async Task<IActionResult> GetOrderDeliveryInfo(int orderId)
        {
            try
            {
                var info = await _deliveryService.GetOrderDeliveryInfoAsync(orderId);
                return Ok(info);
            }
            catch (Exception ex)
            {
                return BadRequest(new { code = 400, message = ex.Message });
            }
        }
    }
}
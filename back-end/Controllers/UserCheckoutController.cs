using BackEnd.Dtos.User;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserCheckoutController : ControllerBase
    {
        private readonly IUserCheckoutService _userCheckoutService;
        private readonly ILogger<UserCheckoutController> _logger;
        public UserCheckoutController(
            IUserCheckoutService userCheckoutService,
            ILogger<UserCheckoutController> logger)
        {
            _userCheckoutService = userCheckoutService;
            _logger = logger;
        }

        [HttpGet("user/coupons")]
        [ProducesResponseType(typeof(List<UserCouponDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UserCouponDto>>> GetUserCoupons(
            [FromQuery, Required] int userId)
        {
            try
            {
                var coupons = await _userCheckoutService.GetUserCouponsAsync(userId);
                return Ok(coupons);
            }
            catch (ValidationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取用户 {UserId} 的优惠券信息时发生错误", userId);
                return StatusCode(500, new { message = "获取优惠券信息时发生错误" });
            }
        }

        [HttpGet("store/shoppingcart")]
        [ProducesResponseType(typeof(ShoppingCartItemDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ShoppingCartItemDto>> GetShoppingCart(
            [FromQuery, Required] int userId,
            [FromQuery, Required] int storeId)
        {
            try
            {
                var shoppingCart = await _userCheckoutService.GetShoppingCartAsync(userId, storeId);
                return Ok(shoppingCart);
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取用户 {UserId} 在店铺 {StoreId} 的购物车信息时发生错误", userId, storeId);
                return StatusCode(500, new { message = "获取购物车信息时发生错误" });
            }
        }

        // 添加或更新购物车项
        [HttpPost("store/cart/change")]
        [ProducesResponseType(typeof(CartResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CartResponseDto>> UpdateCartItem(
            [FromBody] UpdateCartItemDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _userCheckoutService.UpdateCartItemAsync(dto);
                return Ok(new { message = "购物车更新成功" });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "更新购物车项时发生错误 (CartId={CartId}, DishId={DishId})", dto.CartId, dto.DishId);
                return StatusCode(500, new { message = "更新购物车项时发生错误" });
            }
        }

        // 删除购物车项
        [HttpDelete("store/cart/remove")]
        [ProducesResponseType(typeof(CartResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CartResponseDto>> RemoveCartItem(
            [FromBody] RemoveCartItemDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _userCheckoutService.RemoveCartItemAsync(dto);
                return Ok(new { message = "购物车项删除成功" });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "删除购物车项时发生错误 (CartId={CartId}, DishId={DishId})", dto.CartId, dto.DishId);
                return StatusCode(500, new { message = "删除购物车项时发生错误" });
            }
        }
    }

}
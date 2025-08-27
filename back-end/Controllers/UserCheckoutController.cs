using BackEnd.Dtos.User;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserCouponController : ControllerBase
    {
        private readonly IUserCouponService _userCouponService;
        private readonly ILogger<UserCouponController> _logger;

        public UserCouponController(
            IUserCouponService userCouponService,
            ILogger<UserCouponController> logger)
        {
            _userCouponService = userCouponService;
            _logger = logger;
        }

        [HttpGet("coupons")]
        [ProducesResponseType(typeof(List<UserCouponDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UserCouponDto>>> GetUserCoupons(
            [FromQuery, Required] int userId)
        {
            try
            {
                var coupons = await _userCouponService.GetUserCouponsAsync(userId);
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
    }
    [ApiController]
    [Route("api/store")]
    public class StoreController : ControllerBase
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly ILogger<StoreController> _logger;

        public StoreController(
            IShoppingCartService shoppingCartService,
            ILogger<StoreController> logger)
        {
            _shoppingCartService = shoppingCartService;
            _logger = logger;
        }

        [HttpGet("shoppingcart")]
        [ProducesResponseType(typeof(ShoppingCartDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ShoppingCartDto>> GetShoppingCart(
            [FromQuery, Required] int userId,
            [FromQuery, Required] string storeId)
        {
            try
            {
                var shoppingCart = await _shoppingCartService.GetShoppingCartAsync(userId, storeId);
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
    }
}
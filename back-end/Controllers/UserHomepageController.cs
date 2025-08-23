using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BackEnd.Services.Interfaces;
using BackEnd.Dtos.UserHomepage;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/user/home")]
    public class UserHomepageController : ControllerBase
    {
        private readonly IUserHomepageService _userHomepageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<UserHomepageController> _logger;

        public UserHomepageController(IUserHomepageService userHomepageService)
        {
            _userHomepageService = userHomepageService;
        }

        /// <summary>
        /// 获取推荐商家
        /// GET: /api/user/home/recommend
        /// </summary>
        [HttpGet("recommend")]
        public async Task<IActionResult> GetRecommendedStores()
        {
            var result = await _userHomepageService.GetRecommendedStoresAsync();
            if (result == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = "There's No Recommend Store For User.",
                });
            }
            return Ok(result);
        }

        /// <summary>
        /// 搜索商家和菜品
        /// GET: /api/user/home/search
        /// </summary>
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromBody] HomeSearchDto searchDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Invalid request",
                });
            }

            var (stores, dishes) = await _userHomepageService.SearchAsync(searchDto);

            if (stores == null && dishes == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = "There's No Search results.",
                });
            }

            return Ok(new
                {
                    showStore = new
                    {
                        Stores = stores,
                        Dishes = dishes
                    }
                });
        }
        // 输入：用户id
        // 输出：用户的历史订单
        // GET: /api/user/home/orders`
        [HttpGet("orders")]
        public async Task<IActionResult> GetOrderHistory([FromQuery] UserIdDto userIdDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderHistory = await _userHomepageService.GetOrderHistoryAsync(userIdDto.UserId);

            if (orderHistory == null)
            {
                return NotFound(new
                {
                    code = 404,
                    message = "There's No OrderHistory For User.",
                });
            }

            return Ok(orderHistory);
        }
        // 新增的用户信息接口
        /// <summary>
        /// 获取用户信息
        /// GET: /api/user/home/userInfo
        /// </summary>
        [HttpPost("userInfo")]
        public async Task<IActionResult> GetUserInfo([FromBody] UserIdDto userIdDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = 400, message = "Invalid request format" });
            }

            var userInfo = await _userHomepageService.GetUserInfoAsync(userIdDto.UserId);

            if (userInfo == null)
            {
                return NotFound(new { code = 404, message = "User not found" });
            }

            return Ok(userInfo);
        }
        
        [HttpGet("couponInfo")]
        public async Task<IActionResult> GetUserCoupons([FromQuery] UserIdDto userIdDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Invalid request",
                });
            }

            var coupons = await _userHomepageService.GetUserCouponsAsync(userIdDto);

            if (coupons == null || !coupons.Any())
            {
                return NotFound(new
                {
                    code = 404,
                    message = "There's No Coupon For User.",
                });
            }
            return Ok(coupons);
        }

    }
}

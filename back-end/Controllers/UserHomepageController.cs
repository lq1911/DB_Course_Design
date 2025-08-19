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
        /// GET: /api/user/recommend
        /// </summary>
        [HttpGet("recommend")]
        public async Task<IActionResult> GetRecommendedStores()
        {
            var result = await _userHomepageService.GetRecommendedStoresAsync();
            return Ok(result);
        }

        /// <summary>
        /// 搜索商家和菜品
        /// POST: /api/user/search
        /// </summary>
        [HttpPost("search")]
        public async Task<IActionResult> Search([FromBody] HomeSearchDto searchDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var (stores, dishes) = await _userHomepageService.SearchAsync(searchDto);

            return Ok(new
            {
                Stores = stores,
                Dishes = dishes
            });
        }
        // 输入：用户id
        // 输出：用户的历史订单
        [HttpPost("historyorder")]
        public async Task<IActionResult> GetOrderHistory([FromQuery] UserIdDto userIdDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var orderHistory = await _userHomepageService.GetOrderHistoryAsync(userIdDto.UserId);
            return Ok(orderHistory);
        }
        // 新增的用户信息接口
        /// <summary>
        /// 获取用户信息
        /// POST: /api/user/home/info
        /// </summary>
        [HttpPost("info")]
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

            return Ok(new 
            {
                code = 200,
                message = "success",
                data = new 
                {
                    User = userInfo
                }
            });
        }

    }
}

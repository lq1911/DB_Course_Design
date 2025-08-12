using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BackEnd.Services.Interfaces;
using BackEnd.Dtos.UserHomepage;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserHomepageController : ControllerBase
    {
        private readonly IUserHomepageService _userHomepageService;

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
    }
}

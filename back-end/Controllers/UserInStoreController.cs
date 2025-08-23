using BackEnd.Dtos.UserInStore;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/user/store")]
    public class UserStoreController : ControllerBase
    {
        private readonly IUserInStoreService _userInStoreService;

        public UserStoreController(IUserInStoreService userInStoreService)
        {
            _userInStoreService = userInStoreService;
        }

        /// <summary>
        /// 获取店铺信息
        /// GET /api/user/store/storeInfo?storeId=1
        /// </summary>
        [HttpGet("storeInfo")]
        public async Task<ActionResult<StoreResponseDto>> GetStoreInfo([FromQuery] StoreRequestDto request)
        {
            if (request.StoreID <= 0)
                return BadRequest(new
                {
                    code = 400,
                    message = "店铺编号无效",
                });

            var result = await _userInStoreService.GetStoreInfoAsync(request);

            if (result == null) return NotFound("店铺不存在");

            return Ok(result);
        }

        /// <summary>
        /// 获取菜单（平铺菜品）
        /// GET /api/user/store/dish?userId=1&storeId=1
        /// </summary>
        [HttpGet("dish")]
        public async Task<ActionResult<List<MenuResponseDto>>> GetMenu([FromQuery] MenuRequestDto request)
        {
            if (request.StoreID <= 0 || request.UserID <= 0)
                return BadRequest("参数无效");

            var result = await _userInStoreService.GetMenuAsync(request);
            return Ok(result);
        }

        /// <summary>
        /// 获取商家评论列表
        /// GET /api/user/store/commentList?storeId=1
        /// </summary>
        [HttpGet("commentList")]
        public async Task<ActionResult> GetCommentList([FromQuery] int storeId)
        {
            if (storeId <= 0)
                return BadRequest("店铺编号无效");

            var result = await _userInStoreService.GetCommentListAsync(storeId);

            return Ok(new { comments = result });
        }

        /// <summary>
        /// 获取商家评价状态 [好评数, 中评数, 差评数]
        /// GET /api/user/store/commentState?storeId=1
        /// </summary>
        [HttpGet("commentState")]
        public async Task<ActionResult<CommentStateDto>> GetCommentState([FromQuery] int storeId)
        {
            if (storeId <= 0)
                return BadRequest("店铺编号无效");

            var result = await _userInStoreService.GetCommentStateAsync(storeId);

            return Ok(result);
        }
    }
}

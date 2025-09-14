using BackEnd.Dtos.User;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api")]
    public class UserInStoreController : ControllerBase
    {
        private readonly IUserInStoreService _userInStoreService;
        private readonly ILogger<UserInStoreController> _logger;


        public UserInStoreController(IUserInStoreService userInStoreService, ILogger<UserInStoreController> logger)
        {
            _userInStoreService = userInStoreService;
            _logger = logger;
        }

        /// <summary>
        /// 获取店铺信息
        /// GET /api/user/store/storeInfo?storeId=1
        /// </summary>
        [HttpGet("user/store/storeInfo")]
        public async Task<ActionResult<StoreResponseDto>> GetStoreInfo([FromQuery] StoreRequestDto request)
        {
            if (request.StoreId <= 0)
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
        /// GET /api/store/dish?userId=1&storeId=1
        /// </summary>
        [HttpGet("store/dish")]
        public async Task<ActionResult<List<MenuResponseDto>>> GetMenu([FromQuery] MenuRequestDto request)
        {
            if (request.StoreId <= 0)
                return BadRequest("参数无效");

            var result = await _userInStoreService.GetMenuAsync(request);

            if (result == null) return NotFound("当前无菜品");

            return Ok(result);
        }

        /// <summary>
        /// 获取商家评论列表
        /// GET /api/user/store/commentList?storeId=1
        /// </summary>
        [HttpGet("user/store/commentList")]
        public async Task<ActionResult> GetCommentList([FromQuery] int storeId)
        {
            if (storeId <= 0)
                return BadRequest("店铺编号无效");

            var result = await _userInStoreService.GetCommentListAsync(storeId);

            return Ok(new { comments = result });
        }

        /// <summary>
        /// 获取商家评价状态 [好评数, 中评数, 差评数]
        /// GET /api/user/store/commentStatus?storeId=1
        /// </summary>
        [HttpGet("user/store/commentStatus")]
        public async Task<ActionResult<CommentStateDto>> GetCommentState([FromQuery] int storeId)
        {
            if (storeId <= 0)
                return BadRequest("店铺编号无效");

            var result = await _userInStoreService.GetCommentStateAsync(storeId);

            return Ok(result);
        }

        /// <summary>
        /// 用户评价店铺
        /// POST /api/user/comment
        /// </summary>
        [HttpPost("user/comment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _userInStoreService.SubmitCommentAsync(dto);
                return Ok(new { message = "评论已提交，等待审核" });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "用户提交评论时发生错误 (UserId={UserId}, StoreId={StoreId})", dto.UserId, dto.StoreId);
                return StatusCode(500, new { message = "提交评论时发生错误" });
            }
        }

        // 用户投诉店铺
        [HttpPost("user/store/report")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ReportStore([FromBody] UserStoreReportDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _userInStoreService.SubmitStoreReportAsync(dto);
                return Ok(new { message = "投诉已提交，等待管理员审核" });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "用户投诉店铺时发生错误 (UserId={UserId}, StoreId={StoreId})", dto.UserId, dto.StoreId);
                return StatusCode(500, new { message = "提交投诉时发生错误" });
            }
        }


    }
}

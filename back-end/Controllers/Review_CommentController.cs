using BackEnd.Dtos.Comment;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/admin/review-comments")]
    [Authorize] // 在控制器级别添加此特性，该控制器下所有方法都需要认证
    public class Review_CommentController : ControllerBase
    {
        private readonly IReview_CommentService _reviewCommentService;

        public Review_CommentController(IReview_CommentService reviewCommentService)
        {
            _reviewCommentService = reviewCommentService;
        }

        [HttpGet("mine")]
        public async Task<IActionResult> GetReviewCommentsForAdmin()
        {
            // 从 Token 中安全地获取管理员 ID
            var adminIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(adminIdString, out int adminId))
            {
                return Unauthorized("无效的Token");
            }

            var commentDtos = await _reviewCommentService.GetCommentsForAdminAsync(adminId);

            if (commentDtos == null)
            {
                // 如果找不到资源，按照 RESTful 规范返回 404 Not Found
                return NotFound();
            }

            return Ok(commentDtos);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateReviewComment([FromBody] SetCommentInfo request)
        {
            // 验证请求数据
            if (request == null)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "请求数据不能为空"
                });
            }

            // 调用服务层处理业务逻辑
            var result = await _reviewCommentService.UpdateCommentAsync(request);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}


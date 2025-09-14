using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using BackEnd.Dtos.Review;
using BackEnd.Services.Interfaces;
using System.Security.Claims;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    [Authorize]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReviews([FromQuery] int page, [FromQuery] int pageSize, [FromQuery] string? keyword)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest(new { code = 400, message = "页码和每页数量必须大于0" });
            }

            var sellerIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(sellerIdString, out int sellerId))
            {
                return Unauthorized("无效的Token");
            }

            var result = await _reviewService.GetReviewsAsync(sellerId, page, pageSize, keyword);
            return Ok(result);
        }

        [HttpPost("{id}/reply")]
        public async Task<IActionResult> ReplyToReview(int id, [FromBody] ReplyDto replyDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = 400, message = "请求参数错误" });
            }

            var result = await _reviewService.ReplyToReviewAsync(id, replyDto);
            return Ok(result);
        }
    }
}
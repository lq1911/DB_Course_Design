using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using BackEnd.Dtos.Penalty;
using BackEnd.Services.Interfaces;
using System.Security.Claims;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/penalties")]
    public class PenaltiesController : ControllerBase
    {
        private readonly IPenaltyService _penaltyService;

        public PenaltiesController(IPenaltyService penaltyService)
        {
            _penaltyService = penaltyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPenalties([FromQuery] string? keyword)
        {
            var sellerIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(sellerIdString, out int sellerId))
            {
                return Unauthorized("无效的Token");
            }

            var penalties = await _penaltyService.GetPenaltiesAsync(sellerId, keyword);
            return Ok(penalties);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPenaltyById(string id)
        {
            var penalty = await _penaltyService.GetPenaltyByIdAsync(id);
            if (penalty == null)
            {
                return NotFound(new { code = 404, message = "处罚记录不存在" });
            }
            return Ok(penalty);
        }

        [HttpPost("{id}/appeal")]
        public async Task<IActionResult> AppealPenalty(string id, [FromBody] AppealDto appealDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { code = 400, message = "请求参数错误" });
            }

            var result = await _penaltyService.AppealPenaltyAsync(id, appealDto);
            return Ok(result);
        }
    }
}
using System.Threading.Tasks;
using BackEnd.Dtos.Review;

namespace BackEnd.Services.Interfaces
{
    public interface IReviewService
    {
        Task<RPageResultDto<ReviewDto>> GetReviewsAsync(int page, int pageSize, string? keyword);
        Task<ReplyResponseDto> ReplyToReviewAsync(int id, ReplyDto replyDto);
    }
}
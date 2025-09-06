using BackEnd.Dtos.Comment;

namespace BackEnd.Services.Interfaces
{
    public interface IReview_CommentService
    {
        Task<IEnumerable<GetCommentInfo>> GetCommentsForAdminAsync(int adminId);
        Task<SetCommentInfoResponse> UpdateCommentAsync(SetCommentInfo request);
    }
}

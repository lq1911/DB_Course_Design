using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories.Interfaces
{
    public interface IReview_CommentRepository
    {
        Task<IEnumerable<Review_Comment>> GetAllAsync();
        Task<Review_Comment?> GetByIdAsync(int id);
        Task AddAsync(Review_Comment review_comment);
        Task UpdateAsync(Review_Comment review_comment);
        Task DeleteAsync(Review_Comment review_comment);
        Task SaveAsync();
    }
}
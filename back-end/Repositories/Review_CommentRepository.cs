using BackEnd.Models;
using BackEnd.Data;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories
{
    public class Review_CommentRepository : IReview_CommentRepository
    {
        private readonly AppDbContext _context;
        public Review_CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Review_Comment>> GetAllAsync()
        {
            return await _context.Review_Comments
                                 .Include(rc => rc.Admin)   // 关联管理员
                                 .Include(rc => rc.Comment) // 关联评论
                                 .ToListAsync();
        }

        public async Task<Review_Comment?> GetByIdAsync(int adminId, int commentId)
        {
            return await _context.Review_Comments
                                 .Include(rc => rc.Admin)
                                 .Include(rc => rc.Comment)
                                 .FirstOrDefaultAsync(rc => rc.AdminID == adminId && rc.CommentID == commentId);
        }

        public async Task AddAsync(Review_Comment reviewComment)
        {
            await _context.Review_Comments.AddAsync(reviewComment);
            await SaveAsync();
        }

        public async Task UpdateAsync(Review_Comment reviewComment)
        {
            _context.Review_Comments.Update(reviewComment);
            await SaveAsync();
        }

        public async Task DeleteAsync(Review_Comment reviewComment)
        {
            _context.Review_Comments.Remove(reviewComment);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
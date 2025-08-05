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
            return await _context.Set<Review_Comment>().ToListAsync();
        }

        public async Task<Review_Comment?> GetByIdAsync(int id)
        {
            return await _context.Set<Review_Comment>().FindAsync(id);
        }

        public async Task AddAsync(Review_Comment review_comment)
        {
            _context.Set<Review_Comment>().Add(review_comment);
            await SaveAsync();
        }

        public async Task UpdateAsync(Review_Comment review_comment)
        {
            _context.Set<Review_Comment>().Update(review_comment);
            await SaveAsync();
        }

        public async Task DeleteAsync(Review_Comment review_comment)
        {
            _context.Set<Review_Comment>().Remove(review_comment);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
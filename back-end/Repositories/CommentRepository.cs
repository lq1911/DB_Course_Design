using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }



        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            // 预加载所有关联的实体数据
            return await _context.Comments
                                 .Include(c => c.ReplyToComment) // 加载被回复的评论
                                 .Include(c => c.Store)          // 加载评论所属的店铺
                                 .Include(c => c.Commenter)      // 加载发表评论的顾客
                                 .ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            // 对于单个查询，同样建议预加载关联数据
            return await _context.Comments
                                 .Include(c => c.ReplyToComment)
                                 .Include(c => c.Store)
                                 .Include(c => c.Commenter)
                                 .FirstOrDefaultAsync(c => c.CommentID == id);
        }

        public async Task AddAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
        }

        public async Task UpdateAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await SaveAsync();
        }

        public Task DeleteAsync(Comment comment)
        {
            _context.Comments.Remove(comment);
            return Task.CompletedTask;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
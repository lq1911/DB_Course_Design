using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Models.Enums;
using BackEnd.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
                                 .Include(c => c.FoodOrder)      // 加载评论所属的订单
                                 .Include(c => c.Commenter)      // 加载发表评论的顾客
                                 .Include(c => c.CommentReplies) // 加载评论的回复
                                 .Include(c => c.ReviewComments) // 加载审核评论的管理员
                                     .ThenInclude(rc => rc.Admin)
                                 .ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id)
        {
            // 对于单个查询，同样建议预加载关联数据
            return await _context.Comments
                                 .Include(c => c.ReplyToComment)
                                 .Include(c => c.Store)
                                 .Include(c => c.FoodOrder)
                                 .Include(c => c.Commenter)
                                 .Include(c => c.ReviewComments)
                                     .ThenInclude(rc => rc.Admin)
                                 .FirstOrDefaultAsync(c => c.CommentID == id);
        }

        public async Task<IEnumerable<Comment>> GetBySellerAsync(int sellerId)
        {
            return await _context.Comments
                                .Include(c => c.Store)          // 加载评论所属的店铺
                                    .ThenInclude(s => s!.Seller)
                                .Include(c => c.FoodOrder)      // 加载评论所属的订单
                                .Include(c => c.Commenter)      // 加载发表评论的顾客
                                .Where(c => c.Store!.SellerID == sellerId
                                    && c.CommentState == CommentState.Completed)
                                .OrderBy(c => c.CommentID)
                                .ToListAsync();
        }

        public async Task AddAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await SaveAsync();
        }

        public async Task UpdateAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await SaveAsync();
        }

        public async Task DeleteAsync(Comment comment)
        {
            _context.Comments.Remove(comment);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
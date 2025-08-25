using System;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Dtos.Review;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using BackEnd.Services.Interfaces;

namespace BackEnd.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IFoodOrderRepository _orderRepository; 

        public ReviewService(ICommentRepository commentRepository, IFoodOrderRepository orderRepository)
        {
            _commentRepository = commentRepository;
            _orderRepository = orderRepository;
        }

        public async Task<RPageResultDto<ReviewDto>> GetReviewsAsync(int page, int pageSize, string? keyword)
        {
            var comments = await _commentRepository.GetAllAsync();
            
            // 应用搜索过滤
            if (!string.IsNullOrEmpty(keyword))
            {
                comments = comments.Where(c => 
                    c.Content.Contains(keyword) ||
                    c.CommentID.ToString().Contains(keyword))
                    .ToList();
            }

            // 分页处理
            var total = comments.Count();
            var paginatedComments = comments
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            // 转换为DTO
            var reviewDtos = paginatedComments.Select(c => new ReviewDto
            {
                Id = c.CommentID,
                OrderNo = $"ORD{c.StoreID}",  //对店铺评论，这里使用店铺ID
                User = new RUserInfoDto
                {
                    Name = c.Commenter.User.Username,
                    Phone = c.Commenter.User.PhoneNumber.ToString(),
                    Avatar = c.Commenter.User.Avatar
                },
                Content = c.Content,
                CreatedAt = c.PostedAt.ToString("yyyy-MM-dd HH:mm:ss")
            }).ToList();

            return new RPageResultDto<ReviewDto>
            {
                List = reviewDtos,
                Total = total
            };
        }

        public async Task<ReplyResponseDto> ReplyToReviewAsync(int id, ReplyDto replyDto)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return new ReplyResponseDto
                {
                    Success = false,
                    Message = "评论不存在"
                };
            }

            // 创建回复评论
            var replyComment = new Comment
            {
                Content = replyDto.Content,
                PostedAt = DateTime.Now,
                ReplyToCommentID = id,
                StoreID = comment.StoreID,
                CommenterID = comment.StoreID // 这里应该是商家ID，这里使用店铺ID替代
            };

            await _commentRepository.AddAsync(replyComment);
            
            // 更新原评论的回复数
            comment.Replies++;
            await _commentRepository.UpdateAsync(comment);

            return new ReplyResponseDto
            {
                Success = true,
                Message = "回复成功"
            };
        }
    }
}
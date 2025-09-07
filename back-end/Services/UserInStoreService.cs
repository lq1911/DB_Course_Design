using BackEnd.Dtos.User;
using BackEnd.Models;
using BackEnd.Models.Enums;
using BackEnd.Repositories.Interfaces;
using BackEnd.Services.Interfaces;

namespace BackEnd.Services
{
    
    public class UserInStoreService : IUserInStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IReview_CommentRepository _reviewCommentRepository;
        private readonly IStoreViolationPenaltyRepository _penaltyRepository;
        private readonly ISupervise_Repository _superviseRepository;
        private readonly ICouponRepository _couponRepository;
        private readonly IAdministratorRepository _adminRepository;

        public UserInStoreService(
            IStoreRepository storeRepository,
            ICommentRepository commentRepository,
            IReview_CommentRepository reviewCommentRepository,
            IStoreViolationPenaltyRepository penaltyRepository,
            ISupervise_Repository superviseRepository,
            ICouponRepository couponRepository,
            IAdministratorRepository adminRepository)
        {
            _storeRepository = storeRepository;
            _commentRepository = commentRepository;
            _reviewCommentRepository = reviewCommentRepository;
            _penaltyRepository = penaltyRepository;
            _superviseRepository = superviseRepository;
            _couponRepository = couponRepository;
            _adminRepository = adminRepository;
        }
        /// <summary>
        /// 获取店铺详情
        /// </summary>
        public async Task<StoreResponseDto?> GetStoreInfoAsync(StoreRequestDto request)
        {
            var store = await _storeRepository.GetByIdAsync(request.StoreId);

            if (store == null) return null;

            return new StoreResponseDto
            {
                Id = store.StoreID,
                Name = store.StoreName,
                Image = store.StoreImage, // TODO: 店铺图片字段
                Address = store.StoreAddress,
                OpenTime = store.OpenTime,
                CloseTime = store.CloseTime,
                BusinessHours = $"{store.OpenTime:hh\\:mm}-{store.CloseTime:hh\\:mm}",
                Rating = store.AverageRating,
                MonthlySales = store.MonthlySales,
                Discription = store.StoreFeatures,
                CreationTime = store.StoreCreationTime
            };
        }

        /// <summary>
        /// 获取菜单（平铺所有菜品）
        /// </summary>
        public async Task<List<MenuResponseDto>> GetMenuAsync(MenuRequestDto request)
        {
            var store = await _storeRepository.GetByIdAsync(request.StoreId);

            if (store == null || store.Menus == null) return new List<MenuResponseDto>();

            // 拉平所有 Dish（去重，避免同一个菜品在多个菜单版本出现）
            var dishes = store.Menus
                .SelectMany(m => m.MenuDishes.Select(md => md.Dish))
                .Distinct();

            return dishes.Select(d => new MenuResponseDto
            {
                Id = d.DishID,
                CategoryId = 0, // 目前 Dish 没有分类字段，可以扩展
                Name = d.DishName,
                Description = d.Description,
                Price = d.Price,
                Image = d.DishImage,
                IsSoldOut = d.IsSoldOut
            }).ToList();
        }

        /// <summary>
        /// 获取商家评论列表
        /// </summary>
        public async Task<List<CommentResponseDto>> GetCommentListAsync(int storeId)
        {
            var comments = (await _commentRepository.GetAllAsync())
                .Where(c => c.StoreID == storeId && !(c.CommentState == CommentState.Illegal))
                .OrderByDescending(c => c.PostedAt);

            return comments.Select(c => new CommentResponseDto
            {
                Id = c.CommentID,
                Username = c.Commenter?.User?.Username ?? "匿名用户",
                Rating = c.Rating,
                Date = c.PostedAt,
                Content = c.Content,
                Avatar = c.Commenter?.User?.Avatar ?? "/images/user/default.png",
                Images = string.IsNullOrWhiteSpace(c.CommentImage)
                        ? Array.Empty<string>()  // 返回一个空数组 []
                        : c.CommentImage.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            }).ToList();
        }

        /// <summary>
        /// 获取商家评价状态 [好评数, 中评数, 差评数]
        /// </summary>
        public async Task<CommentStateDto> GetCommentStateAsync(int storeId)
        {
            var comments = (await _commentRepository.GetAllAsync())
                .Where(c => c.StoreID == storeId)
                .Select(c => c.Rating);

            int perfect = comments.Count(r => r == 5);
            int good = comments.Count(r => r == 4);
            int normal = comments.Count(r => r == 3);
            int bad = comments.Count(r => r == 2);
            int awful = comments.Count(r => r == 1);

            return new CommentStateDto
            {
                Status = new List<int> { perfect, good, normal, bad, awful }
            };
        }

        /// <summary>
        /// 用户评价店铺（Pending状态，自动分配管理员）
        /// </summary>
        public async Task SubmitCommentAsync(CreateCommentDto dto)
        {
            var comment = new Comment
            {
                Content = dto.Content,
                PostedAt = DateTime.UtcNow,
                Rating = dto.Rating,
                CommentType = CommentType.Store,
                CommentState = CommentState.Pending,
                StoreID = dto.StoreId,
                CommenterID = dto.UserId
            };

            await _commentRepository.AddAsync(comment);

            // 自动分配管理员
            var admin = await PickAdminAsync();
            if (admin == null)
                throw new InvalidOperationException("没有可用的管理员");

            var review = new Review_Comment
            {
                AdminID = admin.UserID,
                CommentID = comment.CommentID,
                ReviewTime = DateTime.UtcNow
            };
            await _reviewCommentRepository.AddAsync(review);
        }

        /// <summary>
        /// 用户投诉店铺（Pending状态，自动分配管理员）
        /// </summary>
        public async Task SubmitStoreReportAsync(UserStoreReportDto dto)
        {
            var penalty = new StoreViolationPenalty
            {
                ViolationPenaltyState = ViolationPenaltyState.Pending,
                PenaltyReason = dto.Content,
                PenaltyTime = DateTime.UtcNow,
                StoreID = dto.StoreId
            };

            await _penaltyRepository.AddAsync(penalty);

            // 自动分配管理员
            var admin = await PickAdminAsync();
            if (admin == null)
                throw new InvalidOperationException("没有可用的管理员");

            var supervise = new Supervise_
            {
                AdminID = admin.UserID,
                PenaltyID = penalty.PenaltyID
            };
            await _superviseRepository.AddAsync(supervise);
        }

        /// <summary>
        /// 选择一个管理员（这里写了随机，可以换成轮询或负载最小）
        /// </summary>
        private async Task<Administrator?> PickAdminAsync()
        {
            var admins = await _adminRepository.GetAllAsync();
            if (admins == null || !admins.Any())
                return null;

            var random = new Random();
            return admins.ElementAt(random.Next(admins.Count()));
        }
    }
}

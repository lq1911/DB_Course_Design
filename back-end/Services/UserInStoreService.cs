using BackEnd.Dtos.User;
using BackEnd.Repositories.Interfaces;

public class UserInStoreService : IUserInStoreService
{
    private readonly IStoreRepository _storeRepository;
    private readonly ICommentRepository _commentRepository;


    public UserInStoreService(
        IStoreRepository storeRepository,
        ICommentRepository commentRepository)
    {
        _storeRepository = storeRepository;
        _commentRepository = commentRepository;
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
            .Where(c => c.StoreID == storeId)
            .OrderByDescending(c => c.PostedAt);

        return comments.Select(c => new CommentResponseDto
        {
            Id = c.CommentID,
            Username = c.Commenter?.User?.Username ?? "匿名用户",
            Rating = c.Rating,
            Date = c.PostedAt,
            Content = c.Content,
            Avatar = c.Commenter?.User?.Avatar ?? "/images/user/default.png",
            Images = c.CommentImage 
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
}

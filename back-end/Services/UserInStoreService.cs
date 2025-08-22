using BackEnd.Dtos.UserInStore;
using BackEnd.Data;
using Microsoft.EntityFrameworkCore;

public class UserInStoreService : IUserInStoreService
{
    private readonly AppDbContext _dbContext;

    public UserInStoreService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// 获取店铺详情
    /// </summary>
    public async Task<StoreResponseDto?> GetStoreInfoAsync(StoreRequestDto request)
    {
        var store = await _dbContext.Stores
            .FirstOrDefaultAsync(s => s.StoreID == request.StoreID);

        if (store == null) return null;

        return new StoreResponseDto
        {
            StoreID = store.StoreID,
            StoreName = store.StoreName,
            StoreImage = "", // 等待店铺图片字段添加
            StoreAddress = store.StoreAddress,
            OpenTime = store.OpenTime,
            CloseTime = store.CloseTime,
            AverageRating = store.AverageRating,
            MonthlySales = store.MonthlySales,
            StoreDiscription = store.StoreFeatures,
            StoreCreationTime = store.StoreCreationTime
        };
    }

    /// <summary>
    /// 获取菜单（平铺所有菜品）
    /// </summary>
    public async Task<List<MenuResponseDto>> GetMenuAsync(MenuRequestDto request)
    {
        var store = await _dbContext.Stores
            .Include(s => s.Menus!)
                .ThenInclude(m => m.MenuDishes)
                    .ThenInclude(md => md.Dish)
            .FirstOrDefaultAsync(s => s.StoreID == request.StoreID);

        if (store == null || store.Menus == null) return new List<MenuResponseDto>();

        // 拉平所有 Dish（去重，避免同一个菜品在多个菜单版本出现）
        var dishes = store.Menus
            .SelectMany(m => m.MenuDishes.Select(md => md.Dish))
            .Distinct();

        return dishes.Select(d => new MenuResponseDto
        {
            DishID = d.DishID,
            DishCategoryID = 0, // 目前 Dish 没有分类字段，可以扩展
            DishName = d.DishName,
            Description = d.Description,
            Price = d.Price,
            Dishimage = "", // 等待菜品图片字段添加
            IsSoldOut = d.IsSoldOut
        }).ToList();
    }

    /// <summary>
    /// 获取商家评论列表
    /// </summary>
    public async Task<List<CommentResponseDto>> GetCommentListAsync(int storeId)
    {
        var comments = await _dbContext.Comments
            .Where(c => c.StoreID == storeId)
            .Include(c => c.Commenter)
                .ThenInclude(cu => cu.User)
            .OrderByDescending(c => c.PostedAt)
            .ToListAsync();

        return comments.Select(c => new CommentResponseDto
        {
            CommentID = c.CommentID,
            Username = c.Commenter?.User?.Username ?? "匿名用户",
            Rating = c.Rating,
            PostedAt = c.PostedAt,
            Content = c.Content,
            Avatar = c.Commenter?.User?.Avatar ?? "/images/user/default.png",
            CommentImage = Array.Empty<string>() // 目前没图片表，可以后续扩展
        }).ToList();
    }

    /// <summary>
    /// 获取商家评价状态 [好评数, 中评数, 差评数]
    /// </summary>
    public async Task<CommentStateDto> GetCommentStateAsync(int storeId)
    {
        var comments = await _dbContext.Comments
            .Where(c => c.StoreID == storeId)
            .Select(c => c.Rating)
            .ToListAsync();

        int good = comments.Count(r => r >= 4);
        int normal = comments.Count(r => r == 3);
        int bad = comments.Count(r => r <= 2);

        return new CommentStateDto
        {
            Status = new List<int> { good, normal, bad }
        };
    }
}

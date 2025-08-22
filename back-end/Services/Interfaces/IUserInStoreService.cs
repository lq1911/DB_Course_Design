using BackEnd.Dtos.UserInStore;

public interface IUserInStoreService
{
    Task<StoreResponseDto?> GetStoreInfoAsync(StoreRequestDto request);
    Task<List<MenuResponseDto>> GetMenuAsync(MenuRequestDto request);
    Task<List<CommentResponseDto>> GetCommentListAsync(int storeId);
    Task<CommentStateDto> GetCommentStateAsync(int storeId);
}
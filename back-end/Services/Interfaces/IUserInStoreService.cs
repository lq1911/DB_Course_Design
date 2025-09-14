using BackEnd.Dtos.User;

namespace BackEnd.Services.Interfaces
{
    public interface IUserInStoreService
    {
        Task<StoreResponseDto?> GetStoreInfoAsync(StoreRequestDto request);
        Task<List<MenuResponseDto>> GetMenuAsync(MenuRequestDto request);
        Task<List<CommentResponseDto>> GetCommentListAsync(int storeId);
        Task<CommentStateDto> GetCommentStateAsync(int storeId);
        Task SubmitCommentAsync(CreateCommentDto dto);
        Task SubmitStoreReportAsync(UserStoreReportDto dto);

    }
}
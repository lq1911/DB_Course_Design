using BackEnd.Dtos.User;

namespace BackEnd.Services.Interfaces
{
    public interface IUserPlaceOrderService
    {
        Task<ResponseDto> CreateOrderAsync(CreateOrderDto dto);
        
    }
}
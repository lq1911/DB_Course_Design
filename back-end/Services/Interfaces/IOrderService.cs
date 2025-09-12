using BackEnd.Dtos.Cart;
using BackEnd.Dtos.Coupon;
using BackEnd.Dtos.Order;

namespace BackEnd.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<FoodOrderDto>> GetOrdersAsync(int? sellerId, int? storeId);
        Task<FoodOrderDto?> GetOrderByIdAsync(int orderId);
        Task<OrderDecisionDto> AcceptOrderAsync(int orderId);
        Task<OrderDecisionDto> MarkAsReadyAsync(int orderId);
        Task<OrderDecisionDto> RejectOrderAsync(int orderId, string? reason);
        Task<IEnumerable<ShoppingCartItemDto>> GetCartItemsAsync(int cartId);
        Task<IEnumerable<OrderCouponInfoDto>> GetOrderCouponsAsync(int orderId);
    }
}
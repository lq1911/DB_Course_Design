using BackEnd.Dtos.Cart;
using BackEnd.Dtos.Coupon;
using BackEnd.Dtos.Order;
using BackEnd.Models;
using BackEnd.Repositories.Interfaces;
using BackEnd.Services.Interfaces;

namespace BackEnd.Services
{
    public class OrderService : IOrderService
    {
        private readonly IFoodOrderRepository _orderRepo;
        private readonly IShoppingCartItemRepository _cartItemRepo;
        private readonly IStoreRepository _storeRepo;

        public OrderService(IFoodOrderRepository orderRepo, 
                           IShoppingCartItemRepository cartItemRepo,
                           IStoreRepository storeRepo)
        {
            _orderRepo = orderRepo;
            _cartItemRepo = cartItemRepo;
            _storeRepo = storeRepo;
        }

        public async Task<IEnumerable<FoodOrderDto>> GetOrdersAsync(int? sellerId, int? storeId)
        {
            var orders = await _orderRepo.GetAllAsync();
            
            // 筛选逻辑
            if (sellerId.HasValue)
                orders = orders.Where(o => o.Store.SellerID == sellerId.Value);
            if (storeId.HasValue)
                orders = orders.Where(o => o.StoreID == storeId.Value);

            return orders.Select(o => new FoodOrderDto
            {
                OrderId = o.OrderID,
                PaymentTime = o.PaymentTime.ToString("o"),
                Remarks = o.Remarks,
                CustomerId = o.CustomerID,
                CartId = o.CartID,
                StoreId = o.StoreID,
                SellerId = o.Store.SellerID
            });
        }

        public async Task<FoodOrderDto?> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepo.GetByIdAsync(orderId);
            if (order == null) return null;

            return new FoodOrderDto
            {
                OrderId = order.OrderID,
                PaymentTime = order.PaymentTime.ToString("o"),
                Remarks = order.Remarks,
                CustomerId = order.CustomerID,
                CartId = order.CartID,
                StoreId = order.StoreID,
                SellerId = order.Store.SellerID
            };
        }

        public async Task<OrderDecisionDto> AcceptOrderAsync(int orderId)
        {
            var order = await _orderRepo.GetByIdAsync(orderId) 
                ?? throw new KeyNotFoundException("订单不存在");

            return new OrderDecisionDto
            {
                OrderId = orderId,
                Decision = "accepted",
                DecidedAt = DateTime.Now.ToString("o")
            };
        }

        public async Task<OrderDecisionDto> RejectOrderAsync(int orderId, string? reason)
        {
            var order = await _orderRepo.GetByIdAsync(orderId) 
                ?? throw new KeyNotFoundException("订单不存在");

            return new OrderDecisionDto
            {
                OrderId = orderId,
                Decision = "rejected",
                DecidedAt = DateTime.Now.ToString("o"),
                Reason = reason
            };
        }

        public async Task<IEnumerable<ShoppingCartItemDto>> GetCartItemsAsync(int cartId)
        {
            var items = await _cartItemRepo.GetByCartIdAsync(cartId); 
            return items.Select(it => new ShoppingCartItemDto
            {
                ItemId = it.ItemID,
                Quantity = it.Quantity,
                TotalPrice = it.TotalPrice,
                DishId = it.DishID,
                CartId = it.CartID,
                Dish = it.Dish != null ? new CartItemDishRefDto
                {
                    DishId = it.Dish.DishID,
                    DishName = it.Dish.DishName,
                    Price = it.Dish.Price,
                    Description = it.Dish.Description,
                    IsSoldOut = (int)it.Dish.IsSoldOut
                } : null
            });
        }

        public async Task<IEnumerable<OrderCouponInfoDto>> GetOrderCouponsAsync(int orderId)
        {
            var order = await _orderRepo.GetByIdAsync(orderId) 
                ?? throw new KeyNotFoundException("订单不存在");

            return order.Coupons?.Select(c => new OrderCouponInfoDto
            {
                CouponId = c.CouponID,
                CouponName = $"优惠券{c.CouponID}",
                Description = $"满{c.CouponManager.MinimumSpend}减{c.CouponManager.DiscountAmount}元",
                DiscountType = "fixed",
                DiscountValue = c.CouponManager.DiscountAmount,
                ValidFrom = c.CouponManager.ValidFrom.ToString("o"),
                ValidTo = c.CouponManager.ValidTo.ToString("o"),
                IsUsed = true
            }) ?? Enumerable.Empty<OrderCouponInfoDto>();
        }
    }
}
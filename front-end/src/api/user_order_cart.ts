export interface ShoppingCartItem {
  itemId: number;      // 购物车项ID
  dishId: number;      // 菜品ID
  quantity: number;    // 数量
  totalPrice: number;  // 单项总价 (单价 * 数量)
}

// 整个购物车
export interface ShoppingCart {
  cartId: number;              // 购物车ID
  totalPrice: number;          // 购物车总价
  lastUpdatedTime: string;     // 最后更新时间 (ISO字符串或 yyyy-MM-dd HH:mm:ss)
  orderId?: number | null;     // 订单ID，下单后填充
  items: ShoppingCartItem[];   // 购物车项列表
}


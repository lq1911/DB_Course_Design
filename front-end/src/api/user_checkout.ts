import { getData } from '@/api/multiuse_function'
import { postData } from '@/api/multiuse_function'
import { deleteData } from '@/api/multiuse_function'

export interface ShoppingCartItem {
    itemId: number;
    dishId: number;
    quantity: number;
    totalPrice: number;
}

export interface ShoppingCart {
    cartId: number;
    totalPrice: number;
    items: ShoppingCartItem[];
}

// 菜品信息
export interface MenuItem {
    id: number;
    categoryId: number;
    name: string;
    description: string;
    price: number;
    image: string;
    isSoldOut: number;
}

export interface Order{
    paymentTime: Date;
    customerID: number;
    cartID: number;
    storeID: number;
}

export async function getMenuItem(StoreID: string) {
    return getData<MenuItem[]>("/api/store/dish", {
        params: {
            storeId: StoreID
        }
    });
}

export async function getShoppingCart(StoreID: string, UserID: number) {
    return getData<ShoppingCart>("/api/store/shoppingcart", {
        params: {
            storeId: StoreID,
            userId: UserID
        }
    });
}

export async function addOrUpdateCartItem(cartId: number, dishId: number, quantity: number) {
    return postData<ShoppingCartItem>('/api/store/cart/change', { cartId, dishId, quantity });
}

export async function removeCartItem(cartId: number, dishId: number) {
    return deleteData<ShoppingCartItem>('/api/store/cart/remove', { cartId, dishId });
}

export async function submitOrder(customerId: number, cartId: number, storeId: number) {
    // 1. 创建 Date 对象，并立即转换为标准的 ISO 8601 字符串
    const paymentTimeString = new Date().toISOString(); // 例如 "2025-09-09T08:00:00.123Z"

    // 2. 构建请求体，字段名与后端 DTO 保持一致
    const requestBody = {
        PaymentTime: paymentTimeString, // 【确认】发送的是字符串
        CustomerId: customerId,
        CartId: cartId,
        StoreId: storeId
    };

    // 3. 打印日志用于调试
    console.log("正在提交订单 (使用字符串格式)，请求体:", requestBody);

    // 4. 发送请求
    return postData<Order>('/api/store/checkout', requestBody);
}

export async function useCoupon(couponId: number | null) {
    if (couponId == null) return;
    return postData(`/api/user/checkout/coupon`, { couponId });
}
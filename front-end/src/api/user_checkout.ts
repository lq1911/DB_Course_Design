import { getData } from '@/api/multiuse_function'

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

export async function getMenuItem(StoreID: string) {
    return getData<MenuItem[]>("/api/store/dish", {
        params: {
            storeId: StoreID
        }
    });
}

export async function getShoppingCart(StoreID: string) {
    return getData<ShoppingCart>("/api/store/shoppingcart", {
        params: {
            storeId: StoreID
        }
    });
}

export async function addOrUpdateCartItem(cartId: number, dishId: number, quantity: number) {
  return getData('/api/cart/item', {
    method: 'POST',
    data: { cartId, dishId, quantity }
  });
}

// 删除购物车项
export async function removeCartItem(cartId: number, dishId: number) {
  return getData('/api/cart/item', {
    method: 'DELETE',
    data: { cartId, dishId }
  });
}
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
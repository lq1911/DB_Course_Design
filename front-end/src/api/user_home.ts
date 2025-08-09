import axios from 'axios'

export interface RecomStore {
    id: number
    averageRating: number
    name: string
    monthlySales: number
}

export interface SearchStore {
    averageRating: number
    name: string
    monthlySales: number
    storeAddress: string
}

export interface OrderInfo {
    orderID: number
    paymentTime: string
    cartID: number
    storeID: number
    storeImage: string
    storeName: string
}

export interface UserInfo {
    name: string
    phoneNumber: number
    image: string
}

export interface CouponInfo {
    couponID: number
    CouponState: number
    orderID: number
    couponManagerID: number
    minimumSpend: number
    discountAmount: number
    validTo: string
}

export async function getData<T>(url: string): Promise<T> {
    try {
        const response = await axios.get(url);
        return response.data;
    } catch (error: unknown) {
        let message = "message字段不存在!";
        
        if (axios.isAxiosError(error)) {
            message = error.message ?? message;
            console.log(`请求失败: ${error.response?.status}，报错信息为${message}`);
        } else if (error instanceof Error) {
            message = error.message ?? message;
            console.log(`请求失败，报错信息为${message}`);
        } else {
            console.log(`请求失败，未知错误:`, error);
        }

        throw error;
    }
}

export async function getRecomStore() {
    return getData<RecomStore[]>(`??`);
}

export async function getSearchStore() {
    return getData<SearchStore[]>(`??`);
}

export async function getOrderInfo(id: number) {
    return getData<OrderInfo[]>(`??`);
}

export async function getUserInfo(id: number) {
    return getData<UserInfo>(`??`);
}

export async function getCouponInfo(id: number) {
    return getData<CouponInfo[]>(`??`);
}
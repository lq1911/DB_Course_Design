import { getData } from '@/api/multiuse_function'

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
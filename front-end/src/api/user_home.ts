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
    dishImage: string[]
}

export interface UserInfo {
    name: string
    phoneNumber: number
    image: string
}

export async function getRecomStore() {
    return getData<RecomStore[]>(`/api/user/home/recommend`);
}

export async function getSearchStore(UserID: number, Address: string, Keyword: string) {
    return getData<SearchStore[]>(`/api/user/home/search`, {
        params: {
            userId: UserID,
            address: Address,
            keyword: Keyword
        }
    });
}

export async function getOrderInfo(UserId: number) {
    return getData<OrderInfo[]>(`/api/user/home/orders`, {
        params: {
            userId: UserId
        }
    });
}

export async function getUserInfo(UserId: number) {
    return getData<UserInfo>(`/api/user/home/userinfo`, {
        params: {
            userId: UserId
        }
    });
}
import { postData } from '@/api/multiuse_function'

export interface StoreReport{
    userId: number;
    storeId: number;
    content: string;
}

export interface RiderReport{
    userId: number;
    orderId: number;
    content: string;
}

export async function postStoreReport(userId: number, storeId: number, content: string) {
    return postData<StoreReport>(`/api/user/store/report`, {userId, storeId, content})
}

export async function postRiderReport(userId: number, orderId: number, content: string) {
    return postData<RiderReport>(`/api/user/courier/report`, {userId, orderId, content})
}
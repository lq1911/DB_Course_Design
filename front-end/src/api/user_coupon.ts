import { getData } from '@/api/multiuse_function'

export interface CouponInfo {
    couponID: number
    couponState: number
    orderID: number
    couponManagerID: number
    minimumSpend: number
    discountAmount: number
    validTo: string
}

export async function getCouponInfo(id: number) {
    return getData<CouponInfo[]>(`??`);
}
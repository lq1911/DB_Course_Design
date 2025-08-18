import { getData } from '@/api/multiuse_function'

export interface AccountInfo{
    name: string;
    phoneNumber: number;
    image: string;
}

export async function getCouponInfo(id: number) {
    return getData<AccountInfo>(`??`);
}
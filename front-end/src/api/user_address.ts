import { getData } from '@/api/multiuse_function'

export interface Address{
    name: string;
    phoneNumber: number;
    address: string;
}

export async function getAddress(UserId: number) {
    return getData<Address>("/api/user/profile/address", {
        params: {
            userId: UserId
        }
    });
}
import { getData } from '@/api/multiuse_function'

export interface AccountInfo{
    name: string;
    phoneNumber: number;
    image: string;
}

export async function getAccountInfo(UserId: number) {
    return getData<AccountInfo>("/api/user/profile/userProfile", {
        params: {
            userId: UserId
        }
    });
}
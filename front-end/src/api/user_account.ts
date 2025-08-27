import { getData } from '@/api/multiuse_function'
import { putData } from '@/api/multiuse_function';

export interface AccountInfo{
    id: number;
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

export async function saveAccountInfo(data: AccountInfo) {
  return putData<AccountInfo>("/api/account/update", data);
}
import { getData } from '@/api/multiuse_function'

export interface Address{
    name: string;
    phoneNumber: number;
    address: string;
}

export async function getAddress<Address>(id: number) {
    return getData<Address>('??');
}
import axios from 'axios'

export interface Recom_Store{
    id: number
    averageRating: number
    name: string
    monthlySales: number
}

export interface Search_Store{
    averageRating: number
    name: string
    monthlySales: number
    storeAddress: string
}

export interface Order_Info{
    orderID: number
    paymentTime: string
    cartID: number
    storeID: number
    storeImage: string
    storeName: string
}

export async function getRecomStore() {
    try {
        const response = await axios.get(`??`);
        return response.data;
    } catch (error: any) {
        console.log(`请求失败: ${error.response.status}，报错信息为${error.response.message}`)
        throw error;
    }
}

export async function getSearchStore() {
    try {
        const response = await axios.get(`??`);
        return response.data;
    } catch (error: any) {
        console.log(`请求失败: ${error.response.status}，报错信息为${error.response.message}`)
        throw error;
    }
}

export async function getOrderInfo(id: number) {
    try {
        const response = await axios.get(`??`);
        return response.data;
    } catch (error: any) {
        console.log(`请求失败: ${error.response.status}，报错信息为${error.response.message}`)
        throw error;
    }
}
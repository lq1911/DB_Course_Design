import axios, { AxiosRequestConfig } from 'axios';

export async function getData<T>(url: string): Promise<T>;
export async function getData<T>(url: string, config: AxiosRequestConfig): Promise<T>;

export async function getData<T>(url: string, config?: AxiosRequestConfig): Promise<T> {
    try {
        const response = await axios.get<T>(url, config);
        return response.data;
    } catch (error: unknown) {
        let message = "message字段不存在!";
        
        if (axios.isAxiosError(error)) {
            message = error.message ?? message;
            console.log(`请求失败: ${error.response?.status}，报错信息为${message}`);
        } else if (error instanceof Error) {
            message = error.message ?? message;
            console.log(`请求失败，报错信息为${message}`);
        } else {
            console.log(`请求失败，未知错误:`, error);
        }

        throw error;
    }
}

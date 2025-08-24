import axios, { AxiosRequestConfig } from 'axios';
import API from './index'

export async function getData<T>(url: string, config?: AxiosRequestConfig): Promise<T> {
    try {
        const response = await API.get<T>(url, config);
        return response.data;
    } catch (error: unknown) {
        handleAxiosError(error);
        throw error;
    }
}

export async function postData<T, D = any>(url: string, data?: D, config?: AxiosRequestConfig): Promise<T> {
    try {
        const response = await API.post<T>(url, data, config);
        return response.data;
    } catch (error: unknown) {
        handleAxiosError(error);
        throw error;
    }
}

export async function deleteData<T, D = any>(url: string, data?: D, config?: AxiosRequestConfig): Promise<T> {
    try {
        const response = await API.delete<T>(url, { ...config, data });
        return response.data;
    } catch (error: unknown) {
        handleAxiosError(error);
        throw error;
    }
}

function handleAxiosError(error: unknown) {
    let message = '请求失败，未知错误'
    if (axios.isAxiosError(error)) {
        message = error.message ?? message
    } else if (error instanceof Error) {
        message = error.message ?? message
    }
    console.warn(message)
}
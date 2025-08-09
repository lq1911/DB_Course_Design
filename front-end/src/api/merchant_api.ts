// src/services/merchant_api.ts
import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:3000/api', 
  timeout: 5000,
});

// 获取店铺概览数据
export const getShopOverview = async () => {
  try {
    const response = await api.get('/shop/overview');
    return response.data;
  } catch (error) {
    console.error('获取店铺概览数据失败:', error);
    throw error;
  }
};

// 获取店铺基本信息
export const getShopInfo = async () => {
  try {
    const response = await api.get('/shop/info');
    return response.data;
  } catch (error) {
    console.error('获取店铺基本信息失败:', error);
    throw error;
  }
};

// 获取商家信息
export const getMerchantInfo = async () => {
  try {
    const response = await api.get('/merchant/info');
    return response.data;
  } catch (error) {
    console.error('获取商家信息失败:', error);
    throw error;
  }
};

// 切换营业状态
export const toggleBusinessStatus = async (status: boolean) => {
  try {
    const response = await api.patch('/shop/status', { isOpen: status });
    return response.data;
  } catch (error) {
    console.error('切换营业状态失败:', error);
    throw error;
  }
};

// 更新店铺信息
export const updateShopField = async (field: string, value: string) => {
  try {
    const response = await api.patch('/shop/update-field', {
      field,  // 字段名
      value   // 字段值
    });
    return response.data;
  } catch (error) {
    console.error(`更新${field}失败:`, error);
    throw error;
  }
};

// src/services/api.js
import axios from 'axios';


//变量声明，登录
interface UserData {
  account: string;
  password: string;
  role: 'consumer' | 'rider' | 'admin' |'merchant' | string;
}

/** 骑手特定信息接口，登录 */
interface RiderInfo {
  vehicleType: string;
}

/** 管理员特定信息接口，登录 */
interface AdminInfo {
  managementObject: string;
  handledItems: string;
}

/** 店铺/商家特定信息接口，登录 */
interface StoreInfo {
  name: string;
  address: string;
  businessHours: string;
  establishmentDate: string;
  businessLicense: File | null; // 假设是文件对象或 null
  category: string;

}

interface RegistrationData {
  // 基础注册信息 (来自 registerForm)
  nickname: string;
  password: string; 
  confirmPassword?: string;// 发送给后端时，密码应该是可选的，因为有时确认密码字段不发送
  phone: string;
  email: string;
  gender: string;
  birthday: string;
  isPublic: number;
  verificationCode: string;

  // 核心角色信息
  role: 'consumer' | 'rider' | 'admin' | 'merchant' | string;

  // 可选的角色特定信息
  // 属性名后面加 `?` 表示这个属性是可选的
  riderInfo?: RiderInfo;
  adminInfo?: AdminInfo;
  storeInfo?: StoreInfo;
}

// 创建一个 Axios 实例，您可以配置基础 URL 和超时时间
const apiClient = axios.create({
  baseURL: 'http://localhost:3000/api', // 这里是您后端服务器的地址和基础路径
  timeout: 10000, // 请求超时时间
  headers: {
    'Content-Type': 'application/json'
  }
});

// 导出封装好的 API 请求函数
export default {
  // 注册接口
  register(data:RegistrationData) {
    return apiClient.post('/register', data);
  },
  // 登录接口
  login(data:UserData) {
    return apiClient.post('/login', data);
  }
  // ...未来可以添加更多接口，比如获取用户信息、更新资料等
};


// 获取用户资料
export const fetchUserProfile = () => apiClient.get('/profile');

// 获取工作状态
export const fetchWorkStatus = () => apiClient.get('/work-status');

// 获取位置信息 (已补全)
export const fetchLocationInfo = () => apiClient.get('/location');


// --- 订单管理 ---

// 获取订单列表
export const fetchOrders = (status: string) => apiClient.get(`/orders?status=${status}`);

// 接受订单
export const acceptOrderAPI = (orderId: string) => apiClient.post(`/orders/${orderId}/accept`);

// 拒绝订单
export const rejectOrderAPI = (orderId: string) => apiClient.post(`/orders/${orderId}/reject`);


// --- 收入与状态变更 ---

// 获取收入汇总数据
export const fetchIncomeData = () => apiClient.get('/earnings/summary');

// 获取收入明细列表 (已补全)
export const fetchEarnings = () => apiClient.get('/earnings/details');

// 切换工作状态
export const toggleWorkStatusAPI = (status: boolean) => apiClient.post('/work-status/toggle', { status });

export const updateUserProfile = (profileData: any) => apiClient.put('/profile', profileData);

export const fetchNewOrder = (notificationId: string) => apiClient.get(`/orders/new/${notificationId}`);
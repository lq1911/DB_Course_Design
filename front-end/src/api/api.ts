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


import type {
    UserProfile,
    WorkStatus,
    IncomeData,
    Order,
    NewOrder,
    LocationInfo
} from './api.mock.ts';

/** 获取用户个人资料 */
export const fetchUserProfile = () => {
    return apiClient.get<UserProfile>('/user/profile');
};

/** 获取骑手工作状态 */
export const fetchWorkStatus = () => {
    return apiClient.get<WorkStatus>('/user/status');
};

/** 
 * 获取收入数据
 * 注意：根据您最新的 mock 文件，这个接口期望直接返回一个数字 (当月收入)
 */
export const fetchIncomeData = () => {
    // 假设后端接口直接返回一个数字，而不是一个JSON对象
    // 如果后端返回的是 { "thisMonth": 3450 }，则需要使用 .then(res => res.data.thisMonth) 进行处理
    return apiClient.get<number>('/user/income/thisMonth');
};

/** 
 * 根据状态获取订单列表 
 * @param status - 订单状态 ('pending', 'delivering', 'completed')
 */
export const fetchOrders = (status: string) => {
    return apiClient.get<Order[]>('/orders', {
        params: { status } // 将 status 作为 URL 查询参数，例如: /orders?status=pending
    });
};

/** 获取骑手当前位置信息 */
export const fetchLocationInfo = () => {
    return apiClient.get<LocationInfo>('/user/location');
};

/** 
 * 根据通知ID获取新订单详情
 * @param notificationId - 新订单的推送通知ID
 */
export const fetchNewOrder = (notificationId: string) => {
    return apiClient.get<NewOrder>(`/orders/new/${notificationId}`);
};

/** 
 * 切换工作状态 (上班/下班) 
 * @param newStatus - 新的工作状态 (true: 上线, false: 下线)
 */
export const toggleWorkStatusAPI = (newStatus: boolean) => {
    // 通常使用 POST 或 PUT 请求来改变服务器上的状态
    return apiClient.post<{ success: boolean }>('/user/status', { isOnline: newStatus });
};

/** 
 * 接受订单 
 * @param orderId - 要接受的订单ID
 */
export const acceptOrderAPI = (orderId: string) => {
    return apiClient.post<{ success: boolean }>(`/orders/${orderId}/accept`);
};

/** 
 * 拒绝订单 
 * @param orderId - 要拒绝的订单ID
 */
export const rejectOrderAPI = (orderId: string) => {
    return apiClient.post<{ success: boolean }>(`/orders/${orderId}/reject`);
};
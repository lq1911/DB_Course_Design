// src/services/api.js
import axios from 'axios';

// --- 接口定义 (Interfaces) ---
// 这部分大部分保持不变，但我们会为更新资料新增接口

// 登录数据接口
interface UserData {
  password:string;
  phoneNum: string;
  role: string;
}

// 注册时基础数据接口 (注意：我们稍后会在Vue组件中确保不把 realName 放进来)
interface RegistrationData {
  nickname: string;
  password: string;
  phone: string;
  email: string;
  gender: string;
  birthday: string;
  verificationCode: string;
  role: string;
  // ... 其他注册所需的基础字段
}

// --- 新增：用于更新个人资料的接口定义 ---

// 更新用户通用资料 (真实姓名等)
interface UserProfileData {
  realName: string;
  // 如果未来还有其他通用信息，比如个人简介，可以在此添加
  // bio?: string;
}

// 更新骑手特定信息
interface RiderInfo {
  vehicleType: string;
}

// 更新管理员特定信息
interface AdminInfo {
  managementObject: string;
  handledItems: string;
}

// 更新店铺/商家特定信息
interface StoreInfo {
  name: string;
  address: string;
  businessHours: string;
  establishmentDate: string;
  businessLicense: File | null;
  category: string;
}


// --- Axios 实例和拦截器 ---

const apiClient = axios.create({
  baseURL: 'http://localhost:5250/api', 
  timeout: 10000,
});

/**
 * 【核心改动】添加请求拦截器
 *
 * 作用: 在每个请求发送出去之前，自动检查本地是否存有 token。
 * 如果有，就把它添加到请求的 Authorization 头部。
 *
 * 好处: 所有需要登录才能访问的接口，我们都不再需要手动添加 token，代码更简洁、不易出错。
 */
apiClient.interceptors.request.use(config => {
  // 从 localStorage 获取 token
  const token = localStorage.getItem('authToken');
  if (token) {
    // 如果 token 存在，则设置 Authorization 请求头
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
}, error => {
  return Promise.reject(error);
});


// --- 导出的 API 函数 ---

export default {
  /**
   * 1. 注册接口 (公开访问)
   *    - 只负责创建最基础的账号
   */
  register(data: RegistrationData | FormData) { // <--- 修改这里
    return apiClient.post('/register', data);
  },

  /**
   * 2. 登录接口 (公开访问)
   *    - 用于获取身份认证 Token
   */
  login(data: UserData) {
    return apiClient.post('/login', data);
  },

  /**
   * 3. 【新增】更新用户通用资料接口 (需要认证)
   *    - 用于提交真实姓名等信息
   */
  updateUserProfile(profileData: UserProfileData) {
    // 这个请求会发送给后端的 PUT /user/profile 或 PATCH /user/profile 接口
    // 因为拦截器的存在，这个请求会自动带上 token
    return apiClient.put('/user/profile', profileData);
  },

  /**
   * 4. 【新增】更新骑手专属资料接口 (需要认证)
   */
  updateRiderInfo(riderData: RiderInfo) {
    return apiClient.put('/user/profile/rider', riderData);
  },

  /**
   * 5. 【新增】更新管理员专属资料接口 (需要认证)
   */
  updateAdminInfo(adminData: AdminInfo) {
    return apiClient.put('/user/profile/admin', adminData);
  },

  /**
   * 6. 【新增】更新商家专属资料接口 (需要认证)
   */
  updateStoreInfo(storeData: StoreInfo) {
    return apiClient.put('/user/profile/merchant', storeData);
  }
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
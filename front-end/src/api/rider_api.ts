// src/api/rider.api.ts
import apiClient from './client'; // 导入我们共享的客户端

// --- 从 .mock.ts 或其他地方导入骑手相关的数据类型 ---
import type {
    UserProfile,
    WorkStatus,
    Order,
    NewOrder,
    LocationInfo
} from './api.mock.ts'; // 假设类型定义在这里


interface RiderInfo {
  vehicleType: string;
}

// --- 导出的 API 函数 ---

/** 获取用户（骑手）个人资料 */
export const fetchUserProfile = () => {
    return apiClient.get<UserProfile>('/user/profile');
};

/** 获取骑手工作状态 */
export const fetchWorkStatus = () => {
    return apiClient.get<WorkStatus>('/user/status');
};

/** 获取收入数据 */
export const fetchIncomeData = () => {
    return apiClient.get<number>('/user/income/thisMonth');
};

/** 根据状态获取订单列表 */
export const fetchOrders = (status: 'pending' | 'delivering' | 'completed') => {
    return apiClient.get<Order[]>('/orders', { params: { status } });
};

/** 获取骑手当前位置信息 */
export const fetchLocationInfo = () => {
    return apiClient.get<LocationInfo>('/user/location');
};

/** 根据通知ID获取新订单详情 */
export const fetchNewOrder = (notificationId: string) => {
    return apiClient.get<NewOrder>(`/orders/new/${notificationId}`);
};

/** 切换工作状态 (上班/下班) */
export const toggleWorkStatus = (newStatus: boolean) => {
    return apiClient.post<{ success: boolean }>('/user/status', { isOnline: newStatus });
};

/** 接受订单 */
export const acceptOrder = (orderId: string) => {
    return apiClient.post<{ success: boolean }>(`/orders/${orderId}/accept`);
};

/** 拒绝订单 */
export const rejectOrder = (orderId: string) => {
    return apiClient.post<{ success: boolean }>(`/orders/${orderId}/reject`);
};

/** 更新骑手专属资料接口 (需要认证) */
export const updateRiderInfo = (riderData: RiderInfo) => {
    return apiClient.put('/user/profile/rider', riderData);
};
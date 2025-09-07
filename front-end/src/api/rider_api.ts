// src/api/rider.api.ts
import apiClient from './client'; // 导入我们共享的客户端

// --- 从 .mock.ts 或其他地方导入骑手相关的数据类型 ---
import type {
    UserProfile,
    WorkStatus,
    Order,
    NewOrder,
    LocationInfo
} from './api.mock'; // 假设类型定义在这里

// --- 修正后的 API 函数 ---

/** 获取用户（骑手）个人资料 */
export const fetchUserProfile = () => {
    // 【已修正】路径从 /user/profile 改为 /courier/profile
    return apiClient.get<UserProfile>('/courier/profile');
};

/** 获取骑手工作状态 */
export const fetchWorkStatus = () => {
    // 【已修正】路径从 /user/status 改为 /courier/status
    // 我们的后端返回 { code, message, data: { isOnline: boolean } }
    // apiClient 应该配置为自动提取 data 字段，所以这里的类型 WorkStatus 是正确的
    return apiClient.get<WorkStatus>('/courier/status');
};

/** 获取收入数据 */
export const fetchIncomeData = () => {
    // 【已修正】路径从 /income/thisMonth 改为 /courier/income/monthly
    // 我们的后端直接返回纯数字，所以类型是 number
    return apiClient.get<number>('/courier/income/monthly');
};

/** 根据状态获取订单列表 */
export const fetchOrders = (status: 'pending' | 'delivering' | 'completed') => {
    // 【已修正】路径正确，并且使用 params 来传递查询参数
    // 这将生成正确的 URL: /api/courier/orders?status=pending
    return apiClient.get<Order[]>('/courier/orders', { params: { status } });
};

/** 获取骑手当前位置信息 */
export const fetchLocationInfo = () => {
    // 【已修正】路径从 /user/location 改为 /courier/location
    // 后端返回 { data: { area: "..." } }，所以需要一个匹配的类型
    return apiClient.get<LocationInfo>('/courier/location');
};

/** 根据通知ID获取新订单详情 */
export const fetchNewOrder = (notificationId: string) => {
    // 【已修正】路径正确
    return apiClient.get<NewOrder>(`/courier/orders/new/${notificationId}`);
};

/** 切换工作状态 (上班/下班) */
export const toggleWorkStatusAPI = (newStatus: boolean) => {
    // 【已修正】路径从 /user/status 改为 /courier/status/toggle
    return apiClient.post<{ success: boolean }>('/courier/status/toggle', { isOnline: newStatus });
};

/** 接受订单 */
export const acceptOrderAPI = (orderId: string) => {
    // 【已修正】路径正确
    return apiClient.post<{ success: boolean }>(`/courier/orders/${orderId}/accept`);
};

/** 拒绝订单 */
export const rejectOrderAPI = (orderId: string) => {
    // 【已修正】路径正确
    return apiClient.post<{ success: boolean }>(`/courier/orders/${orderId}/reject`);
};

// --- 以下是你队友原来的其他接口，可以暂时保留 ---
interface RiderInfo {
    vehicleType: string;
}
export const updateRiderInfo = (riderData: RiderInfo) => {
    return apiClient.put('/user/profile/rider', riderData);
};
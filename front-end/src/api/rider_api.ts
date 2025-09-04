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
    // [待确认] 如果此接口依然404，请与后端确认路径是否正确。
    // 例如，后端提供的路径可能是 /rider/profile 而不是 /user/profile
    return apiClient.get<UserProfile>('/user/profile');
};

/** 获取骑手工作状态 */
export const fetchWorkStatus = () => {
    // [待确认] 如果此接口依然404，请与后端确认路径。
    return apiClient.get<WorkStatus>('/user/status');
};

/** 获取收入数据 */
export const fetchIncomeData = () => {
    // [待确认] 浏览器报错 /api/income/thisMonth 404，请与后端确认路径。
    // 注意这里是 /income 开头，和其他 /user 不一样，请确认是否正确。
    return apiClient.get<number>('/income/thisMonth');
};

/** 根据状态获取订单列表 */
export const fetchOrders = (status: 'pending' | 'delivering' | 'completed') => {
    // [已修正] -------------------------------------------------------------
    // 根据浏览器报错信息 (GET .../api/orders/status-pending)，
    // 后端需要的不是查询参数 `?status=pending`，而是将状态作为路径的一部分。
    // 因此，我们将URL的拼接方式从 params 改为直接嵌入路径中。
    //
    // 原代码: return apiClient.get<Order[]>('/orders', { params: { status } });
    // ---------------------------------------------------------------------
    return apiClient.get<Order[]>(`/orders/status-${status}`);
};

/** 获取骑手当前位置信息 */
export const fetchLocationInfo = () => {
    // [待确认] 如果此接口依然404，请与后端确认路径。
    return apiClient.get<LocationInfo>('/user/location');
};

/** 根据通知ID获取新订单详情 */
export const fetchNewOrder = (notificationId: string) => {
    return apiClient.get<NewOrder>(`/orders/new/${notificationId}`);
};

/** 切换工作状态 (上班/下班) */
export const toggleWorkStatusAPI = (newStatus: boolean) => {
    return apiClient.post<{ success: boolean }>('/user/status', { isOnline: newStatus });
};

/** 接受订单 */
export const acceptOrderAPI = (orderId: string) => {
    return apiClient.post<{ success: boolean }>(`/orders/${orderId}/accept`);
};

/** 拒绝订单 */
export const rejectOrderAPI = (orderId: string) => {
    return apiClient.post<{ success: boolean }>(`/orders/${orderId}/reject`);
};

/** 更新骑手专属资料接口 (需要认证) */
export const updateRiderInfo = (riderData: RiderInfo) => {
    return apiClient.put('/user/profile/rider', riderData);
};
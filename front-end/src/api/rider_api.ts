// src/api/rider.api.ts
import apiClient from './client'; // 导入我们共享的客户端

// --- 从 .mock.ts 或其他地方导入骑手相关的数据类型 ---
import type {
    UserProfile,
    WorkStatus,
    Order,
    NewOrder,
    UpdateProfilePayload,
    OrderStatus,
    LocationInfo,
    Complaint
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
export const fetchOrders = (status:  OrderStatus) => {
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



/** 切换工作状态 (上班/下班) */
export const toggleWorkStatusAPI = (newStatus: boolean) => {
    // 【已修正】路径从 /user/status 改为 /courier/status/toggle
    return apiClient.post<{ success: boolean }>('/courier/status/toggle', { isOnline: newStatus });
};



// 在文件末尾新增这两个函数

/**
 * 骑手确认取单
 * @param orderId 订单ID
 */
export const pickupOrderAPI = (orderId: string) => {
    return apiClient.post<{ success: boolean }>(`/courier/orders/${orderId}/pickup`);
};

/**
 * 骑手确认送达
 * @param orderId 订单ID
 */
export const deliverOrderAPI = (orderId: string) => {
    return apiClient.post<{ success: boolean }>(`/courier/orders/${orderId}/deliver`);
};

// --- 以下是你队友原来的其他接口，可以暂时保留 ---
interface RiderInfo {
    vehicleType: string;
}
export const updateRiderInfo = (riderData: RiderInfo) => {
    return apiClient.put('/user/profile/rider', riderData);
};

/**
 * 骑手接受一个可接订单 (抢单)
 * @param orderId 订单ID
 */
export const acceptAvailableOrderAPI = (orderId: string) => {
    // 这个接口和之前弹窗的 acceptOrderAPI 路径可能一样，也可能不一样
    // 根据后端约定，这里我们假设路径是 /accept
    return apiClient.post<{ success: true }>(`/courier/orders/${orderId}/accept`);
};


/**
 * 获取骑手的投诉记录列表
 */
export const fetchComplaints = () => {
    // 假设后端提供的获取投诉列表的路径是 /courier/complaints
    // 请务必与后端开发人员确认此路径
    return apiClient.get<Complaint[]>('/courier/complaints');
};

/**
 * 获取当前骑手附近的可接订单列表
 */
export const fetchAvailableOrders = () => {
    // 调用后端为我们准备的新接口
    return apiClient.get<Order[]>('/courier/orders/available');
};


/**
 * 更新骑手在服务器上的位置信息
 * @param latitude 纬度
 * @param longitude 经度
 */
export const updateCourierLocationAPI = (latitude: number, longitude: number) => {
    // 调用我们刚刚在后端创建的 POST /api/courier/location/update 接口
    return apiClient.post('/courier/location/update', { latitude, longitude });
};


/**
 * 更新用户（骑手）的个人资料
 * @param profileData 包含更新信息的用户对象
 */
export const updateUserProfile = (profileData: UpdateProfilePayload) => {
    return apiClient.put<{ success: boolean; message: string }>('/courier/profile', profileData);
};

/** 获取用于编辑页面的个人资料 */
export const fetchProfileForEdit = () => {
    // 后端返回 UpdateProfileDto，其结构与 UpdateProfilePayload 兼容
    return apiClient.get<UpdateProfilePayload>('/courier/profile/for-edit');
};

/**
 * 上传头像文件
 * @param file 图片文件对象
 */
export const uploadAvatarAPI = (file: File) => {
    const formData = new FormData();
    formData.append('file', file); // 'file' 必须与后端 IFormFile 参数名一致

    return apiClient.post<{ url: string }>('/files/upload/avatar', formData, {
        headers: {
            'Content-Type': 'multipart/form-data',
        },
    });
};
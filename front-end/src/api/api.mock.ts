// src/api/api.mock.ts

// ===================================================================
//  1. 精简后的接口定义 (Streamlined Interfaces)
// ===================================================================

/** 用户个人资料 (所有属性均在模板中使用) */
export interface UserProfile {
    name: string;
    id: string;
    registerDate: string;
    rating: number;
    creditScore: number;
}

/** 
 * 工作状态详情 
 * @description 只保留了模板中用于判断和切换状态的 isOnline 属性。
 * @removed onlineHours, onlineMinutes, todayOrders, completedOrders, punctualityRate (模板中未显示这些统计数据)
 */
export interface WorkStatus {
    isOnline: boolean;
}

/** 
 * 收入数据结构
 * @description 模板中薪资只显示一个位置，因此只保留一个核心字段。我们保留 thisMonth 作为示例。
 * @removed today, thisWeek, monthlyOrders (如果需要可以加回，但当前模板只适合显示一个值)
 */
export interface IncomeData {
    thisMonth: number;
}

/** 
 * 订单信息
 * @description 移除了模板中未显示的 time 属性。
 * @removed time
 */
export interface Order {
    id: string;
    status: 'pending' | 'delivering' | 'completed';
    restaurant: string;
    address: string;
    fee: string;
    statusText: string;
}

/** 新订单详情 (所有属性均在模板或逻辑中使用) */
export interface NewOrder {
    id: string; // 虽然不在模板中，但在 acceptOrder/rejectOrder 逻辑中必需
    restaurantName: string;
    restaurantAddress: string;
    customerName: string;
    customerAddress: string;
    distance: string;
    fee: number;
    mapImageUrl: string;
}

/** 位置信息 (所有属性均在模板中使用) */
export interface LocationInfo {
    area: string;
    longitude: number; // 新增：经度
    latitude: number;  // 新增：纬度
}


// ===================================================================
//  2. 精简后的模拟数据 (Streamlined Mock Data)
// ===================================================================

const mockUserProfile: UserProfile = {
    name: '张明辉 (模拟)',
    id: 'RD20241201',
    registerDate: '2023-08-15',
    rating: 4.8,
    creditScore: 892
};

const mockWorkStatus: WorkStatus = {
    isOnline: true
};

const mockIncomeData: IncomeData = {
    thisMonth: 3450
};

const mockOrders: Order[] = [
    { id: 'ORD-MOCK-001', restaurant: '模拟-肯德基', address: '模拟-人民广场1号', fee: '10.00', status: 'pending', statusText: '待取单' },
    { id: 'ORD-MOCK-002', restaurant: '模拟-麦当劳', address: '模拟-南京路2号', fee: '8.50', status: 'pending', statusText: '待取单' },
    { id: 'ORD-MOCK-003', restaurant: '模拟-星巴克', address: '模拟-淮海路3号', fee: '15.00', status: 'delivering', statusText: '配送中' },
    { id: 'ORD-MOCK-004', restaurant: '模拟-必胜客', address: '模拟-西藏中路4号', fee: '12.00', status: 'completed', statusText: '已送达' },
];

const mockLocationInfo: LocationInfo = {
    area: '人民广场商圈 (模拟)',
    longitude: 121.4748,
    latitude: 31.2304
};

const mockNewOrder: NewOrder = {
    id: 'ORD-MOCK-NEW-001',
    restaurantName: '肯德基 (模拟-南京路店)',
    restaurantAddress: '模拟-上海市黄浦区南京东路 789 号',
    customerName: '王女士',
    customerAddress: '模拟-上海市黄浦区福州路 321 号 15 楼',
    distance: '2.3km',
    fee: 12.50,
    mapImageUrl: 'https://readdy.ai/api/search-image?query=Simple%20delivery%20route%20map%20showing%20pickup%20and%20delivery%20locations%20with%20orange%20markers%2C%20clean%20interface%20design%2C%20mobile%20app%20style%20map%20view%2C%20clear%20navigation%20paths%2C%20professional%20delivery%20service%20aesthetic&width=280&height=160&seq=ordermap001&orientation=landscape'
};


// ===================================================================
//  3. 模拟 API 函数 (无需修改)
// ===================================================================

function createMockResponse<T>(data: T, delay = 300) {
    return new Promise<{ data: T }>(resolve => {
        setTimeout(() => { resolve({ data: data }); }, delay);
    });
}

export const fetchUserProfile = () => createMockResponse(mockUserProfile);
export const fetchWorkStatus = () => createMockResponse(mockWorkStatus);
export const fetchIncomeData = () => createMockResponse(mockIncomeData.thisMonth);
export const fetchOrders = (status: string) => createMockResponse(mockOrders.filter(o => o.status === status));
export const fetchLocationInfo = () => createMockResponse(mockLocationInfo);
export const fetchNewOrder = () => createMockResponse(mockNewOrder);

export const toggleWorkStatusAPI = (newStatus: boolean) => {
    mockWorkStatus.isOnline = newStatus;
    console.log(`[Mock] 工作状态切换为: ${newStatus ? '在线' : '离线'}`);
    return createMockResponse({ success: true });
};

export const acceptOrderAPI = (orderId: string) => {
    console.log(`[Mock] 接受订单: ${orderId}`);
    return createMockResponse({ success: true });
};

export const rejectOrderAPI = (orderId: string) => {
    console.log(`[Mock] 拒绝订单: ${orderId}`);
    return createMockResponse({ success: true });
};

// 在文件末尾新增这两个函数

/** 模拟骑手取单 */
export const pickupOrderAPI = (orderId: string) => {
    console.log(`[Mock] 正在取单: ${orderId}`);
    const order = mockOrders.find(o => o.id === orderId);
    if (order) {
        order.status = 'delivering'; // 核心逻辑：改变状态
    }
    return createMockResponse({ success: true });
};

/** 模拟骑手送达订单 */
export const deliverOrderAPI = (orderId: string) => {
    console.log(`[Mock] 确认送达: ${orderId}`);
    const order = mockOrders.find(o => o.id === orderId);
    if (order) {
        order.status = 'completed'; // 核心逻辑：改变状态
    }
    return createMockResponse({ success: true });
};
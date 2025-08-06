// src/services/api.mock.ts

// --- 模拟数据定义 ---

const mockUserProfile = { name: '张明辉 (模拟)', id: 'RD20241201', registerDate: '2023-08-15', rating: 4.8, creditScore: 892 };

const mockWorkStatus = { isOnline: true, onlineHours: 3, onlineMinutes: 45, todayOrders: 8, completedOrders: 7, punctualityRate: 99.1 };

const mockIncomeData = { today: 128.50, thisWeek: 765.80, thisMonth: 3450.00, monthlyOrders: 98 };

const mockOrders = [
    { id: 'ORD-MOCK-001', restaurant: '模拟-肯德基', address: '模拟-人民广场1号', fee: '10.00', time: '10:30', status: 'pending', statusText: '待取餐' },
    { id: 'ORD-MOCK-002', restaurant: '模拟-麦当劳', address: '模拟-南京路2号', fee: '8.50', time: '10:45', status: 'pending', statusText: '待取餐' },
    { id: 'ORD-MOCK-003', restaurant: '模拟-星巴克', address: '模拟-淮海路3号', fee: '15.00', time: '09:50', status: 'delivering', statusText: '配送中' },
    { id: 'ORD-MOCK-004', restaurant: '模拟-必胜客', address: '模拟-西藏中路4号', fee: '12.00', time: '09:30', status: 'completed', statusText: '已完成' },
];

// 新增: 模拟的收入明细数据
const mockEarnings = [
    { id: 1, type: '配送费 (模拟)', date: '2024-12-01 14:30', amount: '8.50' },
    { id: 2, type: '配送费 (模拟)', date: '2024-12-01 14:15', amount: '12.00' },
    { id: 3, type: '高峰补贴 (模拟)', date: '2024-12-01 13:45', amount: '5.00' },
    { id: 4, type: '配送费 (模拟)', date: '2024-12-01 13:30', amount: '15.50' }
];

// 新增: 模拟的位置信息
const mockLocationInfo = { area: '人民广场商圈 (模拟)' };


/** 模拟API延迟的辅助函数 */
function createMockResponse(data: any, delay = 300) {
    return new Promise(resolve => {
        setTimeout(() => { resolve({ data: data }); }, delay);
    });
}

// --- 导出与真实API同名的模拟函数 ---

export const fetchUserProfile = () => createMockResponse(mockUserProfile);
export const fetchWorkStatus = () => createMockResponse(mockWorkStatus);
export const fetchIncomeData = () => createMockResponse(mockIncomeData);
export const fetchOrders = (status: string) => createMockResponse(mockOrders.filter(order => order.status === status));

// 新增: 导出获取收益明细的函数
export const fetchEarnings = () => createMockResponse(mockEarnings);
// 新增: 导出获取位置信息的函数
export const fetchLocationInfo = () => createMockResponse(mockLocationInfo);


export const toggleWorkStatusAPI = (status: boolean) => {
    mockWorkStatus.isOnline = status;
    console.log(`[Mock] 工作状态切换为: ${status}`);
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

export const updateUserProfile = (profileData: any) => {
    console.log('[Mock] 收到来自表单的用户资料:', profileData);
    // 更新内存中的原始模拟数据
    Object.assign(mockUserProfile, profileData);
    return createMockResponse({ success: true, message: '用户信息更新成功！' });
};

// 新增: 模拟的新订单详情数据
const mockNewOrder = {
    id: 'ORD-MOCK-NEW-001',
    restaurantName: '肯德基 (模拟-南京路店)',
    restaurantAddress: '模拟-上海市黄浦区南京东路 789 号',
    customerName: '王女士',
    customerAddress: '模拟-上海市黄浦区福州路 321 号 15 楼',
    distance: '2.3km',
    fee: 12.50,
    mapImageUrl: 'https://readdy.ai/api/search-image?query=Simple%20delivery%20route%20map%20showing%20pickup%20and%20delivery%20locations%20with%20orange%20markers%2C%20clean%20interface%20design%2C%20mobile%20app%20style%20map%20view%2C%20clear%20navigation%20paths%2C%20professional%20delivery%20service%20aesthetic&width=280&height=160&seq=ordermap001&orientation=landscape'
};


// 新增: 导出获取新订单详情的函数
export const fetchNewOrder = () => createMockResponse(mockNewOrder);
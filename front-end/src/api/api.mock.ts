// src/api/api.mock.ts

export type OrderStatus = 'to_be_taken' | 'pending' | 'delivering' | 'completed' | 'cancelled';

/** 用户个人资料 (所有属性均在模板中使用) */
export interface UserProfile {
    name: string;
    id: string;
    registerDate: string;
    rating: number;
    creditScore: number;
}


// ▼▼▼ 在其他 interface 定义的旁边，添加下面这个 ▼▼▼
export interface Complaint {
  complaintID: string;
  deliveryTaskID: string;
  complaintTime: string;
  complaintReason: string;
  punishment?: { // punishment 是可选的
    type: string;
    description: string;
    duration?: string; // duration 也是可选的
  };
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
// ▼▼▼ 将旧的 Order 接口替换为这个 ▼▼▼
export interface Order {
  id: string;
  status: OrderStatus; // 使用我们统一的、更精确的类型
  restaurant: string;
  pickupAddress: string;   // 取餐地址
  deliveryAddress: string; // 送达地址
  customer: string;        // 顾客姓名
  fee: string;
  distance: string;        // 配送距离
  time: string;            // 预计时间
}
// ▲▲▲ 替换结束 ▲▲▲

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
    // 您的 to_be_taken 订单 (这些是正确的，保留)
    {
        id: 'AVAIL-001', status: 'to_be_taken', restaurant: '模拟-一点点奶茶',
        pickupAddress: '模拟-科技园路1号', deliveryAddress: '模拟-软件大厦A座 10楼',
        customer: '李先生', fee: '15.00', distance: '1.5', time: '10'
    },
    {
        id: 'AVAIL-002', status: 'to_be_taken', restaurant: '模拟-肯德基宅急送',
        pickupAddress: '模拟-人民广场1号', deliveryAddress: '模拟-市政府大楼 3楼',
        customer: '王女士', fee: '12.50', distance: '2.3', time: '18'
    },

    // --- 以下是修正后的旧订单 ---
    {
        id: 'ORD-MOCK-001', status: 'pending', restaurant: '模拟-肯德基',
        pickupAddress: '模拟-人民广场1号', deliveryAddress: '模拟-客户家A',
        customer: '客户A', fee: '10.00', distance: '2.0', time: '15'
    },
    {
        id: 'ORD-MOCK-002', status: 'pending', restaurant: '模拟-麦当劳',
        pickupAddress: '模拟-南京路2号', deliveryAddress: '模拟-客户家B',
        customer: '客户B', fee: '8.50', distance: '1.2', time: '10'
    },
    {
        id: 'ORD-MOCK-003', status: 'delivering', restaurant: '模拟-星巴克',
        pickupAddress: '模拟-淮海路3号', deliveryAddress: '模拟-客户家C',
        customer: '客户C', fee: '15.00', distance: '3.1', time: '20'
    },
    {
        id: 'ORD-MOCK-004', status: 'completed', restaurant: '模拟-必胜客',
        pickupAddress: '模拟-西藏中路4号', deliveryAddress: '模拟-客户家D',
        customer: '客户D', fee: '12.00', distance: '2.5', time: '18'
    },
        // ▼▼▼ 新增已取消订单数据 ▼▼▼
    {
        id: 'ORD-CXL-001', status: 'cancelled', restaurant: '模拟-汉堡王',
        pickupAddress: '模拟-陆家嘴环路5号', deliveryAddress: '模拟-客户家E',
        customer: '客户E', fee: '9.00', distance: '0.8', time: '8'
    },
    {
        id: 'ORD-CXL-002', status: 'cancelled', restaurant: '模拟-海底捞外送',
        pickupAddress: '模拟-世纪大道6号', deliveryAddress: '模拟-客户家F',
        customer: '客户F', fee: '25.00', distance: '4.5', time: '30'
    },
];


// ▼▼▼ 在 mockOrders, mockLocationInfo 等数据的旁边，添加下面这个 ▼▼▼
const mockComplaints: Complaint[] = [
    {
        complaintID: 'CPL-001',
        deliveryTaskID: 'ORD-MOCK-003',
        complaintTime: '2024-08-21 14:30',
        complaintReason: '送达时间过长，比预计晚了20分钟。',
        punishment: {
            type: '超时罚款',
            description: '因严重超时导致客户投诉，罚款20元。',
        }
    },
    {
        complaintID: 'CPL-002',
        deliveryTaskID: 'ORD-MOCK-004',
        complaintTime: '2024-08-22 19:00',
        complaintReason: '餐品有少量泼洒。',
    },
    {
        complaintID: 'CPL-003',
        deliveryTaskID: 'ORD-MOCK-001',
        complaintTime: '2024-08-23 12:15',
        complaintReason: '骑手态度不佳。',
        punishment: {
            type: '服务警告',
            description: '因服务态度问题收到警告一次，请注意改善服务质量。',
            duration: '7天'
        }
    },
];

const mockLocationInfo: LocationInfo = {
    area: '人民广场商圈 (模拟)',
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
export const fetchOrders = (status: OrderStatus) => createMockResponse(mockOrders.filter(o => o.status === status));
export const fetchLocationInfo = () => createMockResponse(mockLocationInfo);

export const toggleWorkStatusAPI = (newStatus: boolean) => {
    mockWorkStatus.isOnline = newStatus;
    console.log(`[Mock] 工作状态切换为: ${newStatus ? '在线' : '离线'}`);
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

export const acceptAvailableOrderAPI = (orderId: string) => {
    console.log(`[Mock] 正在接受订单: ${orderId}`);
    const order = mockOrders.find(o => o.id === orderId);
    if (order && order.status === 'to_be_taken') {
        order.status = 'pending';
    }
    return createMockResponse({ success: true });
};

/**
 * 模拟更新用户个人资料
 * @param profileData 用户提交的新资料
 */
export const updateUserProfile = (profileData: any) => {
    // 打印到控制台，方便调试，看看前端发了什么数据
    console.log('[Mock] 正在更新用户资料:', profileData);

    // 可选：在内存中更新 mockUserProfile 的数据，这样模拟更真实
    // 如果重新加载这个页面，数据会保持最新状态（直到刷新浏览器）
    if (profileData.name) {
        mockUserProfile.name = profileData.name;
    }
    // 你也可以在这里添加对 gender 和 birthday 的更新逻辑
    
    // 返回一个成功的模拟响应
    return createMockResponse({ success: true, message: '用户信息更新成功' });
};


// ▼▼▼ 在其他 export const 函数的末尾，添加下面这个 ▼▼▼
export const fetchComplaints = () => createMockResponse(mockComplaints);

/**
 * 【新增】模拟获取骑手附近的可接订单
 * 在模拟环境中，我们简单返回所有状态为 'to_be_taken' 的订单
 */
export const fetchAvailableOrders = () => {
    console.log('[Mock] 正在获取可接订单 (模拟附近)...');
    const available = mockOrders.filter(o => o.status === 'to_be_taken');
    return createMockResponse(available);
};
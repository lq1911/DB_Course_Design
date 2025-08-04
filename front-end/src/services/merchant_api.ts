// src/services/merchant_api.ts
import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:3000/api',
  timeout: 5000,
});

// 获取店铺概览数据
export const getShopOverview = async () => {
  try {
    const response = await api.get('/shop/overview');
    return response.data;
  } catch (error) {
    console.error('获取店铺概览数据失败:', error);
    throw error;
  }
};

// 获取店铺基本信息
export const getShopInfo = async () => {
  try {
    const response = await api.get('/shop/info');
    return response.data;
  } catch (error) {
    console.error('获取店铺基本信息失败:', error);
    throw error;
  }
};

// 获取商家信息
export const getMerchantInfo = async () => {
  try {
    const response = await api.get('/merchant/info');
    return response.data;
  } catch (error) {
    console.error('获取商家信息失败:', error);
    throw error;
  }
};

// 切换营业状态
export const toggleBusinessStatus = async (status: boolean) => {
  try {
    const response = await api.patch('/shop/status', { isOpen: status });
    return response.data;
  } catch (error) {
    console.error('切换营业状态失败:', error);
    throw error;
  }
};

// 更新店铺信息
export const updateShopField = async (field: string, value: string) => {
  try {
    const response = await api.patch('/shop/update-field', {
      field,  // 字段名
      value   // 字段值
    });
    return response.data;
  } catch (error) {
    console.error(`更新${field}失败:`, error);
    throw error;
  }
};


// ==================== 类型定义 ====================

// 订单相关类型
interface UserInfo {
  name: string;
  phone: string;
  avatar?: string;
}

interface OrderDish {
  name: string;
  price: number;
  quantity: number;
}

interface Order {
  orderNo: string;
  payTime: string;
  status: '未接单' | '已接单' | '已出餐' | '配送中' | '已送达';
  remark: string;
  userInfo: UserInfo;
  dishes: OrderDish[];
}

// 菜品相关类型
interface Dish {
  id: number;
  name: string;
  price: number;
  category: string;
  description: string;
  status: '上架中' | '已下架';
  image: string;
}

interface NewDishData {
  name: string;
  price: string;
  category: string;
  description: string;
  imagePreview?: string;
}

// 聊天消息类型
interface ChatMessage {
  sender: 'user' | 'merchant';
  content: string;
  time: string;
}


// ==================== 订单管理接口 ====================

// 获取订单列表 - 返回符合Order类型的数据
export const getOrders = async (params?: {
  status?: 'all' | '未接单' | '已接单' | '已出餐' | '配送中' | '已送达';
}) => {
  try {
    const response = await api.get('/orders', { params });
    // 返回的数据应该是Order[]类型
    return response.data as Order[];
  } catch (error) {
    console.error('获取订单列表失败:', error);
    throw error;
  }
};

// 订单详情类型定义
interface OrderDetail extends Order {
  deliveryAddress: string;
  fees: {
    originalPrice: number;
    packingFee: number;
    deliveryFee: number;
    couponDiscount: number;
    finalPrice: number;
    couponName?: string;
  };
}

// 获取订单详情 - 包含费用明细等完整信息
export const getOrderDetail = async (orderNo: string) => {
  try {
    const response = await api.get(`/orders/${orderNo}`);
    return response.data as OrderDetail;
  } catch (error) {
    console.error('获取订单详情失败:', error);
    throw error;
  }
};

// 更新订单状态
export const updateOrderStatus = async (orderNo: string, status: '已接单' | '已出餐' | '配送中' | '已送达') => {
  try {
    const response = await api.patch(`/orders/${orderNo}/status`, { status });
    return response.data;
  } catch (error) {
    console.error('更新订单状态失败:', error);
    throw error;
  }
};

// 发布配送任务 - 将订单状态从"已出餐"改为"配送中"
export const publishDeliveryTask = async (orderNo: string) => {
  try {
    const response = await api.post(`/orders/${orderNo}/publish-delivery`);
    return response.data;
  } catch (error) {
    console.error('发布配送任务失败:', error);
    throw error;
  }
};

// 获取自动接单状态
export const getAutoAcceptStatus = async () => {
  try {
    const response = await api.get('/shop/auto-accept-status');
    return response.data as { autoAcceptOrders: boolean };
  } catch (error) {
    console.error('获取自动接单状态失败:', error);
    throw error;
  }
};

// 切换自动接单状态
export const toggleAutoAcceptOrders = async (enabled: boolean) => {
  try {
    const response = await api.patch('/shop/auto-accept', { enabled });
    return response.data;
  } catch (error) {
    console.error('切换自动接单状态失败:', error);
    throw error;
  }
};

// 模拟添加新订单 - 用于测试
export const addNewOrder = async () => {
  try {
    const response = await api.post('/orders/simulate-new');
    return response.data as Order;
  } catch (error) {
    console.error('模拟新订单失败:', error);
    throw error;
  }
};

// ==================== 菜品管理接口 ====================

// 获取菜品列表
export const getDishes = async (params?: {
  status?: 'all' | '上架中' | '已下架';
}) => {
  try {
    const response = await api.get('/dishes', { params });
    return response.data as Dish[];
  } catch (error) {
    console.error('获取菜品列表失败:', error);
    throw error;
  }
};

// 创建菜品
export const createDish = async (dishData: NewDishData) => {
  try {
    const response = await api.post('/dishes', {
      name: dishData.name,
      price: Number(dishData.price),
      category: dishData.category,
      description: dishData.description,
      status: '上架中',
      image: dishData.imagePreview || 'https://readdy.ai/api/search-image?query=delicious%20Chinese%20cuisine%20dish%20beautifully%20plated%20on%20white%20background%20professional%20food%20photography&width=80&height=80&seq=dish-new&orientation=squarish'
    });
    return response.data as Dish;
  } catch (error) {
    console.error('创建菜品失败:', error);
    throw error;
  }
};

// 更新菜品信息
export const updateDish = async (dishId: number, dishData: {
  name?: string;
  price?: number;
  category?: string;
  description?: string;
  image?: string;
}) => {
  try {
    const response = await api.patch(`/dishes/${dishId}`, dishData);
    return response.data as Dish;
  } catch (error) {
    console.error('更新菜品信息失败:', error);
    throw error;
  }
};

// 切换菜品上下架状态
export const toggleDishStatus = async (dishId: number, status: '上架中' | '已下架') => {
  try {
    const response = await api.patch(`/dishes/${dishId}/toggle-status`, { status });
    return response.data;
  } catch (error) {
    console.error('切换菜品状态失败:', error);
    throw error;
  }
};

// 获取菜品分类数据 - 返回Vue中定义的categoryMap结构
export const getDishCategories = async () => {
  try {
    const response = await api.get('/dishes/categories');
    return response.data as {
      [key: string]: string[];
    };
  } catch (error) {
    console.error('获取菜品分类失败:', error);
    throw error;
  }
};

// ==================== 沟通聊天接口 ====================

// 获取与用户的聊天记录
export const getChatMessages = async (orderNo: string) => {
  try {
    const response = await api.get(`/chat/order/${orderNo}/messages`);
    return response.data as ChatMessage[];
  } catch (error) {
    console.error('获取聊天记录失败:', error);
    throw error;
  }
};

// 发送消息给用户
export const sendMessage = async (orderNo: string, content: string) => {
  try {
    const response = await api.post(`/chat/order/${orderNo}/send`, {
      content,
      sender: 'merchant',
      time: new Date().toLocaleTimeString()
    });
    return response.data as ChatMessage;
  } catch (error) {
    console.error('发送消息失败:', error);
    throw error;
  }
};

// 模拟用户拨号 - 用于callUser功能
export const callUser = async (phone: string) => {
  try {
    const response = await api.post('/communication/call', { phone });
    return response.data;
  } catch (error) {
    console.error('拨打电话失败:', error);
    throw error;
  }
};



// ==================== 图片上传接口 ====================

// 上传菜品图片
export const uploadDishImage = async (file: File) => {
  try {
    const formData = new FormData();
    formData.append('image', file);

    const response = await api.post('/upload/dish-image', formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
    return response.data as { imageUrl: string };
  } catch (error) {
    console.error('上传菜品图片失败:', error);
    throw error;
  }
};
/*
// ==================== 表情和常用语接口 ====================

// 获取表情包列表
export const getEmojiList = async () => {
  try {
    const response = await api.get('/chat/emojis');
    return response.data as string[];
  } catch (error) {
    console.error('获取表情包列表失败:', error);
    throw error;
  }
};

// 获取常用语列表
export const getQuickPhrases = async () => {
  try {
    const response = await api.get('/chat/quick-phrases');
    return response.data as string[];
  } catch (error) {
    console.error('获取常用语列表失败:', error);
    throw error;
  }
};

// 保存常用语
export const saveQuickPhrase = async (phrase: string) => {
  try {
    const response = await api.post('/chat/quick-phrases', { phrase });
    return response.data;
  } catch (error) {
    console.error('保存常用语失败:', error);
    throw error;
  }
};
*/
// ==================== 工具函数 ====================

// 通用错误处理
export const handleApiError = (error: any) => {
  if (error.response) {
    const { status, data } = error.response;
    switch (status) {
      case 400:
        return `请求参数错误: ${data.message || '参数验证失败'}`;
      case 401:
        return '登录已过期，请重新登录';
      case 403:
        return '没有权限执行此操作';
      case 404:
        return '请求的资源不存在';
      case 500:
        return '服务器内部错误，请稍后重试';
      default:
        return `请求失败: ${data.message || '未知错误'}`;
    }
  } else if (error.request) {
    return '网络连接失败，请检查网络设置';
  } else {
    return `请求配置错误: ${error.message}`;
  }
};

// 请求拦截器 - 添加认证token
api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('merchant_token');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

// 响应拦截器 - 统一处理错误
api.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    const errorMessage = handleApiError(error);

    // 如果是401错误，清除token并跳转到登录页
    if (error.response?.status === 401) {
      localStorage.removeItem('merchant_token');
      // 这里可以添加路由跳转到登录页的逻辑
      // router.push('/login');
    }

    return Promise.reject(new Error(errorMessage));
  }
);
// src/services/merchant_api.ts
import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5250/api',
  timeout: 5000,
});

// 获取店铺概览数据
export const getShopOverview = async () => {
  try {
    console.log('正在请求店铺概览数据...');
    const response = await api.get('/shop/overview');
    console.log('店铺概览数据响应:', response.data);
    return response.data;
  } catch (error: any) {
    console.error('获取店铺概览数据失败:');
    console.error('错误类型:', error.constructor.name);
    console.error('错误消息:', error.message);
    if (error.response) {
      console.error('响应状态:', error.response.status);
      console.error('响应数据:', error.response.data);
      console.error('响应头:', error.response.headers);
    } else if (error.request) {
      console.error('请求配置:', error.request);
    }
    console.error('完整错误对象:', error);
    throw error;
  }
};

// 获取店铺基本信息
export const getShopInfo = async () => {
  try {
    console.log('正在请求店铺基本信息...');
    const response = await api.get('/shop/info');
    console.log('店铺基本信息响应:', response.data);
    return response.data;
  } catch (error: any) {
    console.error('获取店铺基本信息失败:');
    console.error('错误类型:', error.constructor.name);
    console.error('错误消息:', error.message);
    if (error.response) {
      console.error('响应状态:', error.response.status);
      console.error('响应数据:', error.response.data);
      console.error('响应头:', error.response.headers);
    } else if (error.request) {
      console.error('请求配置:', error.request);
    }
    console.error('完整错误对象:', error);
    throw error;
  }
};

// 获取商家信息
export const getMerchantInfo = async (): Promise<MerchantInfo> => {
  try {
    console.log('正在请求商家信息...');
    const response = await api.get('/merchant/info');
    console.log('商家信息响应:', response.data);
    return response.data.data as MerchantInfo;
  } catch (error: any) {
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

// 订单相关类型（对齐数据库 FoodOrder）
export interface FoodOrder {
  orderId: number;        // FoodOrder.OrderID
  paymentTime: string;    // FoodOrder.PaymentTime (ISO string)
  remarks?: string;       // FoodOrder.Remarks
  customerId: number;     // FoodOrder.CustomerID
  cartId: number;         // FoodOrder.CartID
  storeId: number;        // FoodOrder.StoreID
  sellerId: number;       // FoodOrder.SellerID
  orderState: number;

  deliveryTaskId?: number | null;  // 新增
  deliveryStatus?: number | null;  // 新增
}

// 菜品相关类型（对齐数据库 Dish）
export interface Dish {
  dishId: number;         // Dish.DishID
  dishName: string;       // Dish.DishName
  price: number;          // Dish.Price (decimal)
  description: string;    // Dish.Description
  isSoldOut: number;      // Dish.IsSoldOut (0=售罄, 2=在售)
}

export interface NewDishData {
  dishName: string;
  price: string | number;
  description: string;
  isSoldOut?: number; // 默认 2 (在售)
}

// 聊天消息类型
// 购物车及条目（用于展示订单明细）
export interface CartItemDishRef {
  dishId: number;
  dishName: string;
  price: number;
  description: string;
  isSoldOut: number;
}

export interface ShoppingCartItem {
  itemId: number;       // ShoppingCartItem.ItemID
  quantity: number;     // ShoppingCartItem.Quantity
  totalPrice: number;   // ShoppingCartItem.TotalPrice
  dishId: number;       // ShoppingCartItem.DishID
  cartId: number;       // ShoppingCartItem.CartID
  dish?: CartItemDishRef; // 级联 Dish（便于前端展示，可选）
}

// 优惠券相关类型（对齐数据库 Coupon）
export interface Coupon {
  couponId: number;           // Coupon.CouponID
  minimumSpend: number;       // Coupon.MinimumSpend
  discountAmount: number;     // Coupon.DiscountAmount
  validFrom: string;          // Coupon.ValidFrom (ISO string)
  validTo: string;            // Coupon.ValidTo (ISO string)
  applicableStoreId: number;  // Coupon.ApplicableStoreID
  orderId?: number;           // Coupon.OrderID (可选，关联订单)
  sellerId: number;           // Coupon.SellerID
}

// 客户优惠券关联类型（对齐数据库 Customer_Coupon）
export interface CustomerCoupon {
  customerId: number;         // Customer_Coupon.CustomerID
  couponId: number;           // Customer_Coupon.CouponID
  couponQuantity: number;     // Customer_Coupon.CouponQuantity
}

// 订单优惠券信息（用于前端展示）
export interface OrderCouponInfo {
  couponId: number;
  couponName?: string;        // 前端展示用，可能需要从其他地方获取
  description?: string;       // 前端展示用
  discountType: 'percentage' | 'fixed';
  discountValue: number;
  validFrom: string;
  validTo: string;
  isUsed: boolean;
}

export interface MerchantInfo {
  username: string;
  sellerId: number;
}

// ==================== 订单管理接口 ====================

// 获取订单列表（FoodOrder）
export const getOrders = async (params?: { sellerId?: number; storeId?: number }) => {
  try {
    const response = await api.get('/orders', { params });
    // 统一映射为驼峰命名
    const list = (response.data || []).map((o: any) => ({
      orderId: o.OrderID ?? o.orderId,
      paymentTime: o.PaymentTime ?? o.paymentTime,
      remarks: o.Remarks ?? o.remarks,
      customerId: o.CustomerID ?? o.customerId,
      cartId: o.CartID ?? o.cartId,
      storeId: o.StoreID ?? o.storeId,
      sellerId: o.SellerID ?? o.sellerId,
      orderState: o.OrderState ?? o.orderState ?? 0,
      deliveryTaskId: o.DeliveryTaskId ?? o.deliveryTaskId ?? null,   // 新增
      deliveryStatus: o.DeliveryStatus ?? o.deliveryStatus ?? null,   // 新增
    })) as FoodOrder[];
    return list;
  } catch (error) {
    console.error('获取订单列表失败:', error);
    throw error;
  }
};

// 获取单个订单（FoodOrder）
export const getOrderById = async (orderId: number) => {
  try {
    const response = await api.get(`/orders/${orderId}`);
    const o = response.data;
    const mapped: FoodOrder = {
      orderId: o.OrderID ?? o.orderId,
      paymentTime: o.PaymentTime ?? o.paymentTime,
      remarks: o.Remarks ?? o.remarks,
      customerId: o.CustomerID ?? o.customerId,
      cartId: o.CartID ?? o.cartId,
      storeId: o.StoreID ?? o.storeId,
      sellerId: o.SellerID ?? o.sellerId,
      orderState: o.OrderState ?? o.orderState,
      deliveryTaskId: o.DeliveryTaskId ?? o.deliveryTaskId ?? null,   // 新增
      deliveryStatus: o.DeliveryStatus ?? o.deliveryStatus ?? null,   // 新增
    };
    return mapped;
  } catch (error) {
    console.error('获取订单详情失败:', error);
    throw error;
  }
};

// 注：数据库中 FoodOrder 未包含“状态”等字段，移除状态更新/发布配送相关接口

// 注：移除自动接单/模拟订单等与数据库不一致的接口

// ==================== 菜品管理接口 ====================

// 获取菜品列表（对齐 Dish）
export const getDishes = async () => {
  try {
    const response = await api.get('/dishes');
    const list = (response.data || []).map((d: any) => ({
      dishId: d.DishID ?? d.dishId,
      dishName: d.DishName ?? d.dishName,
      price: d.Price ?? d.price,
      description: d.Description ?? d.description,
      isSoldOut: d.IsSoldOut ?? d.isSoldOut,
    })) as Dish[];
    return list;
  } catch (error) {
    console.error('获取菜品列表失败:', error);
    throw error;
  }
};

// 创建菜品
export const createDish = async (dishData: NewDishData) => {
  try {
    const payload = {
      DishName: dishData.dishName,
      Price: Number(dishData.price),
      Description: dishData.description,
      IsSoldOut: dishData.isSoldOut ?? 2,
    };
    const response = await api.post('/dishes', payload);
    const d = response.data;
    const mapped: Dish = {
      dishId: d.DishID ?? d.dishId,
      dishName: d.DishName ?? d.dishName,
      price: d.Price ?? d.price,
      description: d.Description ?? d.description,
      isSoldOut: d.IsSoldOut ?? d.isSoldOut,
    };
    return mapped;
  } catch (error) {
    console.error('创建菜品失败:', error);
    throw error;
  }
};

// 更新菜品信息
export const updateDish = async (dishId: number, dishData: {
  dishName?: string;
  price?: number;
  description?: string;
  isSoldOut?: number;
}) => {
  try {
    const payload: any = {};
    if (dishData.dishName !== undefined) payload.DishName = dishData.dishName;
    if (dishData.price !== undefined) payload.Price = dishData.price;
    if (dishData.description !== undefined) payload.Description = dishData.description;
    if (dishData.isSoldOut !== undefined) payload.IsSoldOut = dishData.isSoldOut;
    const response = await api.patch(`/dishes/${dishId}`, payload);
    const d = response.data;
    const mapped: Dish = {
      dishId: d.DishID ?? d.dishId,
      dishName: d.DishName ?? d.dishName,
      price: d.Price ?? d.price,
      description: d.Description ?? d.description,
      isSoldOut: d.IsSoldOut ?? d.isSoldOut,
    };
    return mapped;
  } catch (error) {
    console.error('更新菜品信息失败:', error);
    throw error;
  }
};

// 切换菜品售罄状态（0=售罄, 2=在售）
export const toggleDishSoldOut = async (dishId: number, isSoldOut: number) => {
  try {
    const response = await api.patch(`/dishes/${dishId}/soldout`, { IsSoldOut: isSoldOut });
    return response.data;
  } catch (error) {
    console.error('切换菜品售罄状态失败:', error);
    throw error;
  }
};

// 获取购物车条目（含菜品）
export const getCartItems = async (cartId: number) => {
  try {
    const response = await api.get(`/carts/${cartId}/items`);
    const list = (response.data || []).map((it: any) => ({
      itemId: it.ItemID ?? it.itemId,
      quantity: it.Quantity ?? it.quantity,
      totalPrice: it.TotalPrice ?? it.totalPrice,
      dishId: it.DishID ?? it.dishId,
      cartId: it.CartID ?? it.cartId,
      dish: {
        dishId: it.Dish?.DishID ?? it.dish?.dishId,
        dishName: it.Dish?.DishName ?? it.dish?.dishName,
        price: it.Dish?.Price ?? it.dish?.price,
        description: it.Dish?.Description ?? it.dish?.description,
        isSoldOut: it.Dish?.IsSoldOut ?? it.dish?.isSoldOut,
      } as CartItemDishRef,
    })) as ShoppingCartItem[];
    return list;
  } catch (error) {
    console.error('获取购物车条目失败:', error);
    throw error;
  }
};

// 获取订单优惠券信息
export const getOrderCoupons = async (orderId: number) => {
  try {
    const response = await api.get(`/orders/${orderId}/coupons`);
    const list = (response.data || []).map((c: any) => ({
      couponId: c.CouponID ?? c.couponId,
      minimumSpend: c.MinimumSpend ?? c.minimumSpend,
      discountAmount: c.DiscountAmount ?? c.discountAmount,
      validFrom: c.ValidFrom ?? c.validFrom,
      validTo: c.ValidTo ?? c.validTo,
      applicableStoreId: c.ApplicableStoreID ?? c.applicableStoreId,
      orderId: c.OrderID ?? c.orderId,
      sellerId: c.SellerID ?? c.sellerId,
    })) as Coupon[];

    // 转换为前端展示格式
    return list.map(coupon => ({
      couponId: coupon.couponId,
      couponName: `优惠券${coupon.couponId}`, // 可以根据需要从其他地方获取名称
      description: `满${coupon.minimumSpend}减${coupon.discountAmount}元`,
      discountType: 'fixed' as const, // 根据数据库结构，这里都是固定金额折扣
      discountValue: coupon.discountAmount,
      validFrom: coupon.validFrom,
      validTo: coupon.validTo,
      isUsed: true, // 订单中的优惠券都是已使用的
    })) as OrderCouponInfo[];
  } catch (error) {
    console.error('获取订单优惠券信息失败:', error);
    throw error;
  }
};

// ==================== 配送任务与骑手信息 ====================

export interface DeliveryTask {
  taskId: number;                 // DeliveryTask.TaskID
  estimatedArrivalTime: string;   // EstimatedArrivalTime
  estimatedDeliveryTime: string;  // EstimatedDeliveryTime
  customerId: number;             // CustomerID
  storeId: number;                // StoreID
}

export interface PublishTask {
  sellerId: number;         // SellerID
  deliveryTaskId: number;   // DeliveryTaskID
  publishTime: string;      // PublishTime
}

export interface AcceptTask {
  courierId: number;        // CourierID
  deliveryTaskId: number;   // DeliveryTaskID
  acceptTime: string;       // AcceptTime
}

export interface CourierSummary {
  userId: number;           // Courier.UserID
  courierRegistrationTime?: string; // Courier.CourierRegistrationTime
  vehicleType: string;      // Courier.VehicleType
  reputationPoints: number; // Courier.ReputationPoints
  totalDeliveries: number;  // Courier.TotalDeliveries
  avgDeliveryTime: number;  // Courier.AvgDeliveryTime
  averageRating: number;    // Courier.AverageRating
  monthlySalary: number;    // Courier.MonthlySalary
  fullName?: string;        // 从User表关联获取
  phoneNumber?: number;     // 从User表关联获取
}

export interface OrderDeliveryInfo {
  deliveryTask?: DeliveryTask;
  publish?: PublishTask;
  accept?: AcceptTask;
  courier?: CourierSummary;
}

// 商家发布配送任务（基于订单）
export const publishDeliveryTaskForOrder = async (
  orderId: number,
  payload: { estimatedArrivalTime: string; estimatedDeliveryTime: string }
) => {
  try {
    const response = await api.post(`/delivery-tasks/publish`, {
      OrderID: orderId,
      EstimatedArrivalTime: payload.estimatedArrivalTime,
      EstimatedDeliveryTime: payload.estimatedDeliveryTime,
    });
    const data = response.data || {};
    // 预计返回包含 DeliveryTask 
    const mapDelivery = (dt: any): DeliveryTask | undefined =>
      dt
        ? {
          taskId: dt.TaskID ?? dt.taskId,
          estimatedArrivalTime: dt.EstimatedArrivalTime ?? dt.estimatedArrivalTime,
          estimatedDeliveryTime: dt.EstimatedDeliveryTime ?? dt.estimatedDeliveryTime,
          customerId: dt.CustomerID ?? dt.customerId,
          storeId: dt.StoreID ?? dt.storeId,
        }
        : undefined;
    const mapPublish = (pt: any): PublishTask | undefined =>
      pt
        ? {
          sellerId: pt.SellerID ?? pt.sellerId,
          deliveryTaskId: pt.DeliveryTaskID ?? pt.deliveryTaskId,
          publishTime: pt.PublishTime ?? pt.publishTime,
        }
        : undefined;
    return {
      deliveryTask: mapDelivery(data.DeliveryTask ?? data.deliveryTask),
      publish: mapPublish(data.Publish ?? data.publish),
    } as { deliveryTask?: DeliveryTask; publish?: PublishTask };
  } catch (error) {
    console.error('发布配送任务失败:', error);
    throw error;
  }
};

// 获取订单对应的配送任务与骑手信息
export const getOrderDeliveryInfo = async (orderId: number) => {
  try {
    const response = await api.get(`/orders/${orderId}/delivery-info`);
    const data = response.data || {};
    const deliveryTask: DeliveryTask | undefined = data.DeliveryTask || data.deliveryTask
      ? {
        taskId: (data.DeliveryTask || data.deliveryTask).TaskID ?? (data.DeliveryTask || data.deliveryTask).taskId,
        estimatedArrivalTime: (data.DeliveryTask || data.deliveryTask).EstimatedArrivalTime ?? (data.DeliveryTask || data.deliveryTask).estimatedArrivalTime,
        estimatedDeliveryTime: (data.DeliveryTask || data.deliveryTask).EstimatedDeliveryTime ?? (data.DeliveryTask || data.deliveryTask).estimatedDeliveryTime,
        customerId: (data.DeliveryTask || data.deliveryTask).CustomerID ?? (data.DeliveryTask || data.deliveryTask).customerId,
        storeId: (data.DeliveryTask || data.deliveryTask).StoreID ?? (data.DeliveryTask || data.deliveryTask).storeId,
      }
      : undefined;
    const publish: PublishTask | undefined = data.Publish || data.publish
      ? {
        sellerId: (data.Publish || data.publish).SellerID ?? (data.Publish || data.publish).sellerId,
        deliveryTaskId: (data.Publish || data.publish).DeliveryTaskID ?? (data.Publish || data.publish).deliveryTaskId,
        publishTime: (data.Publish || data.publish).PublishTime ?? (data.Publish || data.publish).publishTime,
      }
      : undefined;
    const accept: AcceptTask | undefined = data.Accept || data.accept
      ? {
        courierId: (data.Accept || data.accept).CourierID ?? (data.Accept || data.accept).courierId,
        deliveryTaskId: (data.Accept || data.accept).DeliveryTaskID ?? (data.Accept || data.accept).deliveryTaskId,
        acceptTime: (data.Accept || data.accept).AcceptTime ?? (data.Accept || data.accept).acceptTime,
      }
      : undefined;
    const courier: CourierSummary | undefined = data.Courier || data.courier
      ? {
        userId: (data.Courier || data.courier).UserID ?? (data.Courier || data.courier).userId,
        courierRegistrationTime: (data.Courier || data.courier).CourierRegistrationTime ?? (data.Courier || data.courier).courierRegistrationTime,
        vehicleType: (data.Courier || data.courier).VehicleType ?? (data.Courier || data.courier).vehicleType,
        reputationPoints: (data.Courier || data.courier).ReputationPoints ?? (data.Courier || data.courier).reputationPoints,
        totalDeliveries: (data.Courier || data.courier).TotalDeliveries ?? (data.Courier || data.courier).totalDeliveries,
        avgDeliveryTime: (data.Courier || data.courier).AvgDeliveryTime ?? (data.Courier || data.courier).avgDeliveryTime,
        averageRating: (data.Courier || data.courier).AverageRating ?? (data.Courier || data.courier).averageRating,
        monthlySalary: (data.Courier || data.courier).MonthlySalary ?? (data.Courier || data.courier).monthlySalary,
        fullName: (data.Courier || data.courier).FullName ?? (data.Courier || data.courier).fullName,
        phoneNumber: (data.Courier || data.courier).PhoneNumber ?? (data.Courier || data.courier).phoneNumber,
      }
      : undefined;
    return { deliveryTask, publish, accept, courier } as OrderDeliveryInfo;
  } catch (error) {
    console.error('获取订单配送信息失败:', error);
    throw error;
  }
};

// ==================== 商家接单/拒单 ====================

export interface OrderDecision {
  orderId: number;
  decision: 'accepted' | 'rejected' | 'ready';
  decidedAt: string;
  reason?: string;
}

// 商家接单
export const acceptOrder = async (orderId: number) => {
  try {
    const res = await api.post(`/orders/${orderId}/accept`);
    const d = res.data || {};
    const mapped: OrderDecision = {
      orderId: d.OrderID ?? d.orderId ?? orderId,
      decision: 'accepted',
      decidedAt: d.DecidedAt ?? d.decidedAt ?? new Date().toISOString(),
    };
    return mapped;
  } catch (error) {
    console.error('商家接单失败:', error);
    throw error;
  }
};

export const markAsReadyApi = async (orderId: number) => {
  try {
    const res = await api.post(`/orders/${orderId}/ready`);
    const d = res.data || {};
    const mapped: OrderDecision = {
      orderId: d.OrderID ?? d.orderId ?? orderId,
      decision: 'ready',
      decidedAt: d.DecidedAt ?? d.decidedAt ?? new Date().toISOString(),
    };
    return mapped;
  } catch (error) {
    console.error('标记订单出餐失败:', error);
    throw error;
  }
};

// 商家拒单
export const rejectOrder = async (orderId: number, reason?: string) => {
  try {
    const res = await api.post(`/orders/${orderId}/reject`, { reason });
    const d = res.data || {};
    const mapped: OrderDecision = {
      orderId: d.OrderID ?? d.orderId ?? orderId,
      decision: 'rejected',
      decidedAt: d.DecidedAt ?? d.decidedAt ?? new Date().toISOString(),
      reason: d.Reason ?? d.reason ?? reason,
    };
    return mapped;
  } catch (error) {
    console.error('商家拒单失败:', error);
    throw error;
  }
};

// 注：数据库未定义菜品“分类/图片”，移除相关接口

// 注：数据库未定义聊天/拨号相关实体，移除相关接口



// 注：数据库未定义菜品图片，移除图片上传接口
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
// ==================== 售后与评论管理接口 ====================

// 根据数据库设计修改接口
export interface Review {
  id: number;                    // CommentID
  orderNo: string;              // 从OrderID关联获取
  user: { name: string; phone: string; avatar?: string }; // 从CommenterID关联User表获取
  content: string;              // Content
  createdAt: string;            // PostedAt
}

export interface AfterSaleApplication {
  id: number;                   // ApplicationID
  orderNo: string;              // 从OrderID关联获取
  user: { name: string; phone: string; avatar?: string }; // 从CustomerID关联User表获取
  reason: string;               // Description
  createdAt: string;            // ApplicationTime
}

export interface PageResult<T> {
  list: T[];
  total: number;
}

export interface AfterSaleListParams {
  page: number;
  pageSize: number;
  keyword?: string;
}

// 获取售后申请列表（分页，带筛选）
export const getAfterSaleList = async (
  params: AfterSaleListParams
): Promise<PageResult<AfterSaleApplication>> => {
  try {
    const response = await api.get('/aftersale', {
      params: {
        page: params.page.toString(),
        pageSize: params.pageSize.toString(),
        ...(params.keyword && { keyword: params.keyword })
      }
    });
    
    return response.data;
  } catch (error) {
    console.error('获取售后申请列表失败:', error);
    throw error;
  }
};

// 获取售后申请详情
export const getAfterSaleDetail = async (id: number): Promise<AfterSaleApplication> => {
  try {
    const response = await api.get(`/aftersale/${id}`);
    
    return response.data;
  } catch (error) {
    console.error('获取售后申请详情失败:', error);
    throw error;
  }
};

// 处理售后申请
export const decideAfterSale = async (
  id: number,
  action: 'approve' | 'reject' | 'negotiate',
  data: { remark: string }
) => {
  try {
    const response = await api.post(`/aftersale/${id}/decide`, {
      action,
      ...data
    });
    
    return response.data;
  } catch (error) {
    console.error('处理售后申请失败:', error);
    throw error;
  }
};

// 获取评论列表
export const getReviewList = async (params: any): Promise<PageResult<Review>> => {
  try {
    const response = await api.get('/reviews', {
      params: {
        page: params.page.toString(),
        pageSize: params.pageSize.toString(),
        ...(params.keyword && { keyword: params.keyword })
      }
    });
    
    return response.data;
  } catch (error) {
    console.error('获取评论列表失败:', error);
    throw error;
  }
};

// 回复评论
export const replyReview = async (id: number, content: string) => {
  try {
    const response = await api.post(`/reviews/${id}/reply`, {
      content
    });
    
    return response.data;
  } catch (error) {
    console.error('回复评论失败:', error);
    throw error;
  }
};

// 已移除评论申诉相关接口

// ==================== 处罚记录相关接口 ====================
export interface PenaltyRecord {
  id: string;                   // PenaltyID
  reason: string;               // PenaltyReason
  time: string;                 // PenaltyTime
  merchantAction: string;       // SellerPenalty
  platformAction: string;       // StorePenalty
}

// 获取处罚记录列表
export const getPenaltyList = async (params?: {
  keyword?: string;
}) => {
  try {
    const response = await api.get('/penalties', {
      params: {
        ...(params?.keyword && { keyword: params.keyword })
      }
    });
    
    return response.data;
  } catch (error) {
    console.error('获取处罚记录失败:', error);
    throw error;
  }
};

// 获取处罚详情
export const getPenaltyDetail = async (id: string) => {
  try {
    const response = await api.get(`/penalties/${id}`);
    
    return response.data;
  } catch (error) {
    console.error('获取处罚详情失败:', error);
    throw error;
  }
};

// 提交处罚申诉
export const appealPenalty = async (id: string, reason: string) => {
  try {
    const response = await api.post(`/penalties/${id}/appeal`, {
      reason
    });
    
    return response.data;
  } catch (error) {
    console.error('申诉处罚失败:', error);
    throw error;
  }
};

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
    const token = localStorage.getItem('authToken');
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

// 添加缺失的接口
export interface ChatMessage {
  sender: 'user' | 'merchant';
  content: string;
  time: string;
}
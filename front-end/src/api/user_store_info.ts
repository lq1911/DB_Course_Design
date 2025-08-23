import { getData } from '@/api/multiuse_function'

// 店家信息
export interface StoreInfo {
  id: number
  name: string
  image: string
  address: string
  businessHours: string
  rating: number
  monthlySales: number
  description: string
  createTime: string
}

// 外送任务
export interface DeliveryTask {
  id: number
  deliveryTime: number
  deliveryFee: number
}

// 评价
export interface Comment {
  id: number;
  username: string;
  rating: number;
  date: string;
  content: string;
  avatar: string;
  images: string[]
}

// 店铺的评论数组
export interface CommentList {
  comments: Comment[];
}

// 综合评价
export interface CommentStatus {
  status: number[];
}

export async function getStoreInfo(StoreId: string): Promise<StoreInfo> {
  return getData<StoreInfo>(`/api/user/store/storeInfo`, {
    params: {
      storeId: StoreId
    }
  });
}

export function getDeliveryTasks() {
  const deliveryTime = Math.floor(Math.random() * 51) + 20; // 20 ~ 70 分钟
  const deliveryFee = parseFloat((deliveryTime * 0.5 + 5).toFixed(2)); // 时间越长费越高，基础5元

  return {
    id: Math.floor(Math.random() * 10000),
    deliveryTime,
    deliveryFee,
  };
}

export async function getCommentList(StoreId: string) {
  return getData<CommentList>("/api/user/store/commentList", {
    params: {
      storeId: StoreId
    }
  });
}

export async function getCommentStatus(StoreId: string): Promise<CommentStatus> {
  return getData<CommentStatus>("/api/user/store/commentStatus", {
    params: {
      storeId: StoreId
    }
  });
}
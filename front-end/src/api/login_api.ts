// src/api/auth.api.ts
import apiClient from './client'; // 导入我们共享的客户端

// --- 接口定义 (Interfaces) ---
// 登录数据接口
interface UserData {
  password: string;
  phoneNum: string;
  role: string;
}

// 注册时基础数据接口
interface RegistrationData {
  nickname: string;
  password: string;
  phone: string;
  email: string;
  gender: string;
  birthday: string;
  verificationCode: string;
  role: string;
}


// 更新骑手特定信息
interface RiderInfo {
  vehicleType: string;
}

// 更新管理员特定信息
interface AdminInfo {
  managementObject: string;
  handledItems: string;
}

// 更新店铺/商家特定信息
interface StoreInfo {
  name: string;
  address: string;
  businessHours: string;
  establishmentDate: string;
  category: string;
}




export default {
  /**
   * 注册接口 (公开访问)
   */
  register(data: RegistrationData | FormData) {
    return apiClient.post('/register', data);
  },

  /**
   * 登录接口 (公开访问)
   */
  login(data: UserData) {
    return apiClient.post('/login', data);
  }
};
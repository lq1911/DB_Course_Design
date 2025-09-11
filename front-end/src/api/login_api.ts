// src/api/login_api.ts
import apiClient from './client';

// ... (你已有的所有 interface 定义保持不变) ...
interface UserData { /* ... */ }
interface RegistrationData { /* ... */ }
// ...

export default {
  register(data: RegistrationData | FormData) {
    return apiClient.post('/register', data);
  },

  login(data: UserData) {
    return apiClient.post('/login', data);
  },

  /**
   * 登出接口 (需要认证)
   */
  logout() {
    // 路径与后端 LoginController 中的 [HttpPost("logout")] 匹配
    return apiClient.post('/login/logout');
  }
};
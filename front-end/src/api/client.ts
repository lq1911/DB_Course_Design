// src/api/client.ts
import axios from 'axios';

// --- Axios 实例和拦截器 ---
const apiClient = axios.create({
  baseURL: 'http://localhost:5250/api', 
  timeout: 10000,
});

/**
 * 请求拦截器: 在每个请求发送前自动添加 Token
 */
apiClient.interceptors.request.use(config => {
  const token = localStorage.getItem('authToken');
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
}, error => {
  return Promise.reject(error);
});

// 只导出这个配置好的客户端
export default apiClient;
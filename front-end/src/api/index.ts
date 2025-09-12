import axios, { AxiosInstance } from 'axios'

// 创建实例
const API: AxiosInstance = axios.create({
    baseURL: 'http://localhost:5250',  // 本地后端地址 
    // baseURL: 'http://113.44.82.210:5250',  // 服务器后端地址
    timeout: 10000,
    headers: {
        'Content-Type': 'application/json'
    }
})

// 请求拦截器
API.interceptors.request.use(config => {
    const token = localStorage.getItem('authToken')
    if (token && config.headers) {
        config.headers.Authorization = `Bearer ${token}`
    }
    return config
})

// 响应拦截器
API.interceptors.response.use(
  response => response,
  error => {
    if (axios.isAxiosError(error)) {
      console.error(`全局请求错误: ${error.response?.status} ${error.message}`)
      // 这里可以统一处理 401/403，弹窗提示等
    } else {
      console.error('全局请求未知错误:', error)
    }
    return Promise.reject(error) // 仍然抛给调用函数处理
  }
)

export default API

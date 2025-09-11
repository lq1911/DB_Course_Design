// src/utils/jwt.ts
import { jwtDecode } from 'jwt-decode';
import apiClient from '@/api/client'; // 导入 apiClient 以便更新其头部

// --- 常量 ---
const TOKEN_KEY = 'authToken'; // 定义一个统一的 key 用于 localStorage

// --- JWT 载荷接口 ---
interface JwtPayload {
  nameid: string;      // 用户ID
  unique_name: string; // 用户名
  role: string;        // 角色
  phone: string;       // 手机号
  exp: number;         // 过期时间
  iat: number;         // 签发时间
}

// ================================================================
//  新增的 Token 管理函数
// ================================================================

/**
 * 将 JWT Token 保存到 localStorage，并设置 apiClient 的默认请求头。
 * @param token JWT 字符串
 */
export function setToken(token: string): void {
  localStorage.setItem(TOKEN_KEY, token);
  apiClient.defaults.headers.common['Authorization'] = `Bearer ${token}`;
  console.log('Token 已保存，API Client 请求头已更新。');
}

/**
 * 从 localStorage 中获取 Token。
 * @returns JWT 字符串或 null
 */
export function getToken(): string | null {
  return localStorage.getItem(TOKEN_KEY);
}

/**
 * 从 localStorage 中移除 Token，并清除 apiClient 的默认请求头 (登出核心操作)。
 */
export function removeToken(): void {
  localStorage.removeItem(TOKEN_KEY);
  delete apiClient.defaults.headers.common['Authorization'];
  console.log('Token 已移除，API Client 请求头已清除。');
}

// ================================================================
//  您已有的 Token 解析函数 (保持不变)
// ================================================================

/**
 * 从 Token 中解析出用户 ID。
 * @param token JWT 字符串
 * @returns 用户 ID 或 null
 */
export function getUserIdFromToken(token: string): string | null {
  try {
    const decoded = jwtDecode<JwtPayload>(token);
    return decoded.nameid;
  } catch (error) {
    console.error('解析token失败:', error);
    return null;
  }
}

/**
 * 从 Token 中解析出完整的用户信息。
 * @param token JWT 字符串
 * @returns 包含用户信息所需字段的对象，或 null
 */
export function getUserInfoFromToken(token: string) {
  try {
    const decoded = jwtDecode<JwtPayload>(token);
    return {
      userId: decoded.nameid,
      username: decoded.unique_name,
      role: decoded.role,
      phone: decoded.phone
    };
  } catch (error) {
    console.error('解析token失败:', error);
    return null;
  }
}
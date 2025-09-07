// src/utils/jwt.ts
import { jwtDecode } from 'jwt-decode';

interface JwtPayload {
  nameid: string; // ClaimTypes.NameIdentifier 在JWT中对应 "nameid"
  unique_name: string; // ClaimTypes.Name 对应 "unique_name"
  role: string; // ClaimTypes.Role 对应 "role"
  phone: string;
  exp: number;
  iat: number;
}

export function getUserIdFromToken(token: string): string | null {
  try {
    const decoded = jwtDecode<JwtPayload>(token);
    return decoded.nameid; // 这就是你的UserID
  } catch (error) {
    console.error('解析token失败:', error);
    return null;
  }
}

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

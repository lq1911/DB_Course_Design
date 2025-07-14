# 用户登录接口

- **接口名称**: LogIn (文档名为LogIn.md)
- **接口描述**: 用户输入用户名和密码后登录系统，返回 token 和用户信息
- **接口地址(URL)**: `/api/user/login`
- **请求方式**: `POST`

---

## 请求体参数

| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `username` | string | 是 | 用户名 |
| `password` | string | 是 | 登录密码（明文或加密，按协议） |

## 请求示例

```json
{
  "username": "zhangsan",
  "password": "123456"
}
```

## 返回结果

```json
{
  "code": 200,
  "message": "登录成功",
  "data": {
    "user_id": 1001,
    "username": "zhangsan",
    "token": "eyJhbGciOiJIUzI1NiIs..."
  }
}
```

## 响应说明

| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `code` | int | 是 | 状态码，200 为成功，401 为登录失败 |
| `message` | string | 是 | 接口返回说明信息 |
| `data.user_id` | int | 是 | 用户 ID |
| `data.username` | string | 是 | 用户名 |
| `data.token` | string | 是 | 登录成功后返回的身份令牌，用于后续鉴权 |
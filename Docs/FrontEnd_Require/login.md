# 🚪 登录/注册页 前端说明文档 (V2)

## 📄 一. 页面描述

### 功能
提供系统统一的登录和多角色注册入口。注册流程现在分为两步：  
首先创建基础账户，然后在登录后完善个人资料。  

### 路径
/login (由路由配置决定)


### 文件
src/views/login/LoginView.vue

---

## 🎯 二. 核心功能点

- 🔄 **表单切换**: 支持在“登录”和“注册”两个表单之间切换。  
- 👥 **角色系统**: 支持“管理员”、“商家”、“骑手”、“消费者”四种角色的登录和注册。  
- 📝 **动态表单**: 注册表单只包含所有角色通用的基础字段。角色专属信息（如店铺名称、配送工具）将在用户首次登录后引导其在个人资料页填写。  
- ⏳ **异步处理**: 登录和注册期间，提交按钮会显示加载状态并被禁用。  
- 💬 **响应处理**: 操作成功或失败时，会通过 alert 弹出后端返回的信息。  
- 🗝️ **Token 存储与管理**:  
  - 存储: 登录成功后，将后端返回的 token 存入 localStorage。  
  - 自动附加: Axios 拦截器会在所有后续 API 请求（除登录/注册外）自动附带 `Authorization: Bearer <token>`。  
- 📜 **协议弹窗**: 注册时，用户协议和隐私政策通过弹窗展示。  

---

## 🔌 三. 调用接口

所有接口通过 `src/services/api.ts` 模块发起。

### 🌐 接口调用说明
| 配置项 | 说明 |
|--------|------|
| 请求基础路径 | `http://localhost:3000/api` |
| 认证机制 | api.ts 中已配置请求拦截器，登录后自动携带 Token |

### 1. 🏫 用户登录接口 (公开)

| 配置项 | 说明 |
|--------|------|
| 调用方法 | `api.login(data)` |
| 请求路径 | `POST /login` |
| 接口说明 | 接收用户凭证，验证成功后返回身份令牌和用户信息 |

#### 请求参数
| 字段名 | 类型 | 来源 | 示例值 |
|--------|------|------|---------|
| account | string | loginForm.account | "user@example.com" |
| password | string | loginForm.password | "123456" |
| role | string | selectedRole.value | "consumer" |

#### 响应数据
##### 成功响应 (HTTP 200)
```json
{
  "token": "a.jwt.token.string",
  "user": { "...": "..." },
  "message": "登录成功！"
}
```

##### 失败响应 (HTTP 4xx/5xx)
```json
{
  "error": "账号或密码错误"
}
```
2. 📝 用户基础注册接口 (公开)
## 注册接口

### 基本信息
- **调用方法**: `api.register(data)`
- **请求路径**: `POST /register`
- **接口说明**: 创建新用户账户的基础信息注册接口

### 请求参数
| 字段名 | 类型 | 来源 | 示例值 | 说明 |
|--------|------|------|---------|------|
| nickname | string | registerForm.nickname | "新用户123" | 用户昵称 |
| password | string | registerForm.password | "mysecretpwd" | 密码 |
| phone | string | registerForm.phone | "13800138000" | 手机号 |
| email | string | registerForm.email | "new@example.com" | 邮箱 |
| gender | string | registerForm.gender | "female" | 性别 |
| birthday | string | registerForm.birthday | "2000-01-01" | 生日 |
| verificationCode | string | registerForm.verificationCode | "123456" | 验证码 |
| role | string | selectedRole.value | "merchant" | 用户角色 |

### 响应数据
#### 成功响应 (HTTP 201)
```json
{
  "code": 201,
  "message": "注册成功！请登录后完善您的个人资料。"
}
```

#### 失败响应 (HTTP 4xx/5xx)
```json
{
  "code": 409,
  "error": "该手机号已被注册。"
}
```
3. 👤 用户资料更新接口 (需要认证)

> 以下接口用于用户在登录后完善个人信息。所有请求都会自动携带 Token。

### 3.1 更新通用资料

| 配置项 | 说明 |
|--------|------|
| 调用方法 | `api.updateUserProfile(profileData)` |
| 请求路径 | `PUT /user/profile` |

#### 请求参数
| 字段名 | 类型 | 示例值 | 说明 |
|--------|------|---------|------|
| realName | string | "张三" | 用户真实姓名 |

### 3.2 更新骑手专属资料

| 配置项 | 说明 |
|--------|------|
| 调用方法 | `api.updateRiderInfo(riderData)` |
| 请求路径 | `PUT /user/profile/rider` |

#### 请求参数
| 字段名 | 类型 | 示例值 | 说明 |
|--------|------|---------|------|
| vehicleType | string | "电动车" | 配送车辆类型 |

### 3.3 更新管理员专属资料

| 配置项 | 说明 |
|--------|------|
| 调用方法 | `api.updateAdminInfo(adminData)` |
| 请求路径 | `PUT /user/profile/admin` |

#### 请求参数
| 字段名 | 类型 | 示例值 | 说明 |
|--------|------|---------|------|
| managementObject | string | "商家审核" | 管理对象 |
| handledItems | string | "处理新商家入驻" | 处理事项 |

### 3.4 更新商家专属资料

| 配置项 | 说明 |
|--------|------|
| 调用方法 | `api.updateStoreInfo(storeData)` |
| 请求路径 | `PUT /user/profile/merchant` |

#### 请求参数
| 字段名 | 类型 | 示例值 | 说明 |
|--------|------|---------|------|
| name | string | "我的美味小店" | 店铺名称 |
| address | string | "XX市XX区XX街道123号" | 店铺地址 |
| businessHours | string | "09:00-21:00" | 营业时间 |
| establishmentDate | string | "2023-10-26" | 成立日期 |
| businessLicense | File/null | (文件对象) | 营业执照 |
| category | string | "中式快餐" | 店铺类别 |

### 通用响应格式

#### 成功响应 (HTTP 200)
```json
{
  "message": "资料更新成功！"
}
```

#### 失败响应 (HTTP 4xx/5xx)
```json
{
  "error": "更新失败，请稍后再试。"
}
```
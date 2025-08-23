# 获取骑手个人资料接口

## 接口名称
`GetCourierProfile`

## 接口描述
获取当前登录骑手的详细个人资料，用于"我的"页面等处的个人信息展示。

## 接口地址(URL)
`/api/courier/profile`

## 请求方式
`GET`

## 请求参数
无

## 返回结果

### 成功响应示例
```json
{
  "name": "张三",
  "id": "1",
  "registerDate": "2023-10-28",
  "rating": 4.85,
  "creditScore": 120
}
```

### 失败响应示例（骑手不存在）
```
HTTP/1.1 404 Not Found
Content-Type: application/problem+json; charset=utf-8

{
    "type": "https://tools.ietf.org/html/rfc7231#section-6.5.4",
    "title": "Not Found",
    "status": 404,
    "traceId": "...",
    "errors": {
        "": [
            "骑手资料未找到"
        ]
    }
}
```
注: 404 响应格式可能因 ASP.NET Core 版本和配置而略有不同，但状态码是关键

### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| name | string | 是 | 骑手的姓名 |
| id | string | 是 | 骑手的唯一用户ID |
| registerDate | string | 是 | 骑手的注册日期 (格式: YYYY-MM-DD) |
| rating | number | 是 | 骑手获得的平均评分 |
| creditScore | number | 是 | 骑手的信誉积分 |

# 切换骑手工作状态接口

## 接口名称
`ToggleCourierStatus`

## 接口描述
切换当前登录骑手的工作状态，用于处理"开工"和"收工"操作。

## 接口地址(URL)
`/api/courier/status/toggle`

## 请求方式
`POST`

## 请求参数

### 请求体 (Request Body)
```json
{
  "isOnline": true
}
```

### 参数说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| isOnline | boolean | 是 | 目标工作状态。true 表示切换为"在线"（开工），false 表示切换为"离线"（收工）。 |

## 返回结果

### 成功响应示例
```json
{
  "code": 200,
  "message": "状态更新成功",
  "data": {
    "success": true
  }
}
```

### 失败响应示例（骑手不存在）
```json
{
  "code": 404,
  "message": "骑手不存在，无法更新状态"
}
```

### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| code | int | 是 | 状态码，200 为成功，401 为未授权，404 为资源未找到 |
| message | string | 是 | 接口返回说明信息 |
| data.success | boolean | 是 | 指示操作是否成功执行，成功时返回 true |

# 获取骑手当前工作状态接口

## 接口名称
`GetCourierStatus`

## 接口描述
获取当前登录骑手的在线状态，用于判断骑手是否处于"开工"状态。

## 接口地址(URL)
`/api/courier/status`

## 请求方式
`GET`

## 请求参数
无

## 返回结果

### 成功响应示例
```json
{
  "code": 200,
  "message": "获取成功",
  "data": {
    "isOnline": true
  }
}
```

### 失败响应示例（骑手不存在）
```json
{
  "code": 404,
  "message": "骑手资料未找到，无法获取状态"
}
```

### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| code | int | 是 | 状态码，200 为成功，401 为未授权，404 为资源未找到 |
| message | string | 是 | 接口返回说明信息 |
| data.isOnline | boolean | 是 | 骑手的在线状态。true 表示在线，false 表示离线 |

# 获取骑手当前位置接口

## 接口名称
`GetCourierLocation`

## 接口描述
获取当前登录骑手的实时位置文本描述（当前为模拟数据）。

## 接口地址(URL)
`/api/courier/location`

## 请求方式
`GET`

## 请求参数
无

## 返回结果

### 成功响应示例
```json
{
  "code": 200,
  "message": "获取成功",
  "data": {
    "area": "模拟位置 (经度: 116.308420, 纬度: 39.983670)"
  }
}
```

### 失败响应示例（骑手不存在或无坐标）
```json
{
  "code": 200,
  "message": "获取成功",
  "data": {
    "area": "位置信息未提供"
  }
}
```

### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| code | int | 是 | 状态码，200 为成功，401 为未授权 |
| message | string | 是 | 接口返回说明信息 |
| data.area | string | 是 | 骑手当前的位置文本描述 |

# 获取骑手订单列表接口

## 接口名称
`GetCourierOrders`

## 接口描述
获取当前登录骑手的订单列表，可以通过状态进行筛选。

## 接口地址(URL)
`/api/courier/orders`

## 请求方式
`GET`

## 请求参数

### 查询参数 (Query Parameters)
示例 URL: `/api/courier/orders?status=delivering`

| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| status | string | 是 | 订单状态。可选值为 pending, delivering, completed。 |

## 返回结果

### 成功响应示例
```json
[
  {
    "id": "101",
    "status": "delivering",
    "restaurant": "麦当劳（中关村店）",
    "address": "海淀区中关村大街1号",
    "fee": "5.00",
    "statusText": "配送中"
  },
  {
    "id": "98",
    "status": "delivering",
    "restaurant": "肯德基（五道口店）",
    "address": "海淀区成府路23号",
    "fee": "6.50",
    "statusText": "配送中"
  }
]
```

### 成功响应示例（无匹配订单）
当没有符合条件的订单时，返回一个空的数组。
```json
[]
```

### 响应说明
响应体是一个 JSON 数组，数组中的每个对象都代表一个订单，其字段说明如下：

| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| id | string | 是 | 订单的唯一ID |
| status | string | 是 | 订单的英文状态 (pending, delivering, completed) |
| restaurant | string | 是 | 订单关联的商家名称 |
| address | string | 是 | 顾客的收货地址 |
| fee | string | 是 | 该订单的配送费，格式化为保留两位的字符串 |
| statusText | string | 是 | 订单状态对应的中文描述 |

# 获取新订单详情接口

## 接口名称
`GetNewOrderDetails`

## 接口描述
根据新订单的推送通知ID，获取该订单的完整详细信息，用于在新订单弹窗中展示。

## 接口地址(URL)
`/api/courier/orders/new/{notificationId}`

## 请求方式
`GET`

## 请求参数

### 路径参数 (URL Parameter)
示例 URL: `/api/courier/orders/new/101`

| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| notificationId | string | 是 | 新订单的推送唯一ID（通常即订单任务的ID）。 |

## 返回结果

### 成功响应示例
```json
{
  "id": "101",
  "restaurantName": "麦当劳（中关村店）",
  "restaurantAddress": "北京市海淀区中关村大街1号",
  "customerName": "王女士",
  "customerAddress": "海淀区中关村大街1号",
  "distance": "约 2.5 公里",
  "fee": 5.00,
  "mapImageUrl": "https://your-placeholder-domain.com/static-map.png"
}
```

### 失败响应示例（订单不存在）
```
HTTP/1.1 404 Not Found

"找不到ID为 9999 的订单详情。"
```

### 响应说明
成功时，响应体是一个直接包含订单详情的 JSON 对象，其字段说明如下：

| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| id | string | 是 | 新订单的唯一ID |
| restaurantName | string | 是 | 商家（餐厅）的名称 |
| restaurantAddress | string | 是 | 商家（餐厅）的地址 |
| customerName | string | 是 | 顾客的姓名 |
| customerAddress | string | 是 | 顾客的收货地址 |
| distance | string | 是 | 商家与顾客之间的配送距离（当前为模拟数据）|
| fee | number | 是 | 该订单的配送费 |
| mapImageUrl | string | 是 | 用于展示路线的静态地图图片URL（当前为模拟数据）|

# 接受订单接口

## 接口名称
`AcceptOrder`

## 接口描述
骑手接受一个处于"待处理"状态的新订单任务。

## 接口地址(URL)
`/api/courier/orders/{orderId}/accept`

## 请求方式
`POST`

## 请求参数

### 路径参数 (URL Parameter)
示例 URL: `/api/courier/orders/101/accept`

| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| orderId | string | 是 | 要接受的订单任务的唯一ID。 |

### 请求体 (Request Body)
请求体: 无

## 返回结果

### 成功响应示例
```json
{
  "success": true
}
```

### 失败响应示例（订单不存在或状态不正确）
```json
{
  "success": false,
  "message": "无法接受该订单，它可能已被处理或不存在。"
}
```

### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| success | boolean | 是 | 指示操作是否成功执行。true 表示订单已成功接受，false 表示操作失败。 |
| message | string | 否 | 当 success 为 false 时，可能附带的错误说明信息。 |

注：成功响应的状态码为 200 OK，失败响应（如订单状态不正确）的状态码为 400 Bad Request

# 拒绝订单接口

## 接口名称
`RejectOrder`

## 接口描述
拒绝一个处于"待处理"状态的新订单任务，通常会将其状态更新为"已取消"。

## 接口地址(URL)
`/api/courier/orders/{orderId}/reject`

## 请求方式
`POST`

## 请求参数

### 路径参数 (URL Parameter)
示例 URL: `/api/courier/orders/102/reject`

| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| orderId | string | 是 | 要拒绝的订单任务的唯一ID。 |

### 请求体 (Request Body)
请求体: 无

## 返回结果

### 成功响应示例
```json
{
  "success": true
}
```

### 失败响应示例（订单不存在或状态不正确）
```json
{
  "success": false,
  "message": "无法拒绝该订单，它可能已被处理或不存在。"
}
```

### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| success | boolean | 是 | 指示操作是否成功执行。true 表示订单已成功拒绝，false 表示操作失败。 |
| message | string | 否 | 当 success 为 false 时，可能附带的错误说明信息。 |

注：成功响应的状态码为 200 OK，失败响应（如订单状态不正确）的状态码为 400 Bad Request

# 获取骑手当月收入接口

## 接口名称
`GetCourierMonthlyIncome`

## 接口描述
获取当前登录骑手的当月总收入（当前为模拟数据）。

## 接口地址(URL)
`/api/courier/income/monthly`

## 请求方式
`GET`

## 请求参数
无

## 返回结果

### 成功响应示例
```
7854.21
```

### 响应体 (Response Body)
Content-Type: text/plain

内容: 响应体直接是一个纯数字（以字符串形式表示），代表骑手的当月模拟收入。这个数字是 4000 到 9000 之间的一个随机数，保留两位小数。

### 失败响应示例（未授权）
```
HTTP/1.1 401 Unauthorized
```

### 响应说明
成功时，此接口返回 200 OK 状态码，响应体为纯数字文本。
如果请求未提供有效的认证 Token，将返回 401 Unauthorized 状态码。

# 骑手端 API 功能实现状态总结

最后更新日期: 2025-8-18

## 文档目的
本文档旨在清晰、准确地记录骑手端所有后端 API 的当前实现状态，明确区分已完全实现的功能和当前使用模拟数据的功能，为后续的开发、测试和功能迭代提供参考。

## 📈 总体进度概览

| 接口总数 | ✅ 功能完全实现 | 🟡 功能部分实现（模拟） | ❌ 未开始 |
|----------|----------------|----------------------|----------|
| 9        | 6             | 3                    | 0        |

## ✅ 功能完全实现的接口

以下接口的所有业务逻辑均已完成，数据完全来自数据库的真实查询和操作，可被视为生产就绪状态。

### 1. 获取骑手个人资料
- 路由: GET /api/courier/profile
- 状态: ✅ 已完成
- 实现说明: 从 Users 和 Couriers 表中聚合数据，返回扁平的个人资料 JSON 对象。

### 2. 获取骑手当前工作状态
- 路由: GET /api/courier/status
- 状态: ✅ 已完成
- 实现说明: 从 Couriers 表中读取 IsOnline 字段，返回包含该布尔值的 JSON 对象。

### 3. 切换骑手工作状态
- 路由: POST /api/courier/status/toggle
- 状态: ✅ 已完成
- 实现说明: 根据请求体中的 isOnline 值，更新 Couriers 表中的 IsOnline 字段。

### 4. 获取订单列表
- 路由: GET /api/courier/orders
- 状态: ✅ 已完成
- 实现说明: 根据 status 查询参数，从 DeliveryTasks 表中筛选订单，并关联 Stores 和 Customers 表获取商家名称和顾客地址。

### 5. 接受订单
- 路由: POST /api/courier/orders/{orderId}/accept
- 状态: ✅ 已完成
- 实现说明: 验证订单状态为 Pending 后，将 DeliveryTasks 表中对应记录的 Status 更新为 Delivering，并记录 CourierID 和 AcceptTime。

### 6. 拒绝订单
- 路由: POST /api/courier/orders/{orderId}/reject
- 状态: ✅ 已完成
- 实现说明: 验证订单状态为 Pending 后，将 DeliveryTasks 表中对应记录的 Status 更新为 Cancelled。

## 🟡 功能部分实现（依赖模拟数据）的接口

以下接口的核心功能和数据契约已经实现，但部分字段依赖于模拟逻辑而非真实计算或外部服务调用。这些是未来需要进一步完成的迭代点。

### 1. 获取骑手当月收入
- 路由: GET /api/courier/income/monthly
- 状态: 🟡 部分实现 (模拟)
- 当前实现:
  - 接口返回一个 4000 到 9000 之间的随机数作为模拟收入。
- 未来待办 (TODO):
  - 替换模拟逻辑: 需要修改 CourierService 中的 GetMonthlyIncomeAsync 方法。
  - 真实实现逻辑: 查询当前骑手在本月内所有状态为 Completed 的 DeliveryTask 记录，并对 DeliveryFee 字段进行 SUM() 求和，返回真实的结算总额。

### 2. 获取骑手当前位置信息
- 路由: GET /api/courier/location
- 状态: 🟡 部分实现 (模拟)
- 当前实现:
  - 接口从数据库中读取骑手真实的经纬度坐标。
  - 返回一个包含这些坐标的模拟字符串，例如 "模拟位置 (经度: 116.30, 纬度: 39.98)"。
- 未来待办 (TODO):
  - 集成第三方地图服务: 需要修改 CourierService 中的 GetCurrentLocationAsync 方法。
  - 真实实现逻辑:
    - 注册地图服务商（如高德地图）的开发者账号并获取 API Key。
    - 调用其逆地理编码 API，将数据库中的经纬度坐标转换为真实的地理位置名称（例如 "北京市海淀区中关村"）。

### 3. 获取新订单详情
- 路由: GET /api/courier/orders/new/{notificationId}
- 状态: 🟡 部分实现 (模拟)
- 当前实现:
  - 接口已能从 DeliveryTasks, Stores, Customers, Users 等多个表中聚合所有真实的订单信息（如商家名、顾客地址、费用等）。
  - 但 distance 和 mapImageUrl 这两个字段返回的是固定的、硬编码的模拟字符串。
- 未来待办 (TODO):
  - 集成第三方地图服务: 需要修改 CourierService 中的 GetNewOrderDetailsAsync 方法。
  - 真实实现逻辑:
    - 调用地图服务的路线规划/距离计算 API，获取真实的 distance。
    - 调用地图服务的静态图 API，根据起点和终点坐标生成真实的 mapImageUrl。

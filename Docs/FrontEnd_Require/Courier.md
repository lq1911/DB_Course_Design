### 骑手端页面前端API说明文档
页面描述
骑手端是外卖系统的移动核心，为骑手提供接单、配送管理、查看收入和个人资料等功能。
页面路径
/courier (或其他您在路由中为骑手页面定义的路径)
页面功能点
核心看板: 显示骑手个人资料、今日数据统计、工作状态切换开关。
订单管理: 按“待取餐”、“配送中”、“已完成”状态展示订单列表。
新订单通知: 弹出新订单通知，包含订单详情，并提供接单或拒单操作。
收入与个人中心: 查看月度收入、信誉分、服务分等个人信息。
调用接口
通用说明: 以下所有接口都需要在请求的 Authorization 头部附带登录时获取的 Bearer Token 进行身份验证。
1. 获取骑手个人资料接口
接口说明: 获取当前登录骑手的核心个人信息，用于首页看板展示。
请求方式: GET
请求URL: /api/user/profile
请求参数说明: 无
返回参数说明 (data 字段)
| 字段名 | 类型 | 是否必填 | 说明 |
| :--- | :--- | :--- | :--- |
| name | string | 是 | 骑手姓名 (例如: "张明辉") |
| id | string | 是 | 骑手ID (例如: "RD20241201") |
| registerDate | string | 是 | 注册日期 (格式: "YYYY-MM-DD") |
| rating | number | 是 | 服务评分 (例如: 4.8) |
| creditScore | number | 是 | 信誉分 (例如: 892) |
2. 获取骑手工作状态接口
接口说明: 查询骑手当前是在线接单还是离线休息状态。
请求方式: GET
请求URL: /api/user/status
请求参数说明: 无
返回参数说明 (data 字段)
| 字段名 | 类型 | 是否必填 | 说明 |
| :--- | :--- | :--- | :--- |
| isOnline | boolean | 是 | true表示在线, false表示离线 |
3. 切换骑手工作状态接口
接口说明: 用于骑手“上线接单”或“下班休息”的操作。
请求方式: POST
请求URL: /api/user/status
请求参数说明 (Request Body)
| 字段名 | 类型 | 是否必填 | 说明 |
| :--- | :--- | :--- | :--- |
| isOnline | boolean | 是 | true表示切换为在线, false表示切换为离线 |
返回参数说明 (data 字段)
| 字段名 | 类型 | 是否必填 | 说明 |
| :--- | :--- | :--- | :--- |
| success | boolean | 是 | true表示操作成功 |
4. 获取骑手当月收入接口
接口说明: 获取当前登录骑手本月的总收入数据。
请求方式: GET
请求URL: /api/user/income/thisMonth
请求参数说明: 无
返回参数说明: 直接返回一个数字（number）类型的值，代表当月总收入。
5. 获取订单列表接口
接口说明: 根据指定的订单状态，获取对应的订单列表。
请求方式: GET
请求URL: /api/orders
请求参数说明 (Query Parameters)
| 字段名 | 类型 | 是否必填 | 说明 |
| :--- | :--- | :--- | :--- |
| status | string | 是 | 订单状态，可选值为: pending (待取餐), delivering (配送中), completed (已完成) |
返回参数说明 (data 字段): 返回一个 Order 对象数组，每个对象结构如下：
| 字段名 | 类型 | 是否必填 | 说明 |
| :--- | :--- | :--- | :--- |
| id | string | 是 | 订单ID |
| status | string | 是 | 订单状态 (pending, delivering, completed) |
| restaurant | string | 是 | 餐厅名称 |
| address | string | 是 | 顾客地址 |
| fee | string | 是 | 配送费 (格式化后的字符串) |
| statusText| string | 是 | 状态的中文描述 (例如: "待取餐") |
6. 获取骑手当前位置信息接口
接口说明: 获取骑手当前所在的地理区域或商圈信息。
请求方式: GET
请求URL: /api/user/location
请求参数说明: 无
返回参数说明 (data 字段)
| 字段名 | 类型 | 是否必填 | 说明 |
| :--- | :--- | :--- | :--- |
| area | string | 是 | 区域/商圈名称 (例如: "人民广场商圈") |
7. 获取新订单详情接口
接口说明: 当有新订单推送时，根据通知ID获取该订单的完整详情。
请求方式: GET
请求URL: /api/orders/new/{notificationId}
请求参数说明 (URL参数)
notificationId: 新订单的推送通知ID。
返回参数说明 (data 字段)
| 字段名 | 类型 | 是否必填 | 说明 |
| :--- | :--- | :--- | :--- |
| id | string | 是 | 新订单的ID |
| restaurantName| string | 是 | 餐厅名称 |
| restaurantAddress| string | 是 | 餐厅地址 |
| customerName | string | 是 | 顾客姓名 |
| customerAddress| string | 是 | 顾客地址 |
| distance | string | 是 | 配送距离 (例如: "2.3km") |
| fee | number | 是 | 配送费 |
| mapImageUrl | string | 是 | 路线地图的图片URL |
8. 接受订单接口
接口说明: 骑手接受一个新订单。
请求方式: POST
请求URL: /api/orders/{orderId}/accept
请求参数说明 (URL参数)
orderId: 要接受的订单ID。
返回参数说明 (data 字段)
| 字段名 | 类型 | 是否必填 | 说明 |
| :--- | :--- | :--- | :--- |
| success | boolean | 是 | true表示操作成功 |
9. 拒绝订单接口
接口说明: 骑手拒绝一个新订单。
请求方式: POST
请求URL: /api/orders/{orderId}/reject
请求参数说明 (URL参数)
orderId: 要拒绝的订单ID。
返回参数说明 (data 字段)
| 字段名 | 类型 | 是否必填 | 说明 |
| :--- | :--- | :--- | :--- |
| success | boolean | 是 | true表示操作成功 |
10. 更新骑手专属资料接口
接口说明: 用于更新骑手特定的信息，例如配送工具。
请求方式: PUT
请求URL: /api/user/profile/rider
请求参数说明 (Request Body)
| 字段名 | 类型 | 是否必填 | 说明 |
| :--- | :--- | :--- | :--- |
| vehicleType | string | 是 | 交通工具类型 (例如: "电动车") |
返回参数说明 (data 字段): 可能返回成功状态或更新后的部分资料。
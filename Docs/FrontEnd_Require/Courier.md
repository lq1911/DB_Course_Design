
# 🛵 骑手工作台页面说明文档

## 📄 页面概述
- **路径**：`front-end\src\views\courier\CourierView.vue`
- **简介**：骑手登录后进入的核心主页，集成状态、数据、订单、收益和个人中心等功能。

## 🧭 页面功能
- 显示骑手基本信息（姓名、ID）和今日总收入。
- 展示当前工作状态（在线/离线）及在线时长。
- 提供「开工 / 收工」按钮。
- 展示今日绩效数据：接单数 / 完成数 / 准时率。
- 集成地图显示当前位置。
- 底部导航栏包含：工作台、订单、收益、我的。
- 订单页可筛选：待处理 / 配送中 / 已完成。
- 接收新订单推送，弹窗展示订单详情与倒计时。
- 支持接受 / 拒绝订单。

---

## 🔌 调用接口

### 📥 1. 获取用户资料接口
**说明**：获取当前登录骑手的个人资料。  
**请求参数**：无

| 字段名        | 类型    | 说明         |
|---------------|---------|--------------|
| code          | int     | 状态码，200 表示成功 |
| message       | string  | 返回提示信息 |
| data.name     | string  | 骑手姓名     |
| data.id       | string  | 骑手唯一ID   |
| data.registerDate | string | 注册日期 (YYYY-MM-DD) |
| data.rating   | number  | 获评均分     |
| data.creditScore | number | 信誉积分     |

---

### 🟢 2. 获取工作状态接口

| 字段名            | 类型     | 说明         |
|-------------------|----------|--------------|
| data.isOnline     | boolean  | 是否在线     |
| data.onlineHours  | number   | 在线小时     |
| data.onlineMinutes| number   | 在线分钟     |
| data.todayOrders  | number   | 今日接单数   |
| data.completedOrders| number | 已完成订单数 |
| data.punctualityRate| number | 准时率 (%)   |

---

### 📍 3. 获取位置信息接口

| 字段名     | 类型   | 说明           |
|------------|--------|----------------|
| data.area  | string | 当前所在区域   |

---

### 📦 4. 获取订单列表接口

**请求参数**：

| 字段名 | 类型   | 是否必填 | 说明 |
|--------|--------|----------|------|
| status | string | ✅ 是     | 可选值：`pending`、`delivering`、`completed` |

**返回字段**：

| 字段名             | 类型   | 说明         |
|--------------------|--------|--------------|
| data[].id          | string | 订单ID       |
| data[].restaurant  | string | 餐厅名称     |
| data[].address     | string | 餐厅地址     |
| data[].fee         | string | 配送费       |
| data[].time        | string | 下单时间     |
| data[].status      | string | 订单状态     |
| data[].statusText  | string | 中文描述     |

---

### ✅ 5. 接受订单接口

**请求路径**：`/orders/{orderId}/accept`

| 字段名   | 类型   | 是否必填 | 说明       |
|----------|--------|----------|------------|
| orderId  | string | ✅ 是     | 接受的订单ID |

---

### ❌ 6. 拒绝订单接口

**请求路径**：`/orders/{orderId}/reject`

| 字段名   | 类型   | 是否必填 | 说明       |
|----------|--------|----------|------------|
| orderId  | string | ✅ 是     | 拒绝的订单ID |

---

### 💰 7. 获取收入汇总接口

| 字段名         | 类型   | 说明     |
|----------------|--------|----------|
| data.today     | number | 今日收入 |
| data.thisWeek  | number | 本周收入 |
| data.thisMonth | number | 本月收入 |
| data.monthlyOrders | number | 本月订单数 |

---

### 📊 8. 获取收入明细列表接口

| 字段名       | 类型   | 说明         |
|--------------|--------|--------------|
| data[].id    | number | 明细ID       |
| data[].type  | string | 收入类型     |
| data[].date  | string | 收入到账时间 |
| data[].amount| string | 金额         |

---

### 🔁 9. 切换工作状态接口

**请求体**：

| 字段名 | 类型    | 是否必填 | 说明          |
|--------|---------|----------|---------------|
| status | boolean | ✅ 是     | `true` 开工，`false` 收工 |

---

### 🆕 10. 获取新订单详情接口

**路径参数**：`/orders/new/{notificationId}`

| 字段名           | 类型   | 说明         |
|------------------|--------|--------------|
| data.id          | string | 订单ID       |
| data.restaurantName | string | 餐厅名称 |
| data.customerName   | string | 顾客姓名 |
| data.customerAddress| string | 顾客地址 |
| data.distance       | string | 距离       |
| data.mapImageUrl    | string | 路线图URL |

---

# 👤 账号与资料设置页面说明文档

## 📄 页面概述
- **路径**：`front-end\src\views\courier\AccountSettings.vue`
- **简介**：用于查看和编辑个人资料，如头像、姓名、性别、生日等。

---

## 🧭 页面功能
- 加载当前用户资料并填充表单。
- 支持本地选择头像并预览。
- 可编辑字段：姓名（必填）、性别、生日。
- 只读字段：用户ID、注册时间等。
- 「保存修改」按钮仅在表单有效 & 有更改时可点击。
- 修改成功后自动返回。
- 提供「返回」按钮回到个人中心。

---

## 🔌 调用接口

### 📥 1. 获取用户资料接口

| 字段名         | 类型   | 说明         |
|----------------|--------|--------------|
| data.name      | string | 姓名         |
| data.id        | string | 骑手ID       |
| data.gender    | string | 性别         |
| data.birthday  | string | 生日 (YYYY-MM-DD) |
| data.userId    | string | 用户ID       |
| data.registerDate | string | 注册时间  |
| data.rating    | number | 评分         |
| data.creditScore | number | 信誉积分     |

---

### ✏️ 2. 更新用户资料接口

**注意：头像上传使用 `multipart/form-data`。否则为 `application/json`。**

| 字段名  | 类型  | 是否必填 | 说明              |
|---------|-------|----------|-------------------|
| name    | string| ✅ 是     | 姓名              |
| gender  | string| ✅ 是     | 性别              |
| birthday| string| ✅ 是     | 生日              |
| avatar  | File  | 否       | （可选）头像图片  |

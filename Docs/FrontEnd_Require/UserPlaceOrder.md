# 生成订单等说明文档

### 调用接口

**1. 生成并提交新订单**

- 用来从购物车生成订单，并写入订单表  

- 输入表单说明

| 参数        | 类型     | 必填 | 说明                   |
| ----------- | -------- | ---- | ---------------------- |
| cartId      | number   | 是   | 购物车 ID              |
| customerId  | number   | 是   | 顾客 ID                |
| storeId     | string   | 是   | 店铺 ID                |
| paymentTime | datetime | 是   | 支付时间（下单时生成） |
| remarks     | string   | 否   | 订单备注               |

- 接口地址
`POST /api/store/order/create`

**2. 更新账户信息**

- 用来修改用户的基本信息（姓名、手机号、头像）

- 输入表单说明

| 参数        | 类型   | 必填 | 说明       |
| ----------- | ------ | ---- | ---------- |
| customerId  | number   | 是   | 顾客 ID  |
| name        | string | 是   | 用户姓名   |
| phoneNumber | number | 是   | 手机号     |
| image       | string | 是   | 头像 URL   |

- 接口地址
`PUT /api/account/update`

**3. 新增或更新收货地址**

- 用来新增或修改用户的收货地址信息

- 输入表单说明

| 参数        | 类型   | 必填 | 说明         |
| ----------- | ------ | ---- | ------------ |
| customerId  | number   | 是   | 顾客 ID  |
| name        | string | 是   | 收件人姓名   |
| phoneNumber | number | 是   | 收件人手机号 |
| address     | string | 是   | 详细收货地址 |

- 接口地址
`PUT /api/account/address/save`
**4.修改用户头像**
- 输入表单
| 参数     | 类型            | 必填 | 说明                     |
| ------ | ------------- | -- | ---------------------- |
| userId | number        | 是  | 用户 ID（作为 URL 路径参数传递）   |
| file   | file (binary) | 是  | 头像图片文件，支持 jpg/png/jpeg |
- 接口地址
`POST /api/account/avatar/{userId}`
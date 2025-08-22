# 用户下单与结算说明文档

### 调用接口

**1. 用户优惠券接口**

- 用来获得用户的优惠券信息

- 输入表单说明

| 字段名   | 类型   | 是否必填 | 说明 |
| -------- | ------ | -------- | ----- |
| userId | int | 是 | 用户编号 |

- 获得信息说明

| 字段名           | 类型        | 说明                       |
| ---------------- | ----------- | -------------------------- |
| couponID         | number      | 优惠券 ID                  |
| couponState      | number      | 优惠券状态（如已使用/未使用） |
| orderID          | number      | 关联的订单 ID               |
| couponManagerID  | number      | 优惠券管理 ID               |
| minimumSpend     | number      | 使用该优惠券的最低消费金额 |
| discountAmount   | number      | 优惠金额                     |
| validTo          | string      | 优惠券有效期（截止日期）     |

- 接口地址: `GET /api/user/coupons`

**2. 购物车接口**

- 用来获得用户的购物车信息

- 输入表单说明

| 字段名   | 类型   | 是否必填 | 说明 |
| -------- | ------ | -------- | ----- |
| userId | int | 是 | 用户编号 |
| storeId | string | 是 | 店铺编号 |

- 获得信息说明

| 字段名       | 类型                  | 说明             |
| ------------ | ------------------- | ---------------- |
| cartId       | number              | 购物车 ID        |
| totalPrice   | number              | 购物车总金额     |
| items        | ShoppingCartItem[]  | 购物车中的商品列表 |

- 以下为上方`ShoppingCartItem`数组的属性

| 字段名       | 类型      | 说明           |
| ------------ | -------- | -------------- |
| itemId       | number   | 购物项 ID      |
| dishId       | number   | 菜品 ID        |
| quantity     | number   | 购买数量       |
| totalPrice   | number   | 该项总金额     |

- 接口地址: `GET /api/store/shoppingcart`

**3. 添加或更新购物车项**

- 用来更新购物车的内容

- 输入表单说明

| 参数      | 类型    | 必填 | 说明           |
| --------- | ------- | ---- | -------------- |
| cartId    | number  | 是   | 购物车 ID      |
| dishId    | number  | 是   | 菜品 ID        |
| quantity  | number  | 是   | 购买数量       |

- 接口地址: `/api/store/cart/change`

**4. 删除购物车项**

- 当购物车项减小到零之后从表中删除

- 输入表单说明

| 参数      | 类型    | 必填 | 说明           |
| --------- | ------- | ---- | -------------- |
| cartId    | number  | 是   | 购物车 ID      |
| dishId    | number  | 是   | 菜品 ID        |

- 接口地址: `/api/store/cart/remove`
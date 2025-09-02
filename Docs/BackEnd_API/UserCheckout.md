# 用户下单与结算接口文档

## 用户优惠券接口
- **接口名称**: GetUserCoupons
- **接口描述**: 用来获得用户的优惠券信息
- **接口地址(URL)**: `/api/user/coupons`
- **请求方式**: `GET`
### 返回结果
- 成功
```json
[
  {
    "couponID": 101,
    "couponState": "Unused",
    "orderID": null,
    "couponManagerID": 5,
    "minimumSpend": 100.00,
    "discountAmount": 20.00,
    "validTo": "2025-09-30 23:59:59"
  },
  {
    "couponID": 102,
    "couponState": "Used",
    "orderID": 5001,
    "couponManagerID": 5,
    "minimumSpend": 50.00,
    "discountAmount": 10.00,
    "validTo": "2025-08-20 23:59:59"
  }
]
```

- 未找到用户
```json
{
  "message": "用户不存在或不是顾客"
}
```

- 服务器出错
```json
{
  "message": "获取优惠券信息时发生错误"
}
```

### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `code` | int | 是 | 状态码 |
| `message` | string | 是 | 接口返回说明信息 |
| `couponID` | int | 是 | 优惠券 ID |
| `couponState` | CouponState | 是 | 优惠券状态（如已使用/未使用） |
| `orderID` | int | 是 | 关联的订单 ID |
| `couponManagerID` | int | 是 | 优惠券管理 ID |
| `minimumSpend` | decimal(10,2) | 是 | 使用该优惠券的最低消费金额 |
| `discountAmount` | decimal(10,2) | 是 | 优惠金额 |
| `validTo`| string | 是 | 优惠券有效期（截止日期） |

## 购物车接口
- **接口名称**:GetShoppingCart
- **接口描述**:用来获得用户的购物车信息
- **接口地址(URL)**: `/api/store/shoppingcart`
- **请求方式**: `GET`
### 返回结果
- 成功
```json
{
  "cartId": 12,
  "totalPrice": 85.50,
  "items": [
    {
      "itemId": 201,
      "dishId": 301,
      "quantity": 2,
      "totalPrice": 50.00
    },
    {
      "itemId": 202,
      "dishId": 305,
      "quantity": 1,
      "totalPrice": 35.50
    }
  ]
}
```
- 购物车为空（成功）
```json
{
  "cartId": 0,
  "totalPrice": 0,
  "items": []
}
```
- 输入错误
```json
{
  "message": "用户不存在或不是顾客"
}
```
- 服务器出错
```json
{
  "message": "获取购物车信息时发生错误"
}
```
### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `code` | int | 是 | 状态码 |
| `message` | string | 是 | 接口返回说明信息 |
| `cartId` | int | 是 | 购物车 ID        |
| `totalPrice` | decimal(10,2) | 是 | 购物车总金额     |
| `items` | ShoppingCartItem[] | 是 | 购物车中的商品列表 |
- 以下为上方`ShoppingCartItem`数组的属性
| 参数名 | 类型 | 是否必填 | 说明 |
| ------------ | -------- | -------------- |
| `itemId` | int | 是 | 购物项 ID |
| `dishId` | int | 是 | 菜品 ID |
| `quantity` | int | 是 | 购买数量 |
| `totalPrice` | decimal(10,2) | 是 | 该项总金额 |

## 添加或更新购物车项
- **接口名称**:UpdateCartItem
- **接口描述**:用来更新购物车的内容
- **接口地址(URL)**: `/api/store/cart/change`
- **请求方式**: `Post`
### 返回结果
- 成功
```json
{
  "cartId": 123,
  "totalPrice": 100.50,
  "items": [
    {
      "itemId": 1,
      "dishId": 101,
      "quantity": 2,
      "totalPrice": 50.00
    },
    {
      "itemId": 2,
      "dishId": 102,
      "quantity": 1,
      "totalPrice": 50.50
    }
  ]
}
```
- 输入错误
```json
{
  "message": "无效的请求数据"
}
```
- 服务器出错
```json
{
  "message": "更新购物车项时发生错误"
}
```
### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `code` | int | 是 | 状态码 |
| `message` | string | 是 | 接口返回说明信息 |
| `cartId` | int | 是 | 购物车 ID        |
| `totalPrice` | decimal(10,2) | 是 | 购物车总金额     |
| `items` | ShoppingCartItem[] | 是 | 购物车中的商品列表 |
- 以下为上方`ShoppingCartItem`数组的属性
| 参数名 | 类型 | 是否必填 | 说明 |
| ------------ | -------- | -------------- |
| `itemId` | int | 是 | 购物项 ID |
| `dishId` | int | 是 | 菜品 ID |
| `quantity` | int | 是 | 购买数量 |
| `totalPrice` | decimal(10,2) | 是 | 该项总金额 |


## 删除购物车项
- **接口名称**:RemoveCartItem
- **接口描述**:用来更新购物车的内容
- **接口地址(URL)**: `/api/store/cart/remove`
- **请求方式**: `Delete`
### 返回结果
- 成功
```json
{
  "cartId": 123,
  "totalPrice": 50.00,
  "items": [
    {
      "itemId": 2,
      "dishId": 102,
      "quantity": 1,
      "totalPrice": 50.00
    }
  ]
}
```
- 输入错误
```json
{
  "message": "无效的请求数据"
}
```
- 服务器出错
```json
{
  "message": "删除购物车项时发生错误"
}
```
### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `code` | int | 是 | 状态码 |
| `message` | string | 是 | 接口返回说明信息 |
| `cartId` | int | 是 | 购物车 ID        |
| `totalPrice` | decimal(10,2) | 是 | 购物车总金额     |
| `items` | ShoppingCartItem[] | 是 | 购物车中的商品列表 |
- 以下为上方`ShoppingCartItem`数组的属性
| 参数名 | 类型 | 是否必填 | 说明 |
| ------------ | -------- | -------------- |
| `itemId` | int | 是 | 购物项 ID |
| `dishId` | int | 是 | 菜品 ID |
| `quantity` | int | 是 | 购买数量 |
| `totalPrice` | decimal(10,2) | 是 | 该项总金额 |


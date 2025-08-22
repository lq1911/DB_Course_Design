# 用户首页接口

## 1. 推荐接口
- **接口名称**: recommend
- **接口描述**: 从评分最靠前的若干店铺中，随机选择4个店铺，获取它们的信息并输出
- **接口地址(URL)**: `/api/user/home/recommend`
- **请求方式**: `GET`

### 返回结果
- 成功：
```json
{
  "code": 200,
  "message": "",
  "data": {
    [
      "StoreID": ,
      "StoreName": "",
      "AverageRating": ,
      "MonthlySales": ,
    ]
  }
}
- 没有推荐商家(调试时用)
```json
{
  "code": 404,
  "message": "There's No Recommend Store For User.",
}
```

### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `code` | int | 是 | 状态码 |
| `message` | string | 是 | 接口返回说明信息 |
| `data.StoreID` | int | 是 | 店铺 ID |
| `data.StoreName` | string | 是 | 店铺名 |
| `data.AverageRating` | decimal(10,2) | 是 | 店铺平均分 |
| `data.MonthlySales` | int | 是 | 店铺月销售量 |

## 2. 搜索接口
- **接口名称**: search
- **接口描述**: 跟据前端输入的信息（店铺名、菜品名），进行匹配，返回所有相关的店铺信息
- **接口地址(URL)**: `/api/user/home/search`
- **请求方式**: `GET`

### 返回结果
- 成功
```json
{
  "code": 200,
  "message": "Search results retrieved successfully",
  "data": {
    "stores": [
      {
        "storeName": "",
        "averageRating": ,
        "monthlySales": ,
        "storeAddress": ""
      }
    ],
    "dishes": [
      {
        "storeName": "",
        "averageRating": ,
        "monthlySales": ,
        "storeAddress": ""
      }
    ]
  }
}
```

- 输入有误
```json
{
  "code": 400,
  "message": "Invalid request",
}
```
- 无搜索结果
```json
{
  "code": 404,
  "message": "There's No Search results.",
}
```

### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `code` | int | 是 | 状态码 |
| `message` | string | 是 | 接口返回说明信息 |
| `averageRating` | decimal(10,2) | 是 | 店铺均分 |
| `name` | string | 是 | 店铺名字 |
| `monthlySales` | int | 是 | 店铺月销售量 |
| `storeAddress` | string | 是 | 店铺地址 |


## 3. 订单接口
- **接口名称**: orders
- **接口描述**: 跟据前端输入的信息（店铺名、菜品名），进行匹配，返回所有相关的店铺信息
- **接口地址(URL)**: `/api/user/home/orders`
- **请求方式**: `GET`
### 返回结果
- 
### 响应说明


## 4. 用户信息接口
- **接口名称**: uesrInfo
- **接口描述**: 跟据前端输入的信息（店铺名、菜品名），进行匹配，返回所有相关的店铺信息
- **接口地址(URL)**: `/api/user/home/userInfo`
- **请求方式**: `GET`
### 返回结果
### 响应说明


## 5. 优惠券接口
- **接口名称**: coupon
- **接口描述**: 查询用户优惠券
- **接口地址(URL)**: `/api/user/home/couponInfo`
- **请求方式**: `GET`
### 返回结果
- 成功
```json
{
  "code": 200,
  "message": "Coupons retrieved successfully",
  "data": [
    {
      "couponID": 1,
      "couponState": 0,
      "orderID": null,
      "couponManagerID": 10,
      "minimumSpend": 100,
      "discountAmount": 20,
      "validTo": "2025-12-31T23:59:59"
    },
    {
      "couponID": 2,
      "couponState": 1,
      "orderID": 55,
      "couponManagerID": 11,
      "minimumSpend": 200,
      "discountAmount": 50,
      "validTo": "2025-11-30T23:59:59"
    }
  ]
}
```

- 输入信息有误
```json
{
  "code": 400,
  "message": "Invalid request",
}
```

- 用户无优惠卷
```json
{
  "code": 404,
  "message": "There's No Coupon For User.",
}
```
### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `code` | int | 是 | 状态码 |
| `message` | string | 是 | 接口返回说明信息 |
| `couponID` | int | 是 | 优惠券编号 |
| `couponState` | CouponState | 是 | 优惠券状态 |
| `orderID` | int | 是 | 关联订单编号（若未使用则可能为空或 0） |
| `couponManagerID` | int | 是 | 发放优惠券的管理员或活动编号 |
| `minimumSpend` | decimal(10,2) | 是 | 使用该优惠券的最低消费金额 |
| `discountAmount` | decimal(10,2) | 是 | 优惠金额 |
| `validTo` | DateTime | 是 | 优惠券有效期截止日期 |


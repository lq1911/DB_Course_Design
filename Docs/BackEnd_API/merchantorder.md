# **订单中心接口文档**
## **1. 获取订单列表接口**

- **接口名称**：FetchOrderList
- **接口描述**：获取当前商家的订单列表，支持按商家ID或门店ID筛选
- **接口地址**：GET /api/orders
- **请求方式**：GET

**请求参数：**

|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|sellerId|number|否|商家ID，用于筛选|
|storeId|number|否|门店ID，用于筛选|

**返回结果：**

```
{
    "code": 200,
    "success": true,
    "message": null,
    "data": [
      {
        "orderId": 1001,
        "paymentTime": "2024-12-01T12:30:00.000Z",
        "remarks": "不要辣椒",
        "customerId": 1,
        "cartId": 10,
        "storeId": 101,
        "sellerId": 201
      }
    ]
}
```

**响应说明：**

|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|code|int|是|状态码|
|success|boolean|是|是否成功|
|message|string|否|错误信息|
|data|object[]|是|订单列表|
|data[].orderId|number|是|订单ID|
|data[].paymentTime|string|是|支付时间（ISO格式）|
|data[].remarks|string|否|订单备注|
|data[].customerId|number|是|客户ID|
|data[].cartId|number|是|购物车ID|
|data[].storeId|number|是|门店ID|
|data[].sellerId|number|是|商家ID|

## **2. 获取订单详情接口**

- **接口名称**：FetchOrderDetail
- **接口描述**：根据订单ID获取订单详情
- **接口地址**：GET /api/orders/{orderId}
- **请求方式**：GET

**请求参数：**

|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|orderId|number|是|订单ID|

**返回结果：**

```
{
    "code": 200,
    "success": true,
    "message": null,
    "data": {
      "orderId": 1001,
      "paymentTime": "2024-12-01T12:30:00.000Z",
      "remarks": "不要辣椒",
      "customerId": 1,
      "cartId": 10,
      "storeId": 101,
      "sellerId": 201
    }
}
```

**响应结果：**

同“获取订单列表”接口的单个订单结构。

## **3. 商家接单接口**

- **接口名称**：AcceptOrder
- **接口描述**：商家接单操作
- **接口地址**：POST /api/orders/{orderId}/accept
- **请求方式**：POST

**请求参数：**

|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|orderId|number|是|订单ID|

**返回结果：**

```
{
    "code": 200,
    "success": true,
    "message": null,
    "data": {
      "orderId": 1001,
      "decision": "accepted",
      "decidedAt": "2024-12-01T12:35:00.000Z"
    }
}
```

**响应结果：**

|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|code|int|是|状态码|
|success|boolean|是|是否成功|
|message|string|否|错误信息|
|data|object|是|接单结果|
|data.orderId|number|是|订单ID|
|data.decision|string|是|接单状态|
|data.decidedAt|string|是|接单时间|

## **4. 商家拒单接口**

- **接口名称**：RejectOrder
- **接口描述**：商家拒单操作
- **接口地址**：POST /api/orders/{orderId}/reject
- **请求方式**：POST

**请求参数：**

|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|orderId|number|是|订单ID|

**请求体：**

```
{
    "reason": "菜品已售罄"
}
```

**返回结果：**

```
{
    "code": 200,
    "success": true,
    "message": null,
    "data": {
      "orderId": 1001,
      "decision": "rejected",
      "decidedAt": "2024-12-01T12:35:00.000Z",
      "reason": "菜品已售罄"
    }
}
```

**响应结果：**

|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|code|int|是|状态码|
|success|boolean|是|是否成功|
|message|string|否|错误信息|
|data|object|是|拒单结果|
|data.orderId|number|是|订单ID|
|data.decision|string|是|拒单状态|
|data.decidedAt|string|是|拒单时间|
|data.reason|string|否|拒单原因|


## **5. 获取订单购物车条目接口**

- **接口名称**：FetchCartItems
- **接口描述**：根据购物车ID获取购物车中的菜品条目
- **接口地址**：GET /api/carts/{cartId}/items
- **请求方式**：GET

**请求参数**：

|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|cartId|number|是|购物车ID|

**返回结果**：

```
{
    "code": 200,
    "success": true,
    "message": null,
    "data": [
      {
        "itemId": 1,
        "quantity": 2,
        "totalPrice": 56,
        "dishId": 101,
        "cartId": 10,
        "dish": {
          "dishId": 101,
          "dishName": "水煮鱼",
          "price": 28,
          "description": "鲜鱼片，烤箱不燃",
          "isSoldOut": 2
        }
      }
    ]
}
```

**响应结果**：

|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|code|int|是|状态码|
|success|boolean|是|是否成功|
|message|string|否|错误信息|
|data|object[]|是|购物车条目列表|
|data[].itemId|number|是|条目ID|
|data[].quantity|number|是|数量|
|data[].totalPrice|number|是|小计金额|
|data[].dishId|number|是|菜品ID|
|data[].cartId|number|是|购物车ID|
|data[].dish|object|否|菜品信息|
|data[].dish.dishId|number|是|菜品ID|
|data[].dish.dishName|string|是|菜品名称|
|data[].dish.price|number|是|菜品价格|
|data[].dish.description|string|是|菜品描述|
|data[].dish.isSoldOut|number|是|是否售罄（0/2）|


## **6. 获取订单优惠券信息接口**

- **接口名称**：FetchOrderCouponInfo
- **接口描述**：根据订单ID获取订单使用的优惠券信息
- **接口地址**：GET /api/orders/{orderId}/coupons
- **请求方式**：GET

**请求参数：**

|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|orderId|number|是|订单ID|

**返回结果：**

```
{
    "code": 200,
    "success": true,
    "message": null,
    "data": {
      "couponId": 5001,
      "couponName": "优惠劵5001",
      "description": "满50减10元",
      "discountType": "fixed",
      "discountValue": 10,
      "validFrom": "2024-01-01T00:00:00.000Z",
      "validTo": "2024-12-31T23:59:59.000Z",
      "isUsed": true
    }
}
```

**响应结果：**

|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|code|int|是|状态码|
|success|boolean|是|是否成功|
|message|string|否|错误信息|
|data|object|是|优惠券信息|
|data.couponId|number|是|优惠券ID|
|data.couponName|string|否|优惠券名称|
|data.description|string|否|优惠券描述|
|data.discountType|string|是|折扣类型：percentage/fixed|
|data.discountValue|number|是|折扣值|
|data.validFrom|string|是|有效期开始时间|
|data.validTo|string|是|有效期结束时间|
|data.isUsed|boolean|是|是否已使用|


## **7.获取菜品列表接口**

- **接口名称**：FetchDishList
- **接口描述**：获取当前商家的菜品列表
- **接口地址**：GET /api/dishes
- **请求方式**：GET

**返回结果**：

```
{
    "code": 200,
    "success": true,
    "message": null,
    "data": [
      {
        "dishId": 1,
        "dishName": "鱼香肉丝",
        "price": 28,
        "description": "味道好吃鲜美",
        "isSoldOut": 2
      },
      {
        "dishId": 2,
        "dishName": "麻辣烫",
        "price": 32,
        "description": "菜品新鲜好吃",
        "isSoldOut": 2
      }
    ]
}
```

**响应结果：**

|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|code|int|是|状态码|
|success|boolean|是|是否成功|
|message|string|否|错误信息|
|data|object[]|是|菜品列表|
|data[].dishId|number|是|菜品ID|
|data[].dishName|string|是|菜品名称|
|data[].price|number|是|菜品价格|
|data[].description|string|是|菜品描述|
|data[].isSoldOut|number|是|是否售罄（0/2）|


## **8.创建菜品接口**

- **接口名称**：CreateDish
- **接口描述**：创建新菜品
- **接口地址**：POST /api/dishes
- **请求方式**：POST

**请求参数：**


|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|dishName|string|是|菜品名称|
|price|number|是|菜品价格|
|description|string|是|菜品描述|
|isSoldOut|number|否|是否售罄（0/2，默认2）|

**请求体：**

```
{
    "dishName": "肥牛拌饭",
    "price": 38,
    "description": "甜鲜可口",
    "isSoldOut": 2
}
```

**返回结果：**

```
{
    "code": 200,
    "success": true,
    "message": null,
    "data": {
      "dishId": 5,
      "dishName": "肥牛拌饭",
      "price": 38,
      "description": "甜鲜可口",
      "isSoldOut": 2
    }
}
```

**响应结果**：

同"获取菜品列表"接口的单个菜品结构。

## **9.更新菜品信息接口**

- **接口名称**：UpdateDish
- **接口描述**：更新菜品信息
- **接口地址**：PATCH /api/dishes/{dishId}
- **请求方式**：PATCH

**请求参数**：

|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|dishId|number|是|菜品ID|

**请求体：**

```
{
    "dishName": 鸡公煲,
    "price": 60,
    "description": "菜品食材更加丰富"
}
```

**返回结果：**

```
{
    "code": 200,
    "success": true,
    "message": null,
    "data": {
    "dishId": 4,
      "dishName": "鸡公煲",
      "price": 60,
      "description": "菜品食材更加丰富",
      "isSoldOut": 2
    }
}
```

**响应结果：**

同"获取菜品列表"接口的单个菜品结构。

## **10.切换菜品售罄状态接口**

- **接口名称**：ToggleDishSoldOut
- **接口描述**：切换菜品的售罄状态
- **接口地址**：PATCH /api/dishes/{dishId}/soldout
- **请求方式**：PATCH

**请求参数:**

|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|dishId|number|是|菜品ID|

**请求体：**

```
{
    "isSoldOut": 0
}
```

**返回结果：**

```
{
    "code": 200, 
    "success": true,
    "message": null,
    "data": {
      "dishId": 5,
      "dishName": "肥牛拌饭",
      "price": 40,
      "description": "甜鲜可口",
      "isSoldOut": 0
    }
}
```

**响应说明:**

同"获取菜品列表"接口的单个菜品结构。

## **11.发布配送任务接口**

- **接口名称**：PublishDeliveryTask
- **接口描述**：为订单发布配送任务
- **接口地址**：POST /api/delivery-tasks/publish
- **请求方式**：POST

**请求参数**：

|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|orderId|number|是|订单ID|
|estimatedArrivalTime|string|是|预计到店时间（ISO格式）|
|estimatedDeliveryTime|string|是|预计送达时间（ISO格式）|

**请求体**：

```
{
    "orderId": 1001,
    "estimatedArrivalTime": "2024-12-01T13:00:00.000Z",
    "estimatedDeliveryTime": "2024-12-01T13:30:00.000Z"
}
```

**返回结果：**

```
{
    "code": 200,
    "success": true,
    "message": null,
    "data": {
      "deliveryTask": {
        "taskId": 2001,
        "estimatedArrivalTime": "2024-12-01T13:00:00.000Z",
        "estimatedDeliveryTime": "2024-12-01T13:30:00.000Z",
        "customerId": 1,
        "storeId": 101
      },
      "publish": {
        "sellerId": 201,
        "deliveryTaskId": 2001,
        "publishTime": "2024-12-01T12:40:00.000Z"
      }
    }
}
```

**响应说明：**

|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|code|int|是|状态码|
|success|boolean|是|是否成功|
|message|string|否|错误信息|
|data|object|是|配送任务信息|
|data.deliveryTask|object|是|配送任务详情|
|data.deliveryTask.taskId|number|是|任务ID|
|data.deliveryTask.estimatedArrivalTime|string|是|预计到店时间|
|data.deliveryTask.estimatedDeliveryTime|string|是|预计送达时间|
|data.deliveryTask.customerId|number|是|客户ID|
|data.deliveryTask.storeId|number|是|门店ID|
|data.publish|object|是|发布信息|
|data.publish.sellerId|number|是|商家ID|
|data.publish.deliveryTaskId|number|是|配送任务ID|
|data.publish.publishTime|string|是|发布时间|


## **12.获取订单配送信息接口**

- **接口名称**：FetchOrderDeliveryInfo
- **接口描述**：获取订单的配送信息
- **接口地址**：GET /api/orders/{orderId}/delivery-info
- **请求方式**：GET

**请求参数：**

|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|orderId|number|是|订单ID|

**返回结果：**

```
{
    "code": 200,
    "success": true,
    "message": null,
    "data": {
      "deliveryTask": {
        "taskId": 2001,
        "estimatedArrivalTime": "2024-12-01T13:00:00.000Z",
        "estimatedDeliveryTime": "2024-12-01T13:30:00.000Z",
        "courierLongitude": 116.397128,
        "courierLatitude": 39.916527,
        "customerId": 1,
        "storeId": 101
      },
      "publish": {
        "sellerId": 201,
        "deliveryTaskId": 2001,
        "publishTime": "2024-12-01T12:40:00.000Z"
      },
      "accept": {
        "courierId": 3001,
        "deliveryTaskId": 2001,
        "acceptTime": "2024-12-01T12:42:00.000Z"
      },
      "courier": {
        "userId": 3001,
        "courierRegistrationTime": "2024-01-15T09:30:00.000Z",
        "vehicleType": "电动车",
        "reputationPoints": 95,
        "totalDeliveries": 120,
        "avgDeliveryTime": 25,
        "averageRating": 4.8,
        "monthlySalary": 5000,
        "fullName": "李四",
        "phoneNumber": 13800138000
      }
    }
}
```

**响应说明：**

|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|code|int|是|状态码|
|success|boolean|是|是否成功|
|message|string|否|错误信息|
|data|object|是|配送信息|
|data.deliveryTask|object|否|配送任务详情|
|data.publish|object|否|发布信息|
|data.accept|object|否|接单信息|
|data.courier|object|否|骑手信息|
|data.accept.courierId|number|是|骑手ID|
|data.accept.deliveryTaskId|number|是|配送任务ID|
|data.accept.acceptTime|string|是|接单时间|
|data.courier.userId|number|是|骑手用户ID|
|data.courier.courierRegistrationTime|string|否|注册时间|
|data.courier.vehicleType|string|是|车型|
|data.courier.reputationPoints|number|是|信誉积分|
|data.courier.totalDeliveries|number|是|总配送次数|
|data.courier.avgDeliveryTime|number|是|平均配送时长|
|data.courier.averageRating|number|是|平均评分|
|data.courier.monthlySalary|number|是|月薪|
|data.courier.fullName|string|否|骑手姓名|
|data.courier.phoneNumber|number|否|骑手电话|


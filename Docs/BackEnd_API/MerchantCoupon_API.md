# 商家配券中心接口

## 1. 获取优惠券列表接口
- **接口名称**: getCoupons
- **接口描述**: 用来获得商家的优惠券列表（分页）
- **接口地址(URL)**: `/api/merchant/coupons`
- **请求方式**: `GET`

### 请求参数
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `page` | int | 是 | 页码（从1开始） |
| `pageSize` | int | 是 | 每页数量（1-100之间） |

### 返回结果
- 成功
```json
{
  "code": 200,
  "message": "获取优惠券列表成功",
  "data": {
    "list": [
      {
        "id": 2001,
        "name": "新用户满50减10",
        "type": "fixed",
        "value": 10.00,
        "minAmount": 50.00,
        "totalQuantity": 1000,
        "usedQuantity": 150,
        "startTime": "2024-01-01T00:00:00Z",
        "endTime": "2024-12-31T23:59:59Z",
        "description": "新用户专享优惠券",
        "status": "active"
      },
      {
        "id": 2002,
        "name": "9折优惠券",
        "type": "discount",
        "value": 0.90,
        "minAmount": 30.00,
        "totalQuantity": 500,
        "usedQuantity": 80,
        "startTime": "2024-01-01T00:00:00Z",
        "endTime": "2024-12-31T23:59:59Z",
        "description": "全场9折优惠",
        "status": "active"
      }
    ],
    "total": 25
  }
}
```
- 输入有误
```json
{
  "code": 400,
  "message": "页码必须大于0，页大小必须在1-100之间"
}
```
- 服务器错误
```json
{
  "code": 500,
  "message": "获取优惠券列表失败，请稍后重试"
}
```

### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `code` | int | 是 | 状态码 |
| `message` | string | 是 | 接口返回说明信息 |
| `data.list` | CouponDto[] | 是 | 优惠券列表 |
| `data.total` | int | 是 | 总条数 |

> 以下为优惠券列表中`CouponDto`对象的具体属性

| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `id` | int | 是 | 优惠券编号 |
| `name` | string | 是 | 优惠券名称 |
| `type` | string | 是 | 优惠券类型（fixed=满减券，discount=折扣券） |
| `value` | decimal | 是 | 优惠值（满减券为金额，折扣券为比例） |
| `minAmount` | decimal | 是 | 最低消费金额 |
| `totalQuantity` | int | 是 | 发放数量 |
| `usedQuantity` | int | 是 | 已使用数量 |
| `startTime` | string | 是 | 开始时间（ISO 8601格式） |
| `endTime` | string | 是 | 结束时间（ISO 8601格式） |
| `description` | string | 是 | 优惠券描述 |
| `status` | string | 是 | 状态（active=有效，expired=已过期，upcoming=未开始） |

## 2. 获取优惠券统计接口
- **接口名称**: getCouponStats
- **接口描述**: 用来获得商家的优惠券统计信息
- **接口地址(URL)**: `/api/merchant/coupons/stats`
- **请求方式**: `GET`

### 返回结果
- 成功
```json
{
  "code": 200,
  "message": "获取统计信息成功",
  "data": {
    "total": 25,
    "active": 15,
    "expired": 8,
    "upcoming": 2,
    "totalUsed": 1250,
    "totalDiscountAmount": 15680.50
  }
}
```
- 服务器错误
```json
{
  "code": 500,
  "message": "获取统计信息失败，请稍后重试"
}
```

### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `code` | int | 是 | 状态码 |
| `message` | string | 是 | 接口返回说明信息 |
| `data.total` | int | 是 | 总优惠券数量 |
| `data.active` | int | 是 | 有效优惠券数量 |
| `data.expired` | int | 是 | 已过期优惠券数量 |
| `data.upcoming` | int | 是 | 未开始优惠券数量 |
| `data.totalUsed` | int | 是 | 总使用次数 |
| `data.totalDiscountAmount` | decimal | 是 | 总优惠金额 |

## 3. 创建优惠券接口
- **接口名称**: createCoupon
- **接口描述**: 用来创建新的优惠券
- **接口地址(URL)**: `/api/merchant/coupons`
- **请求方式**: `POST`

### 请求参数
```json
{
  "id": 2003,
  "name": "夏日特惠券",
  "type": "fixed",
  "value": 15.00,
  "minAmount": 80.00,
  "discountAmount": 15.00,
  "totalQuantity": 200,
  "startTime": "2024-06-01T00:00:00Z",
  "endTime": "2024-08-31T23:59:59Z",
  "description": "夏日特惠，满80减15"
}
```

### 返回结果
- 成功
```json
{
  "code": 200,
  "message": "优惠券创建成功",
  "data": {
    "id": 2003
  }
}
```
- 输入有误
```json
{
  "code": 400,
  "message": "请求数据验证失败: 优惠券名称不能为空"
}
```
- 服务器错误
```json
{
  "code": 500,
  "message": "创建优惠券失败: 错误类型: DbUpdateException; 错误消息: An error occurred while saving the entity changes"
}
```

### 请求参数说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `id` | int | 是 | 优惠券ID（前端提供） |
| `name` | string | 是 | 优惠券名称（最大100字符） |
| `type` | string | 是 | 优惠券类型（fixed=满减券，discount=折扣券） |
| `value` | decimal | 是 | 优惠值（0.01-999999.99之间） |
| `minAmount` | decimal | 是 | 最低消费金额（0.01-999999.99之间） |
| `discountAmount` | decimal | 是 | 优惠金额（0.00-999999.99之间） |
| `totalQuantity` | int | 是 | 发放数量（1-100000之间） |
| `startTime` | string | 是 | 开始时间（ISO 8601格式） |
| `endTime` | string | 是 | 结束时间（ISO 8601格式） |
| `description` | string | 否 | 描述（最大500字符） |

### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `code` | int | 是 | 状态码 |
| `message` | string | 是 | 接口返回说明信息 |
| `data.id` | int | 是 | 新创建的优惠券ID |

## 4. 更新优惠券接口
- **接口名称**: updateCoupon
- **接口描述**: 用来更新已存在的优惠券信息
- **接口地址(URL)**: `/api/merchant/coupons/{id}`
- **请求方式**: `PUT`

### 请求参数
```json
{
  "id": 2003,
  "name": "夏日特惠券（更新）",
  "type": "fixed",
  "value": 20.00,
  "minAmount": 100.00,
  "discountAmount": 20.00,
  "totalQuantity": 300,
  "startTime": "2024-06-01T00:00:00Z",
  "endTime": "2024-08-31T23:59:59Z",
  "description": "夏日特惠，满100减20"
}
```

### 返回结果
- 成功
```json
{
  "code": 200,
  "message": "优惠券更新成功"
}
```
- 输入有误
```json
{
  "code": 400,
  "message": "请求数据验证失败: 优惠券名称不能为空"
}
```
- 资源不存在
```json
{
  "code": 404,
  "message": "优惠券 2003 不存在或不属于商家 3"
}
```
- 服务器错误
```json
{
  "code": 500,
  "message": "更新优惠券失败: 错误类型: DbUpdateException"
}
```

### 请求参数说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `id` | int | 是 | 优惠券ID（路径参数） |
| `name` | string | 是 | 优惠券名称（最大100字符） |
| `type` | string | 是 | 优惠券类型（fixed=满减券，discount=折扣券） |
| `value` | decimal | 是 | 优惠值（0.01-999999.99之间） |
| `minAmount` | decimal | 是 | 最低消费金额（0.01-999999.99之间） |
| `discountAmount` | decimal | 是 | 优惠金额（0.00-999999.99之间） |
| `totalQuantity` | int | 是 | 发放数量（1-100000之间） |
| `startTime` | string | 是 | 开始时间（ISO 8601格式） |
| `endTime` | string | 是 | 结束时间（ISO 8601格式） |
| `description` | string | 否 | 描述（最大500字符） |

### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `code` | int | 是 | 状态码 |
| `message` | string | 是 | 接口返回说明信息 |

## 5. 删除优惠券接口
- **接口名称**: deleteCoupon
- **接口描述**: 用来删除指定的优惠券
- **接口地址(URL)**: `/api/merchant/coupons/{id}`
- **请求方式**: `DELETE`

### 请求参数
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `id` | int | 是 | 优惠券ID（路径参数） |

### 返回结果
- 成功
```json
{
  "code": 200,
  "message": "优惠券删除成功"
}
```
- 资源不存在
```json
{
  "code": 404,
  "message": "优惠券 2003 不存在或不属于商家 3"
}
```
- 服务器错误
```json
{
  "code": 500,
  "message": "删除优惠券失败，请稍后重试"
}
```

### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `code` | int | 是 | 状态码 |
| `message` | string | 是 | 接口返回说明信息 |

## 6. 批量删除优惠券接口
- **接口名称**: batchDeleteCoupons
- **接口描述**: 用来批量删除多个优惠券
- **接口地址(URL)**: `/api/merchant/coupons/batch`
- **请求方式**: `DELETE`

### 请求参数
```json
{
  "ids": [2001, 2002, 2003]
}
```

### 返回结果
- 成功
```json
{
  "code": 200,
  "message": "成功删除 3 张优惠券",
  "data": {
    "deletedCount": 3
  }
}
```
- 输入有误
```json
{
  "code": 400,
  "message": "请求数据验证失败: 优惠券ID列表不能为空"
}
```
- 服务器错误
```json
{
  "code": 500,
  "message": "批量删除优惠券失败，请稍后重试"
}
```

### 请求参数说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `ids` | int[] | 是 | 要删除的优惠券ID列表 |

### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `code` | int | 是 | 状态码 |
| `message` | string | 是 | 接口返回说明信息 |
| `data.deletedCount` | int | 是 | 实际删除的优惠券数量 |

## 7. 错误码说明
| 错误码 | 说明 |
|--------|------|
| 200 | 请求成功 |
| 400 | 请求参数错误 |
| 404 | 资源不存在 |
| 500 | 服务器内部错误 |

## 8. 业务规则说明
- 优惠券类型为"fixed"时，value必须大于0（表示满减金额，如5表示减5元）
- 优惠券类型为"discount"时，value应在0-1之间（表示折扣比例，如0.8表示8折）
- 满减券的最低消费金额必须大于优惠金额
- 折扣券的折扣比例必须在0-1之间
- 已使用的优惠券不能修改
- 优惠券ID由后端自动生成

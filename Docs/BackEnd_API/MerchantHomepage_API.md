# 商家首页接口

## 1. 店铺概况接口
- **接口名称**: shopOverview
- **接口描述**: 用来获得商家的店铺概况信息，包括评分、月销量、营业状态等
- **接口地址(URL)**: `/api/shop/overview`
- **请求方式**: `GET`

### 返回结果
- 成功
```json
{
  "data": {
    "rating": 4.6,
    "monthlySales": 1350,
    "isOpen": true,
    "creditScore": 95
  }
}
```
- 输入有误
```json
{
  "code": 400,
  "message": "商家ID无效"
}
```
- 无返回结果
```json
{
  "error": "商家不存在",
  "details": "堆栈跟踪信息"
}
```

### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `code` | int | 是 | 状态码 |
| `message` | string | 是 | 接口返回说明信息 |
| `data.rating` | decimal(10, 2) | 是 | 店铺平均评分 |
| `data.monthlySales` | int | 是 | 店铺月销量 |
| `data.isOpen` | boolean | 是 | 店铺营业状态（true=营业中，false=已停业） |
| `data.creditScore` | int | 是 | 商家信誉积分 |

## 2. 店铺信息接口
- **接口名称**: shopInfo
- **接口描述**: 用来获得商家的详细店铺信息
- **接口地址(URL)**: `/api/shop/info`
- **请求方式**: `GET`

### 返回结果
- 成功
```json
{
  "data": {
    "storeID": 101,
    "storeName": "海底捞火锅",
    "address": "北京市朝阳区三里屯路8号",
    "startTime": "08:00:00",
    "endTime": "22:00:00",
    "feature": "特色火锅店，服务一流",
    "isOpen": true
  }
}
```
- 输入有误
```json
{
  "code": 400,
  "message": "商家ID无效"
}
```
- 无返回结果
```json
{
  "error": "店铺不存在",
  "details": "堆栈跟踪信息"
}
```

### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `code` | int | 是 | 状态码 |
| `message` | string | 是 | 接口返回说明信息 |
| `data.storeID` | int | 是 | 店铺编号 |
| `data.storeName` | string | 是 | 店铺名称 |
| `data.address` | string | 是 | 店铺地址 |
| `data.startTime` | string | 是 | 营业开始时间（HH:mm:ss格式） |
| `data.endTime` | string | 是 | 营业结束时间（HH:mm:ss格式） |
| `data.feature` | string | 是 | 店铺特色描述 |
| `data.isOpen` | boolean | 是 | 店铺营业状态 |

## 3. 商家信息接口
- **接口名称**: merchantInfo
- **接口描述**: 用来获得商家的个人信息
- **接口地址(URL)**: `/api/merchant/info`
- **请求方式**: `GET`

### 返回结果
- 成功
```json
{
  "data": {
    "sellerID": 3,
    "username": "zhanglaosan",
    "email": "zhang@example.com",
    "phone": "13800138000",
    "createTime": "2023-05-20T12:34:56"
  }
}
```
- 输入有误
```json
{
  "code": 400,
  "message": "商家ID无效"
}
```

### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `code` | int | 是 | 状态码 |
| `message` | string | 是 | 接口返回说明信息 |
| `data.sellerID` | int | 是 | 商家编号 |
| `data.username` | string | 是 | 商家用户名 |
| `data.email` | string | 是 | 商家邮箱 |
| `data.phone` | string | 是 | 商家手机号 |
| `data.createTime` | string | 是 | 商家注册时间（ISO 8601格式） |

## 4. 切换营业状态接口
- **接口名称**: toggleBusinessStatus
- **接口描述**: 用来切换店铺的营业状态
- **接口地址(URL)**: `/api/shop/status`
- **请求方式**: `PATCH`

### 请求参数
```json
{
  "isOpen": true
}
```

### 返回结果
- 成功
```json
{
  "data": {
    "success": true,
    "message": "店铺已开启营业"
  }
}
```
- 输入有误
```json
{
  "code": 400,
  "message": "请求数据验证失败: IsOpen字段不能为空"
}
```

### 请求参数说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `isOpen` | boolean | 是 | 营业状态（true=开启营业，false=关闭营业） |

### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `code` | int | 是 | 状态码 |
| `message` | string | 是 | 接口返回说明信息 |
| `data.success` | boolean | 是 | 操作是否成功 |
| `data.message` | string | 是 | 操作结果描述 |

## 5. 更新店铺字段接口
- **接口名称**: updateShopField
- **接口描述**: 用来更新店铺的特定字段信息
- **接口地址(URL)**: `/api/shop/update-field`
- **请求方式**: `PATCH`

### 请求参数
```json
{
  "field": "storeName",
  "value": "新店铺名称"
}
```

### 返回结果
- 成功
```json
{
  "data": {
    "success": true,
    "message": "店铺信息更新成功"
  }
}
```
- 输入有误
```json
{
  "code": 400,
  "message": "请求数据验证失败: Field字段不能为空"
}
```

### 请求参数说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `field` | string | 是 | 要更新的字段名（storeName/address/feature） |
| `value` | string | 是 | 新的字段值 |

### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `code` | int | 是 | 状态码 |
| `message` | string | 是 | 接口返回说明信息 |
| `data.success` | boolean | 是 | 操作是否成功 |
| `data.message` | string | 是 | 操作结果描述 |

## 6. 数据库连接测试接口
- **接口名称**: testDatabaseConnection
- **接口描述**: 用来测试数据库连接状态（调试用）
- **接口地址(URL)**: `/api/merchant/test-db-connection`
- **请求方式**: `GET`

### 返回结果
- 成功
```json
{
  "success": true,
  "message": "数据库连接成功",
  "storeCount": 5,
  "sellerCount": 3
}
```
- 连接失败
```json
{
  "success": false,
  "message": "无法连接到数据库"
}
```

### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `success` | boolean | 是 | 连接是否成功 |
| `message` | string | 是 | 连接状态描述 |
| `storeCount` | int | 否 | 店铺总数（连接成功时返回） |
| `sellerCount` | int | 否 | 商家总数（连接成功时返回） |

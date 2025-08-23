# 用户访问店铺接口

## 1. 商家接口
- **接口名称**: storeInfo
- **接口描述**: 用来获得商家的详细信息
- **接口地址(URL)**: `/api/user/store/storeInfo`
- **请求方式**: `GET`

### 返回结果
- 成功
```json
{
  "storeId": 1,
  "storeName": "海底捞火锅",
  "storeImage": "/images/store/1.png",
  "storeAddress": "北京市朝阳区三里屯路 8 号",
  "openTime": "10:00:00",
  "closeTime": "22:00:00",
  "averageRating": 4.6,
  "monthlySales": 1350,
  "storeDiscription": "特色火锅店，服务一流",
  "storeCreationTime": "2023-05-20T12:34:56"
}
```
- 输入有误
```json
{
  "code": 400,
  "message": "店铺编号无效"
}
```
- 无返回结果
```json
{
  "message": "店铺不存在"
}
```

### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `code` | int | 是 | 状态码 |
| `message` | string | 是 | 接口返回说明信息 |
| `storeId` | int | 店铺编号  |
| `storeName` | string | 店铺名称 |
| `storeImage` | string | 店铺封面图片 URL |
| `storeAddress` | string | 店铺地址 |
| `openTime` | string || 开店时间 |
| `closeTime`| string || 打烊时间 |
| `averageRating`        | number      | 店铺评分                        |
| `monthlySales`  | number      | 店铺月销售量                     |
| `storeDiscription`   | string      | 店铺描述                        |
| `createTime`    | string      | 店铺创建时间（格式如 YYYY-MM-DD HH:mm:ss） |

## 2. 菜单接口
- **接口名称**: dish
- **接口描述**: 用来获得商家的菜单信息
- **接口地址(URL)**: `/api/user/store/dish`
- **请求方式**: `GET`

### 返回结果
- 成功
```json
[
  {
    "dishId": 101,
    "dishCategoryId": 0,
    "dishName": "番茄牛腩锅底",
    "description": "浓郁番茄汤，搭配牛腩",
    "price": 88.00,
    "dishimage": "/images/dish/101.png",
    "isSoldOut": false
  },
  {
    "dishId": 102,
    "dishCategoryId": 0,
    "dishName": "毛肚",
    "description": "新鲜毛肚，口感脆爽",
    "price": 36.00,
    "dishimage": "/images/dish/102.png",
    "isSoldOut": false
  }
]
```
- 输入有误
```json
{
  "message": "参数无效",
}
```

### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `code` | int | 是 | 状态码 |
| `message` | string | 是 | 接口返回说明信息 |



## 3. 评价接口
- **接口名称**: commentList
- **接口描述**: 用来获得商家的评价信息
- **接口地址(URL)**: `/api/user/store/commentList`
- **请求方式**: `GET`

### 返回结果
- 成功
```json
{
  "comments": [
    {
      "commentId": 201,
      "username": "小明",
      "rating": 5,
      "postedAt": "2023-11-10T18:23:00",
      "content": "味道很棒，服务也很好！",
      "avatar": "/images/user/1.png",
      "commentImage": []
    },
    {
      "commentId": 202,
      "username": "小红",
      "rating": 3,
      "postedAt": "2023-11-08T12:00:00",
      "content": "一般般，等位时间太长",
      "avatar": "/images/user/2.png",
      "commentImage": []
    }
  ]
}
```
- 输入有误
```json
{
  "message": "店铺编号无效"
}
```
- 无返回结果
```json

```

### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `code` | int | 是 | 状态码 |
| `message` | string | 是 | 接口返回说明信息 |



## 4. 评价状态接口
- **接口名称**: commentStatus
- **接口描述**: 用来获得商家的评价状态信息
- **接口地址(URL)**: `/api/user/store/commentStatus`
- **请求方式**: `GET`

### 返回结果
- 成功
```json
{
  "status": [20, 5, 3]
}
```
- 输入有误
```json
{
  "message": "店铺编号无效"
}
```

### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `code` | int | 是 | 状态码 |
| `message` | string | 是 | 接口返回说明信息 |
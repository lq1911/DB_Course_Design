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
  "businessHours": "08:00-22:00",
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
| `Id` | int | 是 | 店铺编号  |
| `Name` | string | 是 | 店铺名称 |
| `Image` | string | 是 | 店铺封面图片 URL |
| `Address` | string | 是 | 店铺地址 |
| `BusinessHours` | string | 是 | 营业时间 |
| `Rating` | decimal(10, 2) | 是 | 店铺评分 |
| `MonthlySales`  | int | 是 | 店铺月销售量 |
| `Discription` | string | 是 | 店铺描述 |
| `CreateTime` | DateTime | 是 | 店铺创建时间（格式如 YYYY-MM-DD HH:mm:ss） |

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
    "id": 101,
    "categoryId": 0,
    "name": "番茄牛腩锅底",
    "description": "浓郁番茄汤，搭配牛腩",
    "price": 88.00,
    "image": "/images/dish/101.png",
    "isSoldOut": false
  },
  {
    "Id": 102,
    "categoryId": 0,
    "name": "毛肚",
    "description": "新鲜毛肚，口感脆爽",
    "price": 36.00,
    "image": "/images/dish/102.png",
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
| `id`           | number      | 是 |菜品编号                       |
| `categoryId`   | number      | 是 |菜品分类编号                   |
| `name`         | string      | 是 |菜品名称                       |
| `description`  | string      | 是 |菜品描述                       |
| `price`        | number      | 是 |菜品价格                       |
| `image`        | string      | 是 |菜品图片 URL                    |
| `isSoldOut`    | number      | 是 |是否售罄（0=在售, 1=售罄）    |


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
      "id": 201,
      "username": "小明",
      "rating": 5,
      "date": "2023-11-10T18:23:00",
      "content": "味道很棒，服务也很好！",
      "avatar": "/images/user/1.png",
      "images": []
    },
    {
      "id": 202,
      "username": "小红",
      "rating": 3,
      "date": "2023-11-08T12:00:00",
      "content": "一般般，等位时间太长",
      "avatar": "/images/user/2.png",
      "images": []
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

### 响应说明
| 参数名 | 类型 | 是否必填 | 说明 |
|--------|------|----------|------|
| `code` | int | 是 | 状态码 |
| `message` | string | 是 | 接口返回说明信息 |
| `comments`  | Comment[]   | 评论列表，每个元素为 Comment 类型 |

> 以下为上面评论数组中`Comment`对象的具体属性

| 参数名  | 类型  | 是否必填 | 说明                          |
| -------- | ----------- | ----------------------------- |
| `id`       | int      | 评论编号                      |
| `username` | string      | 评论用户昵称                  |
| `rating`   | int      | 评分（Range 1-5）                |
| `date`     | string      | 评论日期  |
| `content`  | string      | 评论内容                      |
| `avatar`   | string      | 用户头像 URL                  |
| `images`   | string[]    | 评论附带图片列表              |



## 4. 评价状态接口
- **接口名称**: commentStatus
- **接口描述**: 用来获得商家的评价状态信息
- **接口地址(URL)**: `/api/user/store/commentStatus`
- **请求方式**: `GET`

### 返回结果
- 成功
```json
{
  "status": [10, 20, 5, 3, 1]
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
| 字段名   | 类型        | 说明                          |
| -------- | ----------- | ----------------------------- |
| `status`   | number[]    | 各类评论数量状态数组，长度为5 |
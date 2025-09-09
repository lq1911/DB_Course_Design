# **商家订单售后接口文档**

## **1. 获取处罚记录列表接口**

- **接口名称**：FetchPenaltyList
- **接口描述**：获取当前商家的处罚记录列表，支持关键词搜索
- **接口地址**：GET /api/penalties
- **请求方式**：GET

**请求参数：**

|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|keyword|string|否|处罚编号/原因关键词|

**返回结果：**

```
{
    "code": 200,
    "success": true,
    "message": null,
    "data": [
      {
        "id": "PEN20241201001",
        "reason": "食品安全问题",
        "time": "2024-11-15 16:30:00",
        "merchantAction": "整改厨房卫生",
        "platformAction": "警告处理"
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
|data|object[]|是|处罚记录列表|
|data[].id|string|是|处罚编号|
|data[].reason|string|是|处罚原因|
|data[].time|string|是|处罚时间|
|data[].merchantAction|string|是|商家处罚措施|
|data[].platformAction|string|是|平台处罚措施|


## **2.获取处罚详情接口**

- **接口名称**：FetchPenaltyDetail
- **接口描述**：根据处罚编号获取处罚详情
- **接口地址**：GET /api/penalties/{id}
- **请求方式**：GET

**请求参数:**

|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|id|string|是|处罚编号|

**返回结果：**

同处罚记录列表的单个处罚返回结果。

**响应结果：**

同处罚记录列表的单个处罚数据结构。

## **3.处罚申诉接口**

- **接口名称**：AppealPenalty
- **接口描述**：提交处罚申诉
- **接口地址**：POST /api/penalties/{id}/appeal
- **请求方式**：POST

**请求参数：**

|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|id|string|是|处罚编号|

**请求体**：

```
{
    "reason": "实际卫生整洁，与处罚中内容不符"
}
```

**返回结果：**

```
{
    "code": 200,
    "success": true,
    "message": null,
    "data": [
      {
         "success": true,
         "message": "申诉提交成功"
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
|data|object[]|是|处罚申诉列表|
|data[].success|boolean|是|处罚申诉是否成功|
|data[].message|string|是|处罚申诉信息|


## **4.获取评论列表接口**

- **接口名称**：FetchReviewList
- **接口描述**：分页获取用户评论列表，支持关键词搜索
- **接口地址**：GET /api/reviews
- **请求方式**：GET

**请求参数：**

|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|page|number|是|页码，从1开始|
|pageSize|number|是|每页数量|
|keyword|string|否|内容/订单号关键词|

**返回结果：**

```
{
    "code": 200,
    "success": true,
    "message": null,
    "data": {
      "list": [
        {
          "id": 1,
          "orderNo": "ORD20240601001",
          "user": {
            "name": "美食达人",
            "phone": "13800000001",
            "avatar": "https://randomuser.me/api/portraits/men/32.jpg"
          },
          "content": "菜品新鲜美味，配送很快，五星好评！",
          "createdAt": "2024-06-01 12:30:00"
        }
      ],
      "total": 10
    }
}
```

**响应结果：**

|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|code|int|是|状态码|
|success|boolean|是|是否成功|
|message|string|否|错误信息|
|data|object|是|分页数据|
|data.list|object[]|是|评论列表|
|data.total|number|是|总条数|
|data.list[].id|number|是|评论ID|
|data.list[].orderNo|string|是|订单号|
|data.list[].user|object|是|用户信息|
|data.list[].content|string|是|评论内容|
|data.list[].createdAt|string|是|评论时间|


## **5.回复评论接口**

- **接口名称**：ReplyReview
- **接口描述**：回复指定评论
- **接口地址**：POST /api/reviews/{id}/reply
- **请求方式**：POST

**请求参数：**

|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|id|string|是|评论ID|

**请求体:**

```
{
    "content": "收到上述建议，我们会加以改正"
}
```

**返回结果：**

```
{
    "code": 200,
    "success": true,
    "message": null,
    "data": [
      {
         "success": true,
         "message": "回复成功"
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
|data|object[]|是|回复评论列表|
|data[].success|boolean|是|回复评论是否成功|
|data[].message|string|是|回复评论信息|


## **6.获取售后申请列表接口**

- **接口名称**：FetchAfterSaleList
- **接口描述**：分页获取售后申请列表，支持关键词搜索
- **接口地址**：GET /api/after-sales
- **请求方式**：GET

**请求参数：**

|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|page|number|是|页码，从1开始|
|pageSize|number|是|每页数量|
|keyword|string|否|订单号/用户关键词|

**返回结果：**

```
{
    "code": 200,
    "success": true,
    "message": null,
    "data": {
      "list": [
        {
          "id": 101,
          "orderNo": "ORD20240602001",
          "user": {
            "name": "赵六",
            "phone": "13800000006",
            "avatar": "https://randomuser.me/api/portraits/men/12.jpg"
          },
          "reason": "口味不合适，申请退款",
          "createdAt": "2024-06-02 11:20:00"
        }
      ],
      "total": 5
    }
}
```

**响应结果：**

|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|code|int|是|状态码|
|success|boolean|是|是否成功|
|message|string|否|错误信息|
|data|object|是|分页数据|
|data.list|object[]|是|售后列表|
|data.total|number|是|总条数|
|data.list[].id|number|是|售后ID|
|data.list[].orderNo|string|是|订单号|
|data.list[].user|object|是|用户信息|
|data.list[].content|string|是|售后内容|
|data.list[].createdAt|string|是|售后时间|


## **7.获取售后申请详情接口**

- **接口名称**：FetchAfterSaleDetail
- **接口描述**：根据申请ID获取售后申请详情
- **接口地址**：GET /api/after-sales/{id}
- **请求方式**：GET

**请求参数**：

|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|id|string|是|申请ID|

**返回结果**：

同售后记录列表的单个售后返回结果。

**响应结果：**

同售后记录列表的单个售后数据结构。

## **8.处理售后申请接口**

- **接口名称**：DecideAfterSale
- **接口描述**：处理售后申请（通过/拒绝/协商）
- **接口地址**：POST /api/after-sales/{id}/decide
- **请求方式**：POST

**请求参数**：

|**参数名**|**类型**|**是否必填**|**说明**|
| :- | :- | :- | :- |
|id|string|是|申请ID|

**请求体：**

```
{
    "action": "approve",
    "remark": "予以退款处理"
}
```

**返回结果：**

```
{
    "code": 200,
    "success": true,
    "message": null,
    "data": [
      {
         "success": true,
         "message": "处理成功"
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
|data|object[]|是|售后处理列表|
|data[].success|boolean|是|售后处理是否成功|
|data[].message|string|是|售后处理信息|


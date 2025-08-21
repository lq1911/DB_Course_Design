**商家信息接口文档**

**1. 获取商家信息接口**

- **接口名称**：FetchMerchantInfo
- **接口描述**：获取当前登录商家的详细信息，包括基本资料、注册时间和账号状态等
- **接口地址 (URL)**：GET /api/merchant/profile
- **请求方式**：GET

**请求参数**：无（通过登录态 token 获取当前商家身份）

**返回结果**

{

`  `"code": 200,

`  `"success": true,

`  `"message": null,

`  `"data": {

`    `"id": "1001",

`    `"name": "张三餐厅",

`    `"phone": "13800138000",

`    `"email": "zhangsan@example.com",

`    `"registerTime": "2023-01-15 09:30:00",

`    `"status": "正常营业"

`  `}

}

**响应说明**

|**参数名**|**类型**|**是否必填**|**说明**|
| :-: | :-: | :-: | :-: |
|code|int|是|状态码|
|success|boolean|是|请求是否成功，true 表示成功，false 表示失败|
|message|string|否|错误时的提示信息，成功时为 null|
|data|object|是|商家信息数据对象|
|data.id|string|是|商家 ID|
|data.name|string|是|商家名称（对应用户名）|
|data.phone|string|是|联系电话|
|data.email|string|是|电子邮箱|
|data.registerTime|string|是|注册时间，格式为 YYYY-MM-DD HH:mm:ss|
|data.status|string|是|账号状态，可选值为 "正常营业" 或 "封禁中"|

**2. 更新商家信息接口**

- **接口名称**：SaveShopInfo
- **接口描述**：更新当前登录商家的联系电话和电子邮箱信息
- **接口地址 (URL)**：PUT /api/merchant/profile
- **请求方式**：PUT

**请求体参数**

|**参数名**|**类型**|**是否必填**|**说明**|
| :-: | :-: | :-: | :-: |
|phone|string|是|新的联系电话|
|email|string|是|新的电子邮箱|

**请求示例**

{

`  `"phone": "13900139000",

`  `"email": "newhost@example.com"

}

**返回结果**

成功更新时：

{

`  `"code": 200,

`  `"success": true,

`  `"message": null,

`  `"data": {

`    `"updatedFields": ["phone", "email"],

`    `"updateTime": "2023-10-20 15:45:30"

`  `}

}

无更新内容时：

{

`  `"code": 200,

`  `"success": true,

`  `"message": "没有需要更新的信息",

`  `"data": null

}

更新失败时：

{

`  `"code": 400,

`  `"success": false,

`  `"message": "用户不存在",

`  `"data": null

}

响应说明

|**参数名**|**类型**|**是否必填**|**说明**|
| :-: | :-: | :-: | :-: |
|code|int|是|状态码|
|success|boolean|是|更新是否成功，true 表示成功，false 表示失败|
|message|string|否|失败时的错误信息或无更新时的提示信息|
|data|object|否|更新成功时返回的数据对象，失败或无更新时为 null|
|data.updatedFields|string[]|否|成功更新的字段列表，可能包含 "phone" 和 / 或 "email"|
|data.updateTime|string|否|更新时间，格式为 YYYY-MM-DD HH:mm:ss|


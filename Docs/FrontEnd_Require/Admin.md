## 管理员后台页面前端API说明文档 (最终完整版)
### 页面描述
管理员后台是系统的核心管理界面，用于处理售后申请、用户投诉、违规举报和评论审核，并允许管理员查看和修改自己的个人信息。
### 页面路径
`/admin`
### 页面功能点

信息展示: 页面加载时，自动获取并展示当前登录管理员的个人信息、售后列表、投诉列表、违规列表和评论列表。

数据筛选与搜索: 对各类列表提供状态筛选和关键词搜索功能。

详情查看与处理: 点击各项记录的“查看详情”按钮，可以弹出模态框进行详细信息查看和处理操作（如执行处罚、审核评论等）。

个人信息修改: 管理员可以在“管理员信息”标签页修改自己的个人资料并保存。

登出: 点击右上角登出按钮，清除登录状态并返回登录页。

### 调用接口
通用说明: 以下所有接口都需要在请求的 Authorization 头部附带登录时获取的 Bearer Token 进行身份验证。所有请求的 API 基础路径 为 `http://localhost:5250/api`。

#### 1. 获取当前管理员信息
接口说明: 获取当前登录的管理员的详细个人信息。
请求方式: GET
请求URL: /api/admin/info
请求参数说明: 无
返回参数说明 (data 字段): 直接返回一个 AdminInfo 对象。
| 字段名 | 类型 | 是否必填 | 说明 |
| :--- | :--- | :--- | :--- |
| id | string | 是 | 管理员ID |
| username | string | 是 | 用户名 (登录名) |
| realName| string | 是 | 真实姓名 |
| role | string | 是 | 角色 |
| registrationDate| string | 是 | 注册日期 (格式: "YYYY-MM-DD") |
| avatarUrl | string | 是 | 头像图片的URL |
| phone | string | 是 | 手机号码 |
| email | string | 是 | 电子邮箱 |
| gender | string | 是 | 性别 ("男" 或 "女") |
| birthDate | string | 是 | 生日 (格式: "YYYY-MM-DD") |
| managementScope| string | 是 | 管理范围描述 |
| averageRating | number | 是 | 处理事项获评均分 |
| isPublic | boolean | 是 | 是否公开个人信息 |
#### 2. 更新当前管理员信息
接口说明: 提交修改后的管理员个人信息以进行更新。
请求方式: PUT
请求URL: /api/admin/info
请求参数说明 (Request Body): 发送一个完整的 AdminInfo JSON对象作为请求体。
返回参数说明: 返回一个包含操作结果和数据的对象。
| 字段名 | 类型 | 是否必填 | 说明 |
| :--- | :--- | :--- | :--- |
| success | boolean| 是 | true 表示更新成功，false 表示失败。|
| data | AdminInfo| 是 | 更新成功后，返回最新的 AdminInfo 对象。|
#### 3. 获取售后申请列表
接口说明: 获取所有需要当前管理员处理的售后申请列表。
请求方式: GET
请求URL: /api/admin/after-sales
返回参数说明 (data 字段): 直接返回一个 AfterSaleItem 对象数组。数组中每个 AfterSaleItem 对象的结构如下：
| 字段名 | 类型 | 是否必填 | 说明 |
| :--- | :--- | :--- | :--- |
| applicationId| string | 是 | 申请编号 |
| orderId | string | 是 | 关联的订单编号 |
| applicationTime| string | 是 | 申请时间 |
| description | string | 是 | 问题描述 |
| status | string | 是 | 状态 ("待处理" 或 "已完成") |
| punishment | string | 是 | 最终处理措施 |
| punishmentReason| string | 否 | 处罚原因 |
| processingNote | string | 否 | 处理备注 |
#### 4. 更新售后申请状态
接口说明: 管理员对某一个售后申请进行处理和状态更新。
请求方式: PUT
请求URL: /api/admin/after-sales/{applicationId}
请求参数说明 (URL参数): applicationId (string) - 需要更新的售后申请的ID。
请求参数说明 (Request Body): 发送一个完整的 AfterSaleItem JSON对象作为请求体。
返回参数说明: 返回一个包含操作结果和数据的对象。
| 字段名 | 类型 | 是否必填 | 说明 |
| :--- | :--- | :--- | :--- |
| success | boolean| 是 | true 表示更新成功，false 表示失败。|
| data | AfterSaleItem| 是 | 更新成功后，返回最新的 AfterSaleItem 对象。|
#### 5. 获取投诉列表
接口说明: 获取所有分配给当前管理员的投诉列表。
请求方式: GET
请求URL: /api/admin/complaints
返回参数说明 (data 字段): 直接返回一个 ComplaintItem 对象数组。数组中每个 ComplaintItem 对象的结构如下：
| 字段名 | 类型 | 是否必填 | 说明 |
| :--- | :--- | :--- | :--- |
| complaintId | string | 是 | 投诉编号 |
| target | string | 是 | 投诉对象 |
| content | string | 是 | 投诉内容 |
| applicationTime| string | 是 | 申请时间 |
| status | string | 是 | 状态 ("待处理" 或 "已完成") |
| punishment | string | 是 | 最终处理措施 |
| punishmentReason| string | 否 | 处罚原因 |
| processingNote | string | 否 | 处理备注 |
#### 6. 更新投诉状态
接口说明: 管理员处理某一个投诉并更新其状态。
请求方式: PUT
请求URL: /api/admin/complaints/{complaintId}
请求参数说明 (URL参数): complaintId (string) - 需要更新的投诉的ID。
请求参数说明 (Request Body): 发送一个完整的 ComplaintItem JSON对象作为请求体。
返回参数说明: 返回一个包含操作结果和数据的对象。
| 字段名 | 类型 | 是否必填 | 说明 |
| :--- | :--- | :--- | :--- |
| success | boolean| 是 | true 表示更新成功，false 表示失败。|
| data | ComplaintItem| 是 | 更新成功后，返回最新的 ComplaintItem 对象。|
#### 7. 获取违规举报列表
接口说明: 获取所有需要当前管理员处理的违规举报列表。
请求方式: GET
请求URL: /api/admin/violations
返回参数说明 (data 字段): 直接返回一个 ViolationItem 对象数组。数组中每个 ViolationItem 对象的结构如下：
| 字段名 | 类型 | 是否必填 | 说明 |
| :--- | :--- | :--- | :--- |
| punishmentId | string | 是 | 处罚编号 |
| storeName | string | 是 | 违规的店铺名称 |
| reason | string | 是 | 违规原因 |
| merchantPunishment| string | 是 | 对商家的处罚措施 |
| storePunishment | string | 是 | 对店铺的处罚措施 |
| punishmentTime | string | 是 | 处罚时间 |
| status | string | 是 | 状态 ("待处理", "执行中" 或 "已完成") |
| processingNote | string | 否 | 处理备注 |
#### 8. 更新违规举报状态
接口说明: 管理员处理某个违规举报并更新其状态。
请求方式: PUT
请求URL: /api/admin/violations/{punishmentId}
请求参数说明 (URL参数): punishmentId (string) - 需要更新的违规处罚的ID。
请求参数说明 (Request Body): 发送一个完整的 ViolationItem JSON对象作为请求体。
返回参数说明: 返回一个包含操作结果和数据的对象。
| 字段名 | 类型 | 是否必填 | 说明 |
| :--- | :--- | :--- | :--- |
| success | boolean| 是 | true 表示更新成功，false 表示失败。|
| data | ViolationItem| 是 | 更新成功后，返回最新的 ViolationItem 对象。|
#### 9. 获取待审核评论列表
接口说明: 获取所有需要当前管理员审核的评论列表。
请求方式: GET
请求URL: /api/admin/reviews
返回参数说明 (data 字段): 直接返回一个 ReviewItem 对象数组。数组中每个 ReviewItem 对象的结构如下：
| 字段名 | 类型 | 是否必填 | 说明 |
| :--- | :--- | :--- | :--- |
| reviewId | string | 是 | 评论ID |
| username | string | 是 | 发表评论的用户名 |
| storeName | string | 是 | 被评论的店铺名称 |
| content | string | 是 | 评论内容 |
| rating | number | 是 | 评分 (1-5) |
| submitTime | string | 是 | 评论提交时间 |
| status | string | 是 | 状态 ("待处理" 或 "已完成") |
| punishment | string | 是 | 最终处理措施 |
| punishmentReason| string | 否 | 处理原因 |
| processingNote | string | 否 | 处理备注 |
#### 10. 更新评论审核状态
接口说明: 管理员审核某条评论并更新其状态。
请求方式: PUT
请求URL: /api/admin/reviews/{reviewId}
请求参数说明 (URL参数): reviewId (string) - 需要审核的评论的ID。
请求参数说明 (Request Body): 发送一个完整的 ReviewItem JSON对象作为请求体。
返回参数说明: 返回一个包含操作结果和数据的对象。
| 字段名 | 类型 | 是否必填 | 说明 |
| :--- | :--- | :--- | :--- |
| success | boolean| 是 | true 表示更新成功，false 表示失败。|
| data | ReviewItem| 是 | 更新成功后，返回最新的 ReviewItem 对象。|
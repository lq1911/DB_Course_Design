# Oracle 说明文档

## 1 表设计

### 1.1 用户 User 表

| 列名                | 数据类型      | 约束        | 描述                                        |
| ------------------- | ------------- | ----------- | ------------------------------------------- |
| UserID              | NUMBER(10,0)  | 主键，自增  | 用户ID                                      |
| Username            | VARCHAR2(15)  | 非空，唯一  | 用户名（用于登录）                          |
| Password            | VARCHAR2(10)  | 非空        | 密码（用于登录）                            |
| PhoneNumber         | NUMBER(19,0)  | 非空，唯一  | 手机号码                                    |
| Email               | VARCHAR2(30)  | 非空，唯一  | 电子邮箱                                    |
| Gender              | VARCHAR2(2)   | 允许为空    | 性别                                        |
| FullName            | VARCHAR2(6)   | 允许为空    | 真实姓名                                    |
| Avatar              | VARCHAR2(255) | 允许为空    | 头像图片URL                                 |
| Birthday            | DATE          | 允许为空    | 出生日期                                    |
| AccountCreationTime | DATE          | 非空        | 账户创建时间                                |
| IsProfilePublic     | NUMBER(10,0)  | 非空，默认0 | 资料公开状态 (0:私密, 1:公开)               |
| Role                | NUMBER(10,0)  | 非空，默认0 | 用户角色 (0:顾客, 1:骑手, 2:商家, 3:管理员) |

说明：
- UserID 为主键，自增，唯一标识用户。
- Username、Password、PhoneNumber、Email 为用户登录及联系信息。
- Gender、FullName、Avatar、Birthday 为用户的可选个人信息。
- IsProfilePublic 表示资料是否公开。
- Role 表示用户角色类型。

### 1.2 商家 Seller 表

| 列名                   | 数据类型     | 约束                    | 描述                      |
| ---------------------- | ------------ | ----------------------- | ------------------------- |
| UserID                 | NUMBER(10,0) | 主键，外键(User.UserID) | 商家ID（参照用户表）      |
| SellerRegistrationTime | DATE         | 非空                    | 注册时间                  |
| ReputationPoints       | NUMBER(10,0) | 默认0                   | 信誉积分                  |
| BanStatus              | NUMBER(10,0) | 非空，默认0             | 封禁状态 (0:正常, 1:封禁) |

说明：
- UserID 为主键并引用 User 表。
- ReputationPoints 记录商家信誉积分。
- BanStatus 表示商家是否被封禁。

### 1.3 骑手 Courier 表

| 列名                    | 数据类型     | 约束                    | 描述                 |
| ----------------------- | ------------ | ----------------------- | -------------------- |
| UserID                  | NUMBER(10,0) | 主键，外键(User.UserID) | 骑手ID，继承自用户表 |
| CourierRegistrationTime | DATE         | 非空                    | 注册时间             |
| VehicleType             | VARCHAR2(20) | 非空                    | 配送车型             |
| ReputationPoints        | INTEGER      | 默认0                   | 骑手信誉积分         |
| TotalDeliveries         | INTEGER      | 默认0                   | 总配送量             |
| AvgDeliveryTime         | INTEGER      | 默认0                   | 平均配送时间         |
| AverageRating           | NUMBER(3,2)  | 默认0.00                | 获评均分             |
| MonthlySalary           | INTEGER      | 默认0                   | 月薪                 |

说明：
- UserID 为主键并引用 User 表。
- 记录骑手注册信息、配送能力及评分。

### 1.4 管理员 Administrator 表

| 列名                  | 数据类型     | 约束                    | 描述                   |
| --------------------- | ------------ | ----------------------- | ---------------------- |
| UserID                | NUMBER(10,0) | 主键，外键(User.UserID) | 管理员ID（参照用户表） |
| AdminRegistrationTime | DATE         | 非空                    | 注册时间               |
| ManagedEntities       | VARCHAR2(50) | 非空                    | 管理的实体范围         |
| IssueHandlingScore    | NUMBER(3,2)  | 默认0.00                | 问题处理评分           |

说明：
- UserID 为主键并引用 User 表。
- 记录管理员的注册信息和管理评分。

### 1.5 消费者 Customer 表

| 列名             | 数据类型      | 约束                    | 描述                        |
| ---------------- | ------------- | ----------------------- | --------------------------- |
| UserID           | NUMBER(10,0)  | 主键，外键(User.UserID) | 顾客ID（参照用户表）        |
| DefaultAddress   | VARCHAR2(100) | 允许为空                | 默认收货地址                |
| ReputationPoints | NUMBER(10,0)  | 默认0                   | 信誉积分                    |
| IsMember         | NUMBER(10,0)  | 默认0                   | 会员状态 (0:非会员, 1:会员) |

说明：
- UserID 为主键并引用 User 表。
- DefaultAddress 用于收货。
- ReputationPoints 表示用户信用。
- IsMember 标识会员状态。

### 1.6 店铺 Store 表

| 列名              | 数据类型               | 约束                            | 描述                                |
| ----------------- | ---------------------- | ------------------------------- | ----------------------------------- |
| StoreID           | NUMBER(10,0)           | 主键，自增                      | 店铺ID                              |
| StoreName         | VARCHAR2(50)           | 非空                            | 店铺名称                            |
| StoreAddress      | VARCHAR2(100)          | 非空                            | 店铺地址                            |
| OpenTime          | INTERVAL DAY TO SECOND | 非空，默认 '09:00:00'           | 营业开始时间                        |
| CloseTime         | INTERVAL DAY TO SECOND | 非空，默认 '22:00:00'           | 营业结束时间                        |
| AverageRating     | NUMBER(10,2)           | 默认0.00                        | 平均评分                            |
| MonthlySales      | NUMBER(10,0)           | 非空                            | 月销量                              |
| StoreFeatures     | VARCHAR2(500)          | 非空                            | 店铺特色                            |
| StoreCreationTime | DATE                   | 非空                            | 店铺创建时间                        |
| StoreState        | NUMBER(10,0)           | 非空，默认0                     | 店铺状态 (0:营业中,1:休息,2:已关闭) |
| SellerID          | NUMBER(10,0)           | 非空，外键(Seller.UserID)，唯一 | 所属商家ID                          |

说明：
- StoreID 为主键，自增。
- SellerID 关联商家表。
- OpenTime/CloseTime、StoreState 控制营业时间与状态。
- AverageRating、MonthlySales、StoreFeatures 为店铺统计信息。



### 1.7 菜单 Menu 表

| 列名         | 数据类型     | 约束                      | 描述              |
| ------------ | ------------ | ------------------------- | ----------------- |
| MenuID       | NUMBER(10,0) | 主键，自增                | 菜单ID            |
| Version      | VARCHAR2(50) | 非空                      | 菜单版本号或名称  |
| ActivePeriod | DATE         | 非空                      | 菜单生效时段/日期 |
| StoreID      | NUMBER(10,0) | 非空，外键(Store.StoreID) | 所属店铺ID        |

说明：
- MenuID 为主键，自增。
- StoreID 关联店铺表。

### 1.8 菜品 Dish 表

| 列名        | 数据类型      | 约束        | 描述                     |
| ----------- | ------------- | ----------- | ------------------------ |
| DishID      | NUMBER(10,0)  | 主键，自增  | 菜品ID                   |
| DishName    | VARCHAR2(50)  | 非空        | 菜品名称                 |
| Price       | NUMBER(10,2)  | 非空        | 价格                     |
| Description | VARCHAR2(500) | 非空        | 菜品描述                 |
| IsSoldOut   | NUMBER(10,0)  | 非空，默认1 | 是否售罄 (0:在售,1:售罄) |

说明：
- DishID 为主键，自增。
- IsSoldOut 表示菜品是否售罄。

### 1.9 菜单与菜品关系 Menu–Dish 表

| 列名   | 数据类型     | 约束                            | 描述       |
| ------ | ------------ | ------------------------------- | ---------- |
| MenuID | NUMBER(10,0) | 主键（部分），外键(Menu.MenuID) | 关联菜单ID |
| DishID | NUMBER(10,0) | 主键（部分），外键(Dish.DishID) | 关联菜品ID |

说明：
- MenuID + DishID 组合为主键，表示菜单包含哪些菜品。

### 1.10 出餐订单 FoodOrder 表

| 列名          | 数据类型      | 约束                            | 描述               |
| ------------- | ------------- | ------------------------------- | ------------------ |
| OrderID       | NUMBER(10,0)  | 主键，自增                      | 订单ID             |
| PaymentTime   | DATE          | 非空                            | 支付时间           |
| Remarks       | VARCHAR2(255) | 允许为空                        | 订单备注           |
| Rating        | NUMBER(2,1)   | 允许为空                        | 订单评分 (1.0-5.0) |
| RatingComment | VARCHAR2(500) | 允许为空                        | 评价内容           |
| RatingTime    | DATE          | 允许为空                        | 评价时间           |
| CustomerID    | NUMBER(10,0)  | 非空，外键(Customer.UserID)     | 顾客ID             |
| CartID        | NUMBER(10,0)  | 非空，外键(ShoppingCart.CartID) | 关联购物车ID       |
| StoreID       | NUMBER(10,0)  | 非空，外键(Store.StoreID)       | 店铺ID             |

说明：
- OrderID 为主键，自增。
- CustomerID、CartID、StoreID 关联顾客、购物车和店铺。

### 1.11 购物车 ShoppingCart 表

| 列名            | 数据类型     | 约束                              | 描述         |
| --------------- | ------------ | --------------------------------- | ------------ |
| CartID          | NUMBER(10,0) | 主键，自增                        | 购物车ID     |
| LastUpdatedTime | DATE         | 非空                              | 最后更新时间 |
| TotalPrice      | NUMBER(10,2) | 默认0.00                          | 购物车总价   |
| OrderID         | NUMBER(10,0) | 外键(FoodOrder.OrderID)，允许为空 | 关联订单ID   |

说明：
- CartID 为主键，自增。
- OrderID 关联 FoodOrder，下单后填充。

### 1.12 购物车项 ShoppingCartItem 表

| 列名       | 数据类型     | 约束                            | 描述                 |
| ---------- | ------------ | ------------------------------- | -------------------- |
| ItemID     | NUMBER(10,0) | 主键，自增                      | 购物车项ID           |
| Quantity   | NUMBER(10,0) | 非空                            | 购买数量             |
| TotalPrice | NUMBER(10,2) | 默认0.00                        | 该项总价 (单价*数量) |
| DishID     | NUMBER(10,0) | 非空，外键(Dish.DishID)         | 菜品ID               |
| CartID     | NUMBER(10,0) | 非空，外键(ShoppingCart.CartID) | 所属购物车ID         |

说明：
- ItemID 为主键，自增。
- DishID、CartID 分别关联菜品和购物车。

### 1.13 优惠券 Coupon 表

| 列名            | 数据类型     | 约束                                      | 描述       |
| --------------- | ------------ | ----------------------------------------- | ---------- |
| CouponID        | NUMBER(10,0) | 主键，自增                                | 优惠券ID   |
| CouponState     | NUMBER(10,0) | 非空，默认0                               | 优惠券状态 |
| CouponManagerID | NUMBER(10,0) | 非空，外键(CouponManager.CouponManagerID) | 管理器ID   |
| CustomerID      | NUMBER(10,0) | 非空，外键(Customer.UserID)               | 顾客ID     |
| OrderID         | NUMBER(10,0) | 外键(FoodOrder.OrderID)，允许为空         | 使用订单ID |

说明：
- CouponID 为主键，自增。
- CustomerID 和 CouponManagerID 关联顾客和管理器。

### 1.14 优惠券管理器 CouponManager 表

| 列名            | 数据类型     | 约束                      | 描述         |
| --------------- | ------------ | ------------------------- | ------------ |
| CouponManagerID | NUMBER(10,0) | 主键，自增                | 管理器ID     |
| MinimumSpend    | NUMBER(10,2) | 非空                      | 最低消费金额 |
| DiscountAmount  | NUMBER(10,2) | 非空                      | 优惠金额     |
| ValidFrom       | DATE         | 非空                      | 有效期开始   |
| ValidTo         | DATE         | 非空                      | 有效期结束   |
| StoreID         | NUMBER(10,0) | 非空，外键(Store.StoreID) | 店铺ID       |

说明：
- 管理优惠券的有效期、金额及关联店铺。

### 1.15 配送任务 DeliveryTask 表

| 列名                  | 数据类型     | 约束                        | 描述         |
| --------------------- | ------------ | --------------------------- | ------------ |
| TaskID                | NUMBER(10,0) | 主键，自增                  | 任务ID       |
| EstimatedArrivalTime  | DATE         | 非空                        | 预计到店时间 |
| EstimatedDeliveryTime | DATE         | 非空                        | 预计送达时间 |
| CourierLongitude      | NUMBER(10,6) | 允许为空                    | 骑手经度     |
| CourierLatitude       | NUMBER(10,6) | 允许为空                    | 骑手纬度     |
| PublishTime           | DATE         | 非空                        | 任务发布时间 |
| AcceptTime            | DATE         | 非空                        | 骑手接单时间 |
| CustomerID            | NUMBER(10,0) | 非空，外键(Customer.UserID) | 顾客ID       |
| StoreID               | NUMBER(10,0) | 非空，外键(Store.StoreID)   | 店铺ID       |
| CourierID             | NUMBER(10,0) | 非空，外键(Courier.UserID)  | 骑手ID       |

说明：
- TaskID 为主键，自增。
- CustomerID、StoreID、CourierID 分别关联顾客、店铺和骑手。

### 1.16 配送投诉 DeliveryComplaint 表

| 列名            | 数据类型      | 约束                            | 描述           |
| --------------- | ------------- | ------------------------------- | -------------- |
| ComplaintID     | NUMBER(10,0)  | 主键，自增                      | 投诉ID         |
| ComplaintReason | VARCHAR2(255) | 非空                            | 投诉原因       |
| ComplaintTime   | DATE          | 非空                            | 投诉时间       |
| CourierID       | NUMBER(10,0)  | 非空，外键(Courier.UserID)      | 被投诉骑手ID   |
| CustomerID      | NUMBER(10,0)  | 非空，外键(Customer.UserID)     | 发起投诉顾客ID |
| DeliveryTaskID  | NUMBER(10,0)  | 非空，外键(DeliveryTask.TaskID) | 配送任务ID     |

说明：
- ComplaintID 为主键，自增。
- 与配送任务、骑手和顾客关联。

### 1.17 管理员与配送投诉关系 EvaluateComplaint 表

| 列名        | 数据类型     | 约束                                              | 描述     |
| ----------- | ------------ | ------------------------------------------------- | -------- |
| AdminID     | NUMBER(10,0) | 主键（部分），外键(Administrator.UserID)          | 管理员ID |
| ComplaintID | NUMBER(10,0) | 主键（部分），外键(DeliveryComplaint.ComplaintID) | 投诉ID   |

说明：
- AdminID + ComplaintID 组合为主键，表示管理员对投诉的评估关系。

### 1.18 售后申请 AfterSaleApplication 表

| 列名            | 数据类型      | 约束                          | 描述       |
| --------------- | ------------- | ----------------------------- | ---------- |
| ApplicationID   | NUMBER(10,0)  | 主键，自增                    | 申请ID     |
| Description     | VARCHAR2(255) | 非空                          | 申请内容   |
| ApplicationTime | DATE          | 非空                          | 提交时间   |
| OrderID         | NUMBER(10,0)  | 非空，外键(FoodOrder.OrderID) | 关联订单ID |

说明：
- ApplicationID 为主键，自增。
- OrderID 关联 FoodOrder。

### 1.19 管理员与售后申请关系 EvaluateAfterSale 表

| 列名          | 数据类型     | 约束                                                   | 描述     |
| ------------- | ------------ | ------------------------------------------------------ | -------- |
| AdminID       | NUMBER(10,0) | 主键（部分），外键(Administrator.UserID)               | 管理员ID |
| ApplicationID | NUMBER(10,0) | 主键（部分），外键(AfterSaleApplication.ApplicationID) | 申请ID   |

说明：
- AdminID + ApplicationID 组合为主键，表示管理员对售后申请的评估。

### 1.20 违规店铺处罚 StoreViolationPenalty 表

| 列名          | 数据类型      | 约束                      | 描述             |
| ------------- | ------------- | ------------------------- | ---------------- |
| PenaltyID     | NUMBER(10,0)  | 主键，自增                | 处罚ID           |
| PenaltyReason | VARCHAR2(255) | 非空                      | 处罚原因         |
| PenaltyTime   | DATE          | 非空                      | 处罚时间         |
| SellerPenalty | VARCHAR2(50)  | 允许为空                  | 对商家的处罚措施 |
| StorePenalty  | VARCHAR2(50)  | 允许为空                  | 对店铺的处罚措施 |
| StoreID       | NUMBER(10,0)  | 非空，外键(Store.StoreID) | 关联店铺ID       |

说明：
- PenaltyID 为主键，自增。
- StoreID 关联被处罚的店铺。

### 1.21 管理员与违规店铺处罚关系 Supervise 表

| 列名      | 数据类型     | 约束                                                | 描述     |
| --------- | ------------ | --------------------------------------------------- | -------- |
| AdminID   | NUMBER(10,0) | 主键（部分），外键(Administrator.UserID)            | 管理员ID |
| PenaltyID | NUMBER(10,0) | 主键（部分），外键(StoreViolationPenalty.PenaltyID) | 处罚ID   |

说明：
- AdminID + PenaltyID 组合为主键，表示管理员对处罚的监督关系。

### 1.22 评论 Comment 表

| 列名             | 数据类型      | 约束                              | 描述               |
| ---------------- | ------------- | --------------------------------- | ------------------ |
| CommentID        | NUMBER(10,0)  | 主键，自增                        | 评论ID             |
| Content          | VARCHAR2(500) | 非空                              | 评论内容           |
| PostedAt         | DATE          | 非空                              | 发布时间           |
| Likes            | NUMBER(10,0)  | 默认0                             | 点赞数             |
| Replies          | NUMBER(10,0)  | 默认0                             | 回复数             |
| ReplyToCommentID | NUMBER(10,0)  | 外键(Comment.CommentID)，允许为空 | 回复的评论ID       |
| StoreID          | NUMBER(10,0)  | 非空，外键(Store.StoreID)         | 关联店铺ID         |
| CommenterID      | NUMBER(10,0)  | 非空，外键(Customer.UserID)       | 评论者ID（顾客表） |

说明：
- CommentID 为主键，自增。
- ReplyToCommentID 可为空，实现自关联回复。

### 1.23 管理员与评论审核关系 ReviewComment 表

| 列名       | 数据类型     | 约束                                     | 描述     |
| ---------- | ------------ | ---------------------------------------- | -------- |
| AdminID    | NUMBER(10,0) | 主键（部分），外键(Administrator.UserID) | 管理员ID |
| CommentID  | NUMBER(10,0) | 主键（部分），外键(Comment.CommentID)    | 评论ID   |
| ReviewTime | DATE         | 非空                                     | 审核时间 |

说明：
- AdminID + CommentID 组合为主键，记录管理员审核评论的关系。

### 1.24 收藏夹 FavoritesFolder 表

| 列名       | 数据类型     | 约束                        | 描述       |
| ---------- | ------------ | --------------------------- | ---------- |
| FolderID   | NUMBER(10,0) | 主键，自增                  | 收藏夹ID   |
| FolderName | VARCHAR2(50) | 非空                        | 收藏夹名称 |
| CustomerID | NUMBER(10,0) | 非空，外键(Customer.UserID) | 所属顾客ID |

说明：
- FolderID 为主键，自增。
- CustomerID 关联顾客。

### 1.25 收藏夹项 FavoriteItem 表

| 列名           | 数据类型      | 约束                                 | 描述           |
| -------------- | ------------- | ------------------------------------ | -------------- |
| ItemID         | NUMBER(10,0)  | 主键，自增                           | 收藏项ID       |
| FavoritedAt    | DATE          | 非空                                 | 收藏时间       |
| FavoriteReason | VARCHAR2(500) | 非空                                 | 收藏原因或备注 |
| StoreID        | NUMBER(10,0)  | 非空，外键(Store.StoreID)            | 收藏店铺ID     |
| FolderID       | NUMBER(10,0)  | 非空，外键(FavoritesFolder.FolderID) | 所属收藏夹ID   |

说明：
- ItemID 为主键，自增。
- StoreID 和 FolderID 分别关联店铺和收藏夹。
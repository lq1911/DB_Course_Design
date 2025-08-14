# 用户访问商店说明文档

### 页面描述

页面一共分为三个主要板块，最上方是商家的简短介绍
- **点单:** 显示商家的菜单，提供不同的菜单分类项，用户可以在此将菜品加入购物车
- **评价:** 显示历史用户对商家的评价
- **商家:** 显示商家的详细信息

- **页面路径:** `/store/:storeId`

### 页面功能点

- 点击购物车时，会显示当前购物车内容
- 点击不同菜单分类会显示不同分类下的菜单物品
- 可以点击导航栏切换不同的商店页面
- 能够点击返回按钮回到首页

---

### 调用接口

**1. 商家接口**

- 用来获得商家的详细信息

- 输入表单说明

| 字段名   | 类型   | 是否必填 | 说明 |
| -------- | ------ | -------- | ----- |
| StoreId | int | 是 | 店铺编号 |

- 获得信息说明

| 字段名   | 说明   |
| -------- | ----- |
| Store.StoreID | 店铺编号 |
| Store.StoreName | 店铺名 |
| Store.StoreImage | 店铺头像 |
| Store.StoreAddress | 店铺地址 |
| Store.OpenTime | 上班时间 |
| Store.CloseTime | 下班时间 |
| Store.AverageRating | 店铺评分 |
| Store.MonthlySales | 店铺月销量 |
| Store.Features | 店铺特色 |

**2. 菜单接口**

- 用来获得商家的菜单信息

- 输入信息说明

| 字段名   | 类型   | 是否必填 | 说明 |
| -------- | ------ | -------- | ----- |
| UserId | int | 是 | 消费者编号 |
| StoreId | int | 是 | 店铺编号 |

- 获得信息说明

| 字段名   | 说明   |
| -------- | ----- |
| Menu.MenuID | 菜单编号 |

| 字段名   | 说明   |
| -------- | ----- |
| Dish.DishID | 菜品编号 |
| StoreCatory | 所属菜单分类 |
| Dish.DishName | 菜品名称 |
| Dish.Discripstion | 菜品描述 |
| Dish.Price | 菜品价格 |
| Dish.Image | 菜品图片 |
| Dish.IsSoldOut | 是否售罄 |

**3. 评价接口**

- 用来获得商家的评价信息

- 输入表单说明

| 字段名   | 类型   | 是否必填 | 说明 |
| -------- | ------ | -------- | ----- |
| StoreId | int | 是 | 店铺编号 |

- 获得信息说明

| 字段名   | 说明   |
| -------- | ----- |
| CommentList | 评论数组Array<Comment> |

> 以下为上面评论数组中`Comment`对象的具体属性

| 字段名   | 说明   |
| -------- | ----- |
| Comment.CommentID | 评价ID |
| Custommer.CustommerName | 用户姓名 |
| Comment.Rating | 评价分数 |
| Comment.PostedAt | 评价时间 |
| Comment.Content | 评价内容 |
| Custommer.Avator | 用户头像 |

**4. 评价状态接口**

- 用来获得商家的评价状态信息

- 输入表单说明

| 字段名   | 类型   | 是否必填 | 说明 |
| -------- | ------ | -------- | ----- |
| StoreId | int | 是 | 店铺编号 |

- 获得信息说明

| 字段名   | 说明   |
| -------- | ----- |
| CommentStatus | 返回<number>数组，分别为和1~5分的评论的数量，分数和数组下标对应 |

---

### UI设计稿
- 设计链接：https://readdy.link/share/f017b0693efc1299795a46c253b08633
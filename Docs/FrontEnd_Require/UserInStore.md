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

**1. 菜单接口**

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
| Dish.DishName | 菜品名称 |
| Dish.Price | 菜品价格 |
| Dish.Discripstion | 菜品描述 |
| Dish.IsSoldOut | 是否售罄 |

**2. 购物车接口**

- 用来修改用户的购物车

- 输入表单说明

| 字段名   | 类型   | 是否必填 | 说明 |
| -------- | ------ | -------- | ----- |
| UserId | int | 是 | 消费者编号 |
| StoreId | int | 是 | 店铺编号 |
| Dish.DishId | int | 是 | 菜品编号 |
| ShoppingCartItem.Quantity | int | 是 | 菜品数量 |
| ShoppingCartItem.TotalPrice | int | 是 | 总价格 |
| ShoppingCart.LastUpdateTime | int | 是 | 最后修改时间 |

**3. 评价接口**

- 用来获得商家的评价信息

- 输入表单说明

| 字段名   | 类型   | 是否必填 | 说明 |
| -------- | ------ | -------- | ----- |
| UserId | int | 是 | 消费者编号 |
| StoreId | int | 是 | 店铺编号 |

- 获得信息说明

| 字段名   | 说明   |
| -------- | ----- |
| Comment.CommentID | 评价ID |
| Comment.Content | 评价内容 |
| Comment.Likes | 喜欢 |
| Comment.Replies | 回复信息 |
| Comment.CommenterID | 评价者ID |

**4. 商家接口**

- 用来获得商家的详细信息

- 输入表单说明

| 字段名   | 类型   | 是否必填 | 说明 |
| -------- | ------ | -------- | ----- |
| StoreId | int | 是 | 店铺编号 |

- 获得信息说明

| 字段名   | 说明   |
| -------- | ----- |
| Store.StoreName | 店铺名 |
| Store.StoreAddress | 店铺地址 |
| Store.BusinessHours | 工作时间 |
| Store.AverageRating | 店铺评分 |
| Store.MonthlySales | 店铺月销量 |
| Store.Features | 店铺特色 |

---

### UI设计稿
- 设计链接：https://readdy.link/share/f017b0693efc1299795a46c253b08633
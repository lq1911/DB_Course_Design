# 注册后端说明文档
## 接口基本信息
- 接口路径：/api/register
- 请求方法：POST
- 请求头：
  - Content-Type: application/json （普通用户、骑手、管理员）
  - Content-Type: multipart/form-data （商家，因为要上传文件）
## 请求体示例
- Customer
    ```json
    {
    "nickname": "Tom",
    "password": "123456",
    "confirmPassword": "123456",
    "phone": "13812345678",
    "email": "tom@example.com",
    "gender": "male",
    "birthday": "2000-01-01",
    "verificationCode": "666666",
    "role": "customer",
    "isPublic": 1
    }
    ```
- Rider
    ```json
    {
    "nickname": "Jerry",
    "password": "123456",
    "confirmPassword": "123456",
    "phone": "13912345678",
    "email": "jerry@example.com",
    "gender": "male",
    "birthday": "1998-05-10",
    "verificationCode": "888888",
    "role": "rider",
    "isPublic": 1,
    "riderInfo": {
        "vehicleType": "bike",
        "name": "小李"
    }
    }
    ```
- Admin
    ```json
    {
    "nickname": "Admin001",
    "password": "admin123",
    "confirmPassword": "admin123",
    "phone": "13712345678",
    "email": "admin@example.com",
    "gender": "female",
    "birthday": "1990-07-20",
    "verificationCode": "999999",
    "role": "admin",
    "isPublic": 1,
    "adminInfo": {
        "managementObject": "平台运营",
        "name": "小王"
    }
    }
    ```
- Merchant
    ```js
    const formData = new FormData();
    formData.append("nickname", "ShopOwner");
    formData.append("password", "shop123");
    formData.append("confirmPassword", "shop123");
    formData.append("phone", "13612345678");
    formData.append("email", "shop@example.com");
    formData.append("gender", "male");
    formData.append("birthday", "1985-03-15");
    formData.append("verificationCode", "123456");
    formData.append("role", "merchant");
    formData.append("isPublic", "1");

    // storeInfo 字段
    formData.append("storeInfo.SellerName", "老张");
    formData.append("storeInfo.StoreName", "张记快餐");
    formData.append("storeInfo.Address", "北京市海淀区XX路88号");
    formData.append("storeInfo.OpenTime", "09:00");
    formData.append("storeInfo.CloseTime", "21:00");
    formData.append("storeInfo.EstablishmentDate", "2020-06-01");
    formData.append("storeInfo.Category", "Food");
    formData.append("storeInfo.BusinessLicense", fileInput.files[0]); // 上传文件
    ```
### 响应结果
- 成功
    ```json
    {
    "success": true,
    "code": 201,
    "message": "注册成功！"
    }
    ```
- 失败
    ```json
    {
    "success": false,
    "code": 400,
    "message": "错误信息"
    }
    ```
# 登录后端说明文档
## 接口基本信息
- 接口路径：/api/login
- 请求方法：POST
- 请求头：
  - Content-Type: application/json
## 请求体示例
- 登录请求
```json
{
  "phoneNum": "13812345678",
  "password": "123456",
  "role": "customer"
}
```
- 登陆成功
```json
{
  "success": true,
  "code": 200,
  "message": "登录成功",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6Ikp...", 
  "user": {
    "userId": 1,
    "username": "Tom",
    "role": "customer",
    "avatar": "https://example.com/avatar/tom.png"
  }
}
```
- 登陆失败
```json
{
  "success": false,
  "code": 401,
  "message": "手机号或密码错误"
}
```
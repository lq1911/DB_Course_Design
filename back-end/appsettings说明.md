`appsettings.json` 是 ASP.NET Core 应用程序的**配置文件**，用于存储各种配置信息，比如数据库连接字符串、日志等级、应用设置等。

常见配置内容举例
1. 日志（Logging）
控制应用程序日志的记录等级和行为。

```json
"Logging": {
  "LogLevel": {
    "Default": "Information",
    "Microsoft.AspNetCore": "Warning"
  }
}
```
Default：默认日志级别为 Information，表示记录信息、警告、错误等日志。

Microsoft.AspNetCore：只记录 Warning（警告）及以上日志，减少系统自身日志的冗余。

2. 允许主机（AllowedHosts）
```json
"AllowedHosts": "*"
```
允许所有主机访问（* 表示通配符）

也可以配置特定域名，增强安全性，例如：

```json
"AllowedHosts": "example.com,www.example.com"
```
3. 连接字符串（ConnectionStrings）
存储数据库连接信息，方便程序连接数据库。

```json
"ConnectionStrings": {
  "DefaultConnection": "User Id=hr;Password=123456;Data Source=oracle-db"
}
```
DefaultConnection 是连接字符串名称，可根据需要自定义。

字符串内容因数据库不同而异，比如 SQL Server、Oracle、MySQL 都有不同格式
# TJFeast

> Copyright © 2025 TJFeast Development Team - Licensed under [MIT License](LICENSE)
>
> 版权所有 © 2025 TJFeast | TJFeast 项目开发组 - 采用 [MIT 许可证](LICENSE)授权

![License](https://img.shields.io/badge/license-MIT-green)
![Build](https://img.shields.io/badge/build-passing-brightgreen)
![Version](https://img.shields.io/badge/version-1.0.0-blue)

<!-- 技术栈徽章 -->
![Vue](https://img.shields.io/badge/Vue-3.3.4-brightgreen)
![TypeScript](https://img.shields.io/badge/TypeScript-5.1-blue)
![Tailwind CSS](https://img.shields.io/badge/TailwindCSS-3.3.3-blue)
![Element Plus](https://img.shields.io/badge/ElementPlus-2.3.10-purple)
![C#](https://img.shields.io/badge/C%23-8.0-blue)
![.NET](https://img.shields.io/badge/.NET-8.0-purple)
![EF Core](https://img.shields.io/badge/EF_Core-8.0-orange)

## 项目名称

TJFeast

## 项目简介

TJFeast- A next-generation food delivery platform designed for fast, reliable, and enjoyable dining experiences.

TJFeast：为用户提供快速、可靠且愉快的外卖订餐体验的下一代外卖平台。

![](./front-end/src/assets/project-intro.png)

> ***Relevant course***  
> * Database Course Project 2025 (同济大学数据库课程设计 2025)

The project consists of five main modules-

* Login & Registration Module
* Customer Module
* Seller Module
* Courier Module
* Administrator Module

本项目由五个主要模块组成：

* 登录-注册模块
* 消费者模块
* 商家模块
* 骑手模块
* 管理员模块

## 成员分工

| 姓名 | 学号 | GitHub 主页 | 代码行数 |
| ----- | ----- | ----- | ----- |
| 王雷 | 2351299 | [WingWR](https://github.com/WingWR) | 24034 |
| 林琪 | 2352609 | [lq1911](https://github.com/lq1911) | 4403 |
| 周达 | 2354185 | [zdlovepro](https://github.com/zdlovepro) | 3156 |
| 黄景胤 | 2351129 | [7719Drinkin](https://github.com/7719Drinkin) | 6248 |
| 李星烁 | 2351877 | [L-Dramatic](https://github.com/L-Dramatic) | 22032 |
| 金子涵 | 2351442 | [MMmmsvt](https://github.com/MMmmsvt) | 2822 |
| 达思睿 | 2352288 | [siri-iii](https://github.com/siri-iii) | 4361 |
| 肖相宇 | 2351110 | [Yushen-x](https://github.com/Yushen-x) | 12362 |
| 胡宝怡 | 2353409 | [Momokos101](https://github.com/Momokos101) | 7636 |
| 陈柏熙 | 2353120 | [cbx6666](https://github.com/cbx6666) | 21485 |

- **Git统计代码行数:**
```bash
$ git log --format='%aN' | sort -u | while read name; do
    echo -en "$name\t"
    git log --author="$name" --pretty=tformat: --numstat | awk '{ add += $1; subs += $2; loc += $1 - $2 } END { printf "added lines: %s, removed lines: %s, total lines: %s\n", add, subs, loc }' -
done
```

## 项目运行

### 启动前端

```bash
$ cd front-end 
$ npm install
$ npm run serve
```

### 启动后端
确认电脑上有安装.NET SDK 8.0.412，若未安装，请到网站https-//dotnet.microsoft.com/zh-cn/download/dotnet/8.0 下载
```bash
$ cd back-end
$ dotnet run
```
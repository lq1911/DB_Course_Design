# Git 分支管理规范（团队协作版）

## 分支命名规范

| 类型       | 命名格式                 | 用途说明                   |
|------------|--------------------------|--------------------------|
| 主分支     | `main`                   | 用于发布线上稳定版本        |
| 开发分支   | `develop`                | 所有功能合并前的主干开发分支 |
| 功能分支   | `feature/xxx`            | 开发新功能                 |
| 修复分支   | `fix/xxx`                | 修复 bug                   |

示例：
- `feature/login-ui`
- `fix/signup-crash`

---

## 分支协作流程

```
        main
          ▲
          │
          ▲
          │
       develop ←←←←←←←←←←←←←←←←←
          ▲
          │
     feature/ & fix/
```

- `main`：仅用于上线版本，稳定可靠；
- `develop`：用于日常集成开发；
- `feature/` 和 `fix/`：由开发人员创建并开发，完成后合并进 `develop`；

---

## 合并与提交规范

- **禁止直接推送到 `main` 和 `develop`**；
- 所有功能或修复分支完成后，需提交 Pull Request；
- 合并顺序建议如下：

```bash
feature/* → develop
fix/*     → develop
develop   → main
```

## 分支清理

- 功能或修复分支合并完成后，及时删除远程和本地分支：

```bash
$ git branch -d feature/xxx        # 删除本地
$ git push origin --delete feature/xxx  # 删除远程
```

---

## 操作规则

- 每次开发前：更新 `develop` 分支并拉新分支：
```bash
$ git checkout develop
$ git pull origin develop
$ git checkout -b feature/xxx
```

- 每次提交前：自测 & 保证不破坏其他模块；
- 提交信息清晰规范，例如：

```bash
20250718: 新增用户登录界面
20250718: 修复注册按钮点击无响应问题
20250718: 优化数据加载逻辑
```
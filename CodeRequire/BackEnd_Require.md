# 后端 API 接口文档(按功能命名)

> API接口文档放在`../Docs/BackEnd_API`下

- **接口名称**: xxx
- **接口描述**: xxxx
- **接口地址(URL)**: `.../Api/xxx`
- **请求方式**: `GET/POST/PUT...`

---

## 返回结果
```json
{
    "code": "xxx",
    "message": "xxx",
    "data": {
        "variable_1": "xxx",
        "variable_2": "xxx"
    }
}
```

## 响应说明

| 参数名 | 类型 | 说明 |
|--------|------|------|
| `code` | int | 状态码，200 为成功 |
| `message` | string | 操作结果说明 |
| `data.variable_1` | xxx | xxx |
| `data.variable_2` | xxx | xxx |

---

# 文档示例

见`Docs\BackEnd_API\Example.md`

[文档示例](../Docs/BackEnd_API/Example.md)
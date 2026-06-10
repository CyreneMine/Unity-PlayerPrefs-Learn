# 结合反射读取基础数据

## 本节目标

根据传入的目标类型创建一个新对象，再按照保存阶段使用的 key 规则，从 PlayerPrefs 中恢复其基础类型字段。

## 读取流程

当前读取逻辑分成两层：

1. `LoadData` 创建对象、遍历字段、重建 key，并把读取结果写回字段。
2. `LoadValue` 根据字段类型选择对应的 PlayerPrefs 读取 API。

这与保存阶段的 `SaveData` 和 `SaveValue` 形成对应关系。

## 动态创建对象

```csharp
object data = Activator.CreateInstance(type);
```

调用方只传入 `Type`，管理器便可以在运行时创建对应对象。目标类型需要能够通过无参方式创建，否则可能创建失败。

当前 `PlayerInfo` 和 `ItemInfo` 都提供了可用的无参构造函数，为后续自定义类型读取做好了准备。

## 重建相同的 key

读取阶段使用：

```text
外部Key_数据类型_字段类型_字段名称
```

它必须与保存阶段完全一致。读取失败很多时候不是 PlayerPrefs API 出错，而是保存和读取拼接出的 key 不相同。

## 基础类型读取

- `int` 使用 `PlayerPrefs.GetInt`
- `float` 使用 `PlayerPrefs.GetFloat`
- `string` 使用 `PlayerPrefs.GetString`
- `bool` 读取整数，并将 `1` 转换为 `true`，其他值转换为 `false`

读取结果通过：

```csharp
fieldInfo.SetValue(data, value);
```

写入动态创建的新对象。

## 当前阶段现象

List、Dictionary 和自定义类型读取尚未实现，`LoadValue` 对这些类型会返回 `null`。`SetValue` 随后会把测试对象中原本初始化好的集合和自定义对象字段覆盖为 `null`。

这是当前课程进度下的预期现象，不代表保存数据已经丢失。后续补全对应读取分支后，才能恢复这些字段。

## 缺失 key 与默认值

未使用 `PlayerPrefs.HasKey()` 时，读取不存在的 key 会得到默认值，例如：

- `int` 返回 `0`
- `float` 返回 `0`
- `string` 返回空字符串
- 当前 `bool` 规则会得到 `false`

因此无法区分：

- 该字段曾经保存过默认值
- 该字段从未保存过

是否需要使用 `HasKey`，取决于框架未来如何处理初始值、旧版本数据和缺失字段。

## 本次测试

测试脚本先保存一个 `PlayerInfo`，随后通过 `LoadData(typeof(PlayerInfo), "TestPlayer1")` 创建并读取新对象，再调用 `Show()` 输出字段。

代码检查确认基础字段读取链路完整。Console 的实际输出仍需要在 Unity 中运行确认。

## 本节理解

反射读取不是直接恢复整个对象，而是创建新对象，再逐个恢复字段。保存与读取的类型规则和 key 规则必须保持对称。

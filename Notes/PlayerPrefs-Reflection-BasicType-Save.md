# 结合反射保存基础数据

## 本节目标

本节开始补全 `PlayerPrefsDataMgr`，让管理器能够接收一个普通 C# 对象，通过反射遍历其公开字段，并保存 PlayerPrefs 支持的基础数据。

## 保存流程

当前保存逻辑可以分成两层：

1. `SaveData` 获取对象类型并遍历字段。
2. `SaveValue` 根据字段值的实际类型选择对应的 PlayerPrefs API。

这种分层让“对象中有哪些字段”和“某一种值应该怎么保存”各自负责一件事，后续扩展集合或自定义类型时更容易继续补充分支。

## 反射获取字段

```csharp
Type type = data.GetType();
FieldInfo[] infos = type.GetFields();
```

`GetFields()` 默认获取公开字段。遍历得到的每个 `FieldInfo` 包含：

- 字段名称：`fieldInfo.Name`
- 字段声明类型：`fieldInfo.FieldType`
- 指定对象中的字段值：`fieldInfo.GetValue(data)`

## key 命名规则

当前规则由以下内容组成：

```text
外部Key_数据类型_字段类型_字段名称
```

例如：

```text
TestPlayer1_PlayerInfo_String_name
TestPlayer1_PlayerInfo_Int32_age
TestPlayer1_PlayerInfo_Single_height
TestPlayer1_PlayerInfo_Boolean_sex
```

统一规则可以减少不同对象和字段之间的 key 冲突。读取数据时必须使用完全相同的规则还原 key。

## 基础类型保存

- `int` 使用 `PlayerPrefs.SetInt`
- `float` 使用 `PlayerPrefs.SetFloat`
- `string` 使用 `PlayerPrefs.SetString`
- `bool` 转换为 `0` 或 `1` 后使用 `PlayerPrefs.SetInt`

PlayerPrefs 不直接支持 `bool`，因此保存和读取时需要约定一致的转换规则。

## 检查中发现的边界

### 字段值为 `null`

当前已在调用 `value.GetType()` 前判断字段值是否为 `null`，避免产生空引用异常。

需要注意：跳过 `null` 字段只代表本次不保存它。如果这个字段对应的 key 以前保存过值，旧值仍然存在，不会因为当前值是 `null` 而自动删除。这个问题将在全部保存读取功能完成后，根据框架统一的空值、覆盖和删除规则进行处理。

如果传给 `SaveData` 的整个 `data` 对象为 `null`，当前会在进入反射逻辑前直接跳过，避免调用 `data.GetType()` 产生异常。字段为空和保存对象本身为空是两个不同的边界。

### 尚未支持的类型

当前遇到未支持类型时不会保存，也不会输出提示。这在框架尚未补全时可以理解，但调试阶段应意识到“没有报错”不等于“数据已经保存”。

### `PlayerPrefs.Save()` 的调用时机

调用 `SetInt`、`SetFloat` 或 `SetString` 后，数据已经更新到 PlayerPrefs。是否立即调用 `PlayerPrefs.Save()` 取决于需求：

- 重要节点需要立即落盘时，可以主动保存。
- 连续修改大量数据时，可以集中保存，减少频繁磁盘写入。

当前实现把 `PlayerPrefs.Save()` 放在 `SaveData` 的字段循环之后。这样会先写完对象的全部字段，再统一主动落盘一次，比每保存一个字段就调用一次更合理。

## 面试复盘：没有调用 `Save()`，为什么仍能看到数据？

这是值得记录的 Unity 基础问题，面试中更可能询问的是 `PlayerPrefs.Set...` 与 `PlayerPrefs.Save()` 的区别，而不是要求背诵 Windows 注册表路径。

可以这样回答：

- `PlayerPrefs.SetInt`、`SetFloat`、`SetString` 会修改 PlayerPrefs 中的数据。
- Unity 在应用正常退出时通常会自动保存 PlayerPrefs，因此即使没有手动调用 `Save()`，正常停止运行后也可能在注册表或平台对应存储位置看到新值。
- `PlayerPrefs.Save()` 的作用是主动要求当前修改立即持久化，降低游戏崩溃、强制关闭或断电时丢失尚未自动保存数据的风险。
- 不应在每帧或每个字段保存后频繁调用 `Save()`，因为持久化写入可能带来性能开销。

一句话总结：`Set...` 负责修改数据，`Save()` 负责明确要求现在就持久化；正常退出时 Unity 通常还会自动保存一次。

## 本次测试

测试类 `PlayerInfo` 包含 `string`、`int`、`float` 和 `bool` 字段，覆盖了当前 `SaveValue` 的全部基础类型分支。`Test` 脚本已挂载并启用在 `SampleScene` 中。

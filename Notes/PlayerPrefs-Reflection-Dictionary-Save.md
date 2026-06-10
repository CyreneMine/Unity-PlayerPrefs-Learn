# 结合反射保存基础类型 Dictionary

## 本节目标

在已有基础类型与 List 保存逻辑上增加 Dictionary 保存支持。目前字典的 key 和 value 只处理基础类型，自定义类型将在后续补全。

## 判断 Dictionary 类型

当前使用：

```csharp
typeof(IDictionary).IsAssignableFrom(fieldType)
```

`Dictionary<TKey, TValue>` 实现了 `IDictionary`，因此可以统一获取字典数量、遍历 key，并根据 key 取得对应 value。

## 保存结构

Dictionary 无法直接保存到 PlayerPrefs，因此当前拆成：

```text
字典Key             -> 字典项数量
字典Key_key_0       -> 第 0 项的 key
字典Key_value_0     -> 第 0 项的 value
字典Key_key_1       -> 第 1 项的 key
字典Key_value_1     -> 第 1 项的 value
```

key 和 value 使用相同下标形成一组，读取时可以按数量逐项恢复。

## Dictionary 遍历顺序

Dictionary 不应被当作依赖下标顺序的数据结构，遍历顺序也不应作为业务规则。

当前保存逻辑在同一次循环中保存 `obj` 和 `dic[obj]`，两者使用同一个下标，因此无论本次遍历顺序如何，每组 key/value 的配对关系都是正确的。

## 复用 `SaveValue`

字典的 key 和 value 都再次调用 `SaveValue`。当前它们为基础类型时会进入已有保存分支，后续增加自定义类型支持后也可以继续复用。

这种设计让 Dictionary 分支只负责：

- 保存数量
- 遍历字典
- 为每组 key/value 生成配对下标

## 当前测试

测试类增加了：

```csharp
public Dictionary<int, string> dic = new Dictionary<int, string>()
{
    { 1, "123" },
    { 2, "456" },
};
```

它验证了基础类型 key/value 的配对保存流程。`Test` 组件仍挂载在 `SampleScene` 中。

## 边界与后续处理

- 空字典会保存数量 `0`。
- 字典项减少后，旧下标对应的 key/value 可能残留，但读取时可以按新数量忽略。
- Dictionary 的 key 不能为 `null`；value 可以为 `null`，但当前会被 `SaveValue` 跳过。
- 如果 key 或 value 是尚未支持的类型，它也会被跳过，但字典数量仍包含该项，可能造成读取缺口。
- 旧 key 清理、`null` 和不支持类型的处理规则，将在全部保存读取功能完成后统一设计。

## 日志理解

当前 `Debug.Log(keyName)` 位于 `SaveValue` 的类型判断之前。因此看到日志只能说明该值进入了保存流程，不能单独证明它已经被某个 `PlayerPrefs.Set...` 分支成功保存。

## 本节理解

Dictionary 保存的关键是保存数量，并保持每一组 key/value 的配对关系。相同下标承担的是“配对标识”，不是 Dictionary 的业务顺序。

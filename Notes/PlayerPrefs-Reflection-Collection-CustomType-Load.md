# 结合反射读取集合与自定义类型

## 本节目标

在基础类型读取之上，补全 List、Dictionary 和自定义类型的递归读取，让嵌套数据能够按照保存时的结构重新恢复。

## List 读取

List 保存时记录了数量，并按下标保存每个元素。读取时执行相反流程：

1. 读取列表数量。
2. 使用 `Activator.CreateInstance(data)` 创建具体列表。
3. 使用 `GetGenericArguments()[0]` 取得元素类型。
4. 按下标递归调用 `LoadValue`。
5. 使用 `IList.Add` 加入恢复出的元素。

这套逻辑既能恢复基础类型 List，也能恢复 `List<ItemInfo>`。

## Dictionary 读取

Dictionary 保存时使用相同下标配对 key 和 value。读取时：

1. 读取字典数量。
2. 动态创建具体字典。
3. 使用两个泛型参数分别确定 key 和 value 类型。
4. 按同一个下标读取一组 key/value。
5. 使用 `IDictionary.Add` 恢复键值对。

Dictionary 不依赖原始遍历顺序，只需要保证读取时每组 key/value 使用相同下标。

## 自定义类型读取

当 `LoadValue` 遇到基础类型、List 和 Dictionary 以外的类型时，会递归调用：

```csharp
LoadData(data, keyName);
```

`LoadData` 创建自定义对象，再逐个读取并设置它的 public 字段。因此当前能够恢复：

- 单个 `ItemInfo`
- `List<ItemInfo>`
- `Dictionary<int, ItemInfo>`

## 动态创建的条件

当前实现使用 `Activator.CreateInstance` 创建对象与集合，因此目标类型必须能够被实际实例化。

- 自定义类需要可用的无参构造函数。
- 具体的 `List<T>` 和 `Dictionary<TKey, TValue>` 可以创建。
- 如果字段声明成接口、抽象类或其他不可实例化类型，创建会失败。

## 当前测试能证明什么

当前 `Start()` 流程是：

```text
创建并设置 PlayerInfo
保存 PlayerInfo
立即读取新的 PlayerInfo
Show 输出结果
```

它能够证明保存与读取的 key 规则、类型分支和递归逻辑互相对应。

但它还不能完整模拟实际游戏中的持久化流程，因为保存和读取发生在同一次运行中，并且部分测试数据在字段声明时已经预设。

## 实际游戏流程验证

当前已经完成以下分阶段测试：

1. 数据最初不在字段声明时预设具体内容。
2. 第一次运行在 `Start()` 中模拟玩家游玩后赋值并保存。
3. 停止运行后注释保存代码。
4. 第二次运行只执行读取，并通过 `Show()` 确认恢复结果。

这样可以证明读取到的内容确实来自持久化数据，而不是当前内存对象或同一帧刚写入的结果。

本次跨运行测试覆盖：

- 基础字段
- `List<int>`
- `Dictionary<int, string>`
- 单个自定义对象 `ItemInfo`

非空 `List<ItemInfo>` 与 `Dictionary<int, ItemInfo>` 的递归逻辑已经在此前测试中覆盖，但当前跨运行样例中它们保持为空。后续可以将其作为加强验证，不影响本节完成。

## 本节理解

List 和 Dictionary 读取是在恢复容器结构，自定义类型读取是在恢复对象字段。递归让它们能够互相嵌套，但每一层都依赖类型可实例化，以及保存和读取规则保持对称。

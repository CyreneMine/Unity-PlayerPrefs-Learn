# 结合反射保存自定义数据类型

## 本节目标

当字段不是基础类型、List 或 Dictionary 时，将它视为自定义对象，再次进入 `SaveData` 拆分其字段。

## 递归保存思路

当前 `SaveValue` 的最终分支会调用：

```csharp
SaveData(value, keyName);
```

这样可以处理：

- 单个自定义对象
- `List<自定义类型>`
- `Dictionary<基础类型, 自定义类型>`
- 自定义对象内部继续嵌套其他对象

每一层负责拆分自己的结构，直到字段最终变成 PlayerPrefs 可以保存的基础类型。

## 当前测试结构

`PlayerInfo` 中增加了：

- 单个 `ItemInfo`
- `List<ItemInfo>`
- `Dictionary<int, ItemInfo>`

这三个测试能够验证自定义对象在普通字段、List 元素和 Dictionary value 中是否都能进入递归保存流程。

## `GetFields()` 与字段权限

当前保存代码使用：

```csharp
FieldInfo[] infos = type.GetFields();
```

无参数 `GetFields()` 默认获取 public 字段。最初测试类 `ItemInfo` 的字段是 private，因此不会被取得。

```csharp
public int num;
public string str;
```

当前已将这两个字段改为 public，递归进入 `ItemInfo` 后能够取得并保存它们。

当前框架采用的规则是：

- 使用无参数 `GetFields()` 保存 public 字段。
- private 字段不会被当前框架保存。
- 如果未来需要保存 private 字段，再使用合适的反射范围参数，并重新考虑哪些字段应该参与持久化。

这次问题不是递归思路错误，而是“反射能够看到哪些字段”的范围与测试类字段权限最初不一致。

## 日志验证误区

当前日志在类型判断之前输出。看到自定义对象的 key，只能说明它进入了 `SaveValue`。

验证是否真正保存成功时，应继续确认：

- 是否生成了对象内部字段对应的完整 key
- 注册表或平台存储中是否存在内部字段值
- 后续读取是否能恢复这些字段

## 递归风险

当前所有未支持类型都会进入 `SaveData`。如果两个对象互相引用，递归保存可能永远无法结束，最终导致栈溢出。

这是通用反射序列化需要考虑的边界。当前课程阶段先建立意识，等框架功能完成后再决定是否限制可保存类型或检测循环引用。

## 当前结论

自定义类型的递归保存框架已经完成。当前规则只保存 public 字段，`ItemInfo` 的两个字段改为 public 后，单个对象、`List<ItemInfo>` 和 `Dictionary<int, ItemInfo>` 都可以递归拆分并保存。

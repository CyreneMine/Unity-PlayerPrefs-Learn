# 反射必备知识点

## 本节目标

本节为后续结合反射封装 PlayerPrefs 做准备，重点不是一次掌握完整反射系统，而是理解如何在运行时获取、判断和使用类型。

## 获取 `Type`

已知类型时，可以使用 `typeof`：

```csharp
Type fatherType = typeof(Father);
```

已经拥有对象时，可以使用 `GetType()`：

```csharp
List<float> list = new List<float>();
Type listType = list.GetType();
```

`typeof` 面向代码中明确写出的类型，`GetType()` 获取对象运行时的实际类型。

## 判断类型能否赋值

```csharp
fatherType.IsAssignableFrom(sonType)
```

这句话可以理解为：`Father` 类型的变量能否接收一个 `Son` 类型的实例。

- 调用 `IsAssignableFrom` 的类型是接收方
- 传入的类型是准备赋值的来源方
- 因为 `Son` 继承自 `Father`，所以本次判断结果为 `true`

这个参数方向容易混淆。可以把它对应到普通赋值来帮助判断：

```csharp
Father father = new Son();
```

## 根据类型动态创建实例

```csharp
Father father = Activator.CreateInstance(sonType) as Father;
```

`Activator.CreateInstance` 可以根据运行时得到的 `Type` 创建对象。本次能够成功，是因为：

- `Son` 可以赋值给 `Father`
- `Son` 存在可用的无参构造函数

后续使用时要注意，目标类型无法被创建、缺少对应构造函数，或者转换类型不兼容，都可能导致创建失败或得到 `null`。

## 获取泛型参数

```csharp
Type[] types = listType.GetGenericArguments();
```

- `List<float>` 有一个泛型参数，`types[0]` 是 `System.Single`
- `Dictionary<string, float>` 有两个泛型参数，`types[0]` 是 `System.String`，`types[1]` 是 `System.Single`

泛型参数的顺序与类型声明中的顺序一致。后续处理 `List` 和 `Dictionary` 时，可以通过这些类型信息判断集合内部保存的具体数据类型。

## 与 PlayerPrefs 封装的关系

PlayerPrefs 只能直接处理 `int`、`float` 和 `string`。当数据管理类接收到不同类型的数据时，可以通过反射：

- 判断传入数据是什么类型
- 判断它是否为某种集合或自定义类型
- 获取集合内部的泛型参数
- 根据类型选择对应的拆分、保存和读取逻辑

反射让封装代码可以处理运行时类型，但也会让错误更晚暴露，因此仍需要明确类型规则，并做好运行验证。

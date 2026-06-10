# PlayerPrefs 数据管理类框架

## 本节目标

随着需要保存的数据类型增多，如果每个业务类都直接调用 PlayerPrefs，会出现大量重复的类型判断、key 拼接和保存读取代码。

本节先搭建 `PlayerPrefsDataMgr` 的基础框架，把后续保存和读取逻辑集中到一个管理类中。

## 当前框架结构

### 普通 C# 单例

管理器使用静态字段保存唯一实例，并通过静态属性提供访问入口：

```text
PlayerPrefsDataMgr.Instance
```

构造函数设为私有后，外部无法随意 `new PlayerPrefsDataMgr()`，因此所有调用方会使用同一个管理器实例。

当前管理器不是 `MonoBehaviour`，不需要挂载到 GameObject，也不依赖 Unity 生命周期。

### 统一保存与读取入口

当前框架预留了保存和读取方法。后续会逐步在入口内部判断数据类型，并分别处理：

- PlayerPrefs 支持的常用类型
- `List`
- `Dictionary`
- 自定义数据类型

这样外部代码只需要关心“保存什么数据”和“使用什么 key”，不需要重复编写底层拆分规则。

## `object` 类型需要注意

当前代码使用了：

```csharp
using Object = UnityEngine.Object;
```

因此方法参数和返回值中的 `Object` 实际表示 `UnityEngine.Object`。它主要用于 `GameObject`、`Component`、`ScriptableObject` 等 Unity 对象，普通 C# 数据类、`List` 和 `Dictionary` 并不继承它。

通用数据管理器通常需要接收的是 `System.Object`，在 C# 中可以直接写成小写 `object`。它能够引用普通数据类和集合，后续才能通过反射检查这些数据的实际类型。

需要建立的区分：

- `UnityEngine.Object`：Unity 引擎对象体系的基类
- `System.Object`：所有 C# 类型的共同基类
- `object`：`System.Object` 的 C# 关键字写法

## 命名复盘

`Data` 表示数据，`Date` 表示日期。保存和读取数据的方法名应注意二者区别，否则以后阅读调用代码时容易误解方法职责。

当前已将方法名修正为 `SaveData` 和 `LoadData`。

## 当前修正结果

- `SaveData` 的参数已使用小写 `object`，可以接收普通 C# 数据和集合。
- `LoadData` 的返回类型也已改为小写 `object`，保存和读取入口的类型设计保持一致。
- 文件顶部未使用的 `UnityEngine.Object` 别名已经清理，避免后续误用大写 `Object`。

## 后续补全方向

接下来的课程会逐步补全统一入口内部的处理逻辑。每加入一种类型，都应验证：

- 保存时使用了哪些 key
- 读取时能否恢复同样的数据
- 不同对象的 key 是否会冲突
- 集合元素和自定义类型如何递归处理

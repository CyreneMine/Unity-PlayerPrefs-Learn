# PlayerPrefs 学习进度

本文件用于记录唐老狮 PlayerPrefs 课程的学习路线和完成情况。

状态说明：

- 未开始：还没有正式学习
- 学习中：正在看课、写练习或调试
- 已完成：已完成本节学习和基础复盘
- 待复盘：学完但还需要整理理解、检查代码或补充笔记

## 课程目录

| 序号 | 课程内容 | 阶段 | 状态 | 学习日期 | 备注 |
| --- | --- | --- | --- | --- | --- |
| 01 | PlayerPrefs概述 | PlayerPrefs 基础 | 已完成 | 2026-06-02 | 已开始理解 PlayerPrefs 的用途和学习工程记录方式 |
| 02 | PlayerPrefs基本方法 | PlayerPrefs 基础 | 已完成 | 2026-06-02 | 已练习基础类型保存、读取和 Save |
| 03 | PlayerPrefs基本方法练习题 | PlayerPrefs 基础 | 已完成 | 2026-06-02 | 完成玩家数据和装备 List 的保存读取练习，修正引用类型读取问题 |
| 04 | PlayerPrefs存储位置 | PlayerPrefs 基础 | 已完成 | 2026-06-03 | 理解多份数据需要用不同 key 前缀区分 |
| 05 | PlayerPrefs存储位置练习题 | PlayerPrefs 基础 | 已完成 | 2026-06-03 | 完成多玩家信息存储和排行榜存取练习 |
| 06 | PlayerPrefs总结 | PlayerPrefs 基础 | 已完成 | 2026-06-03 | 回顾 PlayerPrefs 基础用法，并了解后续课程会进行封装 |
| 07 | 必备知识点及知识小补充 | 必要补充知识 | 已完成 | 2026-06-10 | 练习 Type、IsAssignableFrom、Activator 和泛型参数反射 |
| 08 | 需求分析 | 必要补充知识 | 已完成 | 2026-06-10 | 明确统一保存读取入口和后续封装方向 |
| 09 | 数据管理类创建 | 数据管理类与常用类型存取 | 已完成 | 2026-06-10 | 搭建 PlayerPrefsDataMgr 单例和保存读取入口 |
| 10 | 结合反射常用数据类型存储 | 数据管理类与常用类型存取 | 已完成 | 2026-06-10 | 通过反射保存 int、float、string 和 bool 字段 |
| 11 | 结合反射List数据类型存储 | 数据管理类与常用类型存取 | 已完成 | 2026-06-10 | 使用 IList 保存列表数量和基础类型元素 |
| 12 | 结合反射Dictionary数据类型存储 | 数据管理类与常用类型存取 | 未开始 |  |  |
| 13 | 结合反射自定义数据存储 | 数据管理类与常用类型存取 | 未开始 |  |  |
| 14 | 结合反射读取常用数据类型 | 数据管理类与常用类型存取 | 未开始 |  |  |
| 15 | 结合反射读取List数据类型 | 数据管理类与常用类型存取 | 未开始 |  |  |
| 16 | 结合反射读取Dictionary数据类型 | 数据管理类与常用类型存取 | 未开始 |  |  |
| 17 | 结合反射读取自定义数据类型 | 数据管理类与常用类型存取 | 未开始 |  |  |
| 18 | 加密思路 | 收尾 | 未开始 |  |  |
| 19 | 打包总结 | 收尾 | 未开始 |  |  |

## 阶段复盘模板

每完成一个阶段，可以在下方追加复盘记录。

```md
### YYYY-MM-DD 阶段名称

- 学习主题：
- 涉及 API：
- 完成的练习：
- 遇到的问题：
- 解决方案：
- 我的理解总结：
```

## 阶段复盘记录

### 2026-06-02 PlayerPrefs 基本方法练习题

- 学习主题：使用 PlayerPrefs 保存和读取玩家基础信息与装备列表。
- 涉及 API：`PlayerPrefs.SetString`、`PlayerPrefs.SetInt`、`PlayerPrefs.GetString`、`PlayerPrefs.GetInt`、`PlayerPrefs.Save`。
- 完成的练习：创建 `Player` 玩家类和 `Item` 装备类，为玩家封装 `Save()`、`Load()`、`Show()` 方法，并用 `ItemCount + 下标 key` 的方式保存装备列表。
- 遇到的问题：读取装备列表时，最开始把 `Item item = new Item()` 写在循环外，导致 List 中多个位置可能引用同一个装备对象。
- 解决方案：把 `new Item()` 放进读取循环内部，每读取一条装备数据就创建一个新的 `Item` 对象，再添加到 `items` 列表。
- 我的理解总结：PlayerPrefs 保存的是 key-value 数据，数据本身没有因为引用问题丢失；真正出错的是读取回内存时，如果重复使用同一个 class 对象，就会让 List 中多个元素指向同一个地址，最后显示成最后一次读取的数据。
- 待确认事项：脚本代码已完成，但需要在 Unity 场景中确认 `Lesson1_PlayerPrefs` 是否挂载到某个 GameObject 上，否则 `Start()` 不会执行。

### 2026-06-03 PlayerPrefs 多玩家和排行榜练习题

- 学习主题：用 PlayerPrefs 支持多名玩家数据，以及保存读取排行榜信息。
- 涉及 API：`PlayerPrefs.SetString`、`PlayerPrefs.SetInt`、`PlayerPrefs.GetString`、`PlayerPrefs.GetInt`、`PlayerPrefs.Save`。
- 完成的练习：为玩家数据增加 key 前缀，让同一套字段可以按 `Player1`、`Player2` 等不同玩家分开保存；创建排行榜数据类，保存玩家名、分数和通关时间；在重复读取玩家装备前清空旧列表，避免重复追加；排行榜排序支持分数降序和同分时间升序。
- 遇到的问题：教程答案没有处理重复玩家名，可能导致同一个玩家多次出现在排行榜中。
- 解决方案：在新增排行榜记录前先查找同名玩家，如果不存在则新增；如果存在则按排行榜规则更新成绩，避免同一个玩家重复出现在榜单里。
- 我的理解总结：排行榜通常应该表达“玩家当前成绩记录”，同名玩家重复出现会降低可读性；是否覆盖、只保留最高分、还是保留历史记录，取决于需求，但需要明确规则。
- 后续注意：本次进一步理解了 `CompareTo` 在排序委托中的返回值含义；更新成绩时仍要注意分数和通关时间是否来自同一次通关记录。

### 2026-06-03 PlayerPrefs 基础阶段总结

- 学习主题：总结 PlayerPrefs 基础 API、存储方式和练习题思路。
- 涉及 API：`PlayerPrefs.SetInt`、`PlayerPrefs.SetFloat`、`PlayerPrefs.SetString`、`PlayerPrefs.GetInt`、`PlayerPrefs.GetFloat`、`PlayerPrefs.GetString`、`PlayerPrefs.Save`、`PlayerPrefs.DeleteKey`、`PlayerPrefs.DeleteAll`。
- 完成的练习：保存读取玩家基础信息、装备列表、多名玩家信息和排行榜数据。
- 遇到的问题：基础 API 只能直接保存简单类型，复杂数据需要拆成多个 key；多份同结构数据需要 key 前缀；列表需要保存数量和下标字段；读取 class 对象时要注意引用类型。
- 解决方案：使用统一 key 命名规则、数量字段、循环保存和读取、读取前清空旧列表、每次循环创建新对象。
- 我的理解总结：PlayerPrefs 适合少量本地简单数据保存。当前写法可以完成练习，但 key 拼接、重复代码和复杂类型拆分会越来越多，所以后续课程会对 PlayerPrefs 做封装，提高复用性并降低出错概率。

### 2026-06-10 反射必备知识点

- 学习主题：为后续使用反射封装 PlayerPrefs 保存和读取逻辑补充基础知识。
- 涉及 API：`typeof`、`object.GetType()`、`Type.IsAssignableFrom()`、`Activator.CreateInstance()`、`Type.GetGenericArguments()`。
- 完成的练习：获取父类和子类的 `Type`；判断子类类型能否赋值给父类；根据运行时类型动态创建对象；读取 `List<float>` 和 `Dictionary<string, float>` 的泛型参数类型。
- 容易混淆的地方：`fatherType.IsAssignableFrom(sonType)` 表示“一个 `Son` 类型的实例能否赋值给 `Father` 类型变量”，调用方是接收类型，参数是准备赋值的来源类型，反过来判断结果会不同。
- 我的理解总结：反射可以在代码运行时获取和判断类型，并根据 `Type` 创建实例。后续封装 PlayerPrefs 时，可以利用这些能力识别传入数据的具体类型，再选择对应的保存和读取方式。
- 运行验证：已将 `Reflection` 脚本挂载并启用在 `SampleScene` 的 GameObject 上，同时禁用旧的 `Lesson2` 组件以避免日志干扰；已在 Unity 中完成运行测试。

### 2026-06-10 PlayerPrefs 数据管理类框架搭建

- 学习主题：分析 PlayerPrefs 封装需求，并创建统一数据管理类的基础框架。
- 完成的练习：创建 `PlayerPrefsDataMgr`；使用静态实例和私有构造函数实现普通 C# 单例；预留统一保存和读取方法，为后续通过反射处理不同类型做准备。
- 当前框架思路：外部通过 `PlayerPrefsDataMgr.Instance` 获取唯一管理器，再使用统一入口传入数据、类型和 key，具体保存读取规则由管理器内部负责。
- 修正记录：已将保存方法参数和读取方法返回值统一改为小写 `object`，普通 C# 数据类、`List` 和 `Dictionary` 可以通过统一入口保存与返回；同时已清理未使用的 `UnityEngine.Object` 别名。
- 命名修正：已将 `SaveDate` 和 `LoadDate` 修正为 `SaveData` 和 `LoadData`；`Date` 表示日期，`Data` 表示数据。
- 我的理解总结：这一阶段先确定管理器对外暴露什么入口，不急着实现所有类型。好的统一入口能够隐藏 key 拼接和类型判断细节，但参数类型必须足够通用，否则后续封装会在入口处限制住可保存的数据。

### 2026-06-10 结合反射保存常用数据类型

- 学习主题：通过反射读取对象字段，并将常用基础数据保存到 PlayerPrefs。
- 涉及 API：`object.GetType()`、`Type.GetFields()`、`FieldInfo.GetValue()`、`FieldInfo.FieldType`、`PlayerPrefs.SetInt`、`PlayerPrefs.SetFloat`、`PlayerPrefs.SetString`。
- 完成的练习：遍历对象的公开字段；使用“外部 key + 数据类型 + 字段类型 + 字段名”组成保存 key；根据运行时类型分别保存 `int`、`float`、`string` 和 `bool`；将 `bool` 转换为 `0` 或 `1` 保存。
- 运行验证：创建包含四种基础字段的 `PlayerInfo` 测试数据；`Test` 脚本已挂载并启用在 `SampleScene` 中，旧的 `Lesson2` 和 `Reflection` 组件已禁用，避免测试日志互相干扰。
- 修正记录：已在 `SaveValue` 中判断字段值是否为 `null`，避免直接调用 `value.GetType()` 产生空引用异常；并在整份对象字段保存完成后调用一次 `PlayerPrefs.Save()`，保证本次修改立即落盘。
- 修正与待办：已在 `SaveData` 外层判断整个对象是否为 `null`，避免调用 `data.GetType()` 产生异常。字段为 `null` 时旧 key 不会自动删除的问题，将在全部保存读取功能完成后统一设计处理；遇到当前未支持的字段类型时仍会静默跳过。
- 保存时机理解：`SetInt`、`SetFloat` 和 `SetString` 用于修改 PlayerPrefs 数据，Unity 正常退出时通常会自动保存；`PlayerPrefs.Save()` 用于主动要求立即写入持久化存储。重要节点可以主动保存，频繁变化的数据可以集中保存，避免不必要的磁盘写入。
- 我的理解总结：反射负责发现对象有哪些字段并取得字段值，`SaveValue` 负责根据具体类型选择 PlayerPrefs API。这样新增类型处理时可以集中扩展，而业务对象不需要自己拼接每一个 key。

### 2026-06-10 结合反射保存基础类型 List

- 学习主题：在统一保存入口中识别 List，并将列表拆成数量与下标元素保存。
- 涉及 API：`Type.IsAssignableFrom()`、`IList`、`IList.Count`、`foreach`。
- 完成的练习：使用 `typeof(IList).IsAssignableFrom(fieldType)` 判断列表类型；保存列表数量；遍历列表并复用 `SaveValue` 保存每个元素；使用 `List<int>` 完成基础运行测试。
- 当前支持范围：列表元素为 `int`、`float`、`string` 或 `bool` 时，可以复用已有基础类型保存分支；自定义类型元素将在后续课程中补全。
- 设计优点：List 分支只负责列表结构，元素具体如何保存继续交给 `SaveValue`，避免为每一种 `List<T>` 重复编写保存代码。
- 边界与待办：空列表可以保存数量 `0`；列表缩短后旧下标 key 会残留，但后续读取可以按新数量忽略；列表中的 `null` 元素会计入数量但不会保存对应值。旧 key 清理和 `null` 元素规则将在全部功能完成后统一处理。
- 测试范围：当前实际测试覆盖 `List<int>`；其他基础类型列表使用相同保存分支，但后续可以分别补充样例验证 key 和值。
- 我的理解总结：PlayerPrefs 不能直接保存 List，因此需要保存数量并把每个元素拆开。递归调用 `SaveValue` 后，列表结构和元素类型处理可以分开，后续扩展自定义类型时也能继续复用。

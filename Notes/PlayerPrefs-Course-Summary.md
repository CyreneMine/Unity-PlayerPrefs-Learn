# PlayerPrefs 课程最终总结

## 完成内容

本项目已完成 19 P PlayerPrefs 课程学习，并保留了从基础 API 到反射封装的完整练习过程。

学习路线包括：

- PlayerPrefs 基础保存、读取、删除和持久化
- 多玩家数据与排行榜保存
- List、Dictionary 和自定义类型的拆分思路
- 反射获取类型、字段和泛型参数
- 统一 PlayerPrefs 数据管理器
- 递归保存与读取基础类型、集合和自定义类型
- 运行时赋值与跨运行读取验证
- 本地数据加密与安全边界
- 数据管理器目录整理与 Unity Package 导出

## 最终数据管理器思路

`PlayerPrefsDataMgr` 对外提供统一保存和读取入口。

保存时：

1. 通过反射遍历对象的 public 字段。
2. 使用统一规则生成 key。
3. 基础类型直接调用 PlayerPrefs API。
4. List 保存数量并递归保存元素。
5. Dictionary 保存数量并按相同下标保存 key/value。
6. 自定义类型递归拆分其字段。

读取时执行对称流程：动态创建对象或集合，使用相同 key 规则读取数据，再通过反射恢复字段和容器元素。

## 重要理解

- 保存与读取的 key 规则必须完全对称。
- class 是引用类型，循环恢复对象时需要创建独立实例。
- List 和 Dictionary 需要额外保存数量。
- Dictionary 的下标用于恢复键值配对，不代表业务顺序。
- 反射创建的自定义类型需要可用的无参构造函数。
- 当前无参数 `GetFields()` 只保存 public 字段。
- `PlayerPrefs.Save()` 用于主动要求当前修改持久化，正常退出时 Unity 通常也会自动保存。
- PlayerPrefs 属于客户端本地存储，不适合作为重要货币、付费状态和排行榜结果的唯一权威来源。

## 已知边界与后续改进

- 字段变为 `null` 时，旧 key 不会自动删除。
- List 或 Dictionary 缩短后，旧下标 key 可能残留。
- 集合中的 `null` 元素会造成保存数量与实际值之间的缺口。
- 缺少 `HasKey` 判断时，无法区分缺失数据与保存过的默认值。
- 循环引用可能导致递归无法结束。
- 反射带来便利，也需要考虑性能和错误发现时机。

这些问题没有阻碍当前课程目标，但在真实项目中需要根据数据价值和业务规则继续完善。

## 打包结果

最终脚本整理在：

```text
Assets/Scripts/PlayerPrefsDataMgr/
```

项目根目录已导出：

```text
PlayerPrefsDataMgr.unitypackage
```

导出包只包含 `PlayerPrefsDataMgr.cs` 及其 `.meta`，适合导入其他 Unity 项目复用。该文件被项目的 `*.unitypackage` Git 忽略规则排除，因此只保留在本地。

## 最终复盘

这套管理器的价值不只是减少 PlayerPrefs API 调用，而是把复杂数据拆分、key 管理和类型分派集中起来。课程真正训练的是：先理解底层限制，再通过明确规则封装重复逻辑，同时知道封装仍然存在适用边界。

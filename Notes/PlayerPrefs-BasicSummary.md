# PlayerPrefs 基础阶段总结

## 本阶段学到的内容

PlayerPrefs 是 Unity 提供的本地简单数据存储方式，适合保存少量配置、进度、分数、玩家偏好等数据。

基础 API 可以按类型分为：

- 保存：`SetInt`、`SetFloat`、`SetString`
- 读取：`GetInt`、`GetFloat`、`GetString`
- 写入磁盘：`Save`
- 删除：`DeleteKey`、`DeleteAll`

## 当前练习中的保存思路

### 基础字段

玩家名字、年龄、攻击力、防御力这类字段，可以拆成多个 key 保存：

```text
Player1_name
Player1_age
Player1_atk
Player1_def
```

如果要保存多名玩家，需要给 key 增加不同前缀，避免后保存的数据覆盖前面的数据。

### 列表数据

PlayerPrefs 不能直接保存 `List<Item>`，所以需要拆开保存：

```text
Player1_ItemCount
Player1_itemId0
Player1_itemNum0
Player1_itemId1
Player1_itemNum1
```

读取时先读数量，再循环恢复每一个对象。

## 这一阶段踩过的坑

- class 是引用类型，循环读取对象时要每次 `new` 新对象，否则 List 中多个元素可能指向同一个对象。
- 同一个对象重复读取列表数据前要先清空旧列表，否则会重复追加。
- 多份同结构数据不能共用同一套 key，否则会互相覆盖。
- 排行榜要先明确规则：是否允许同名玩家重复出现，是否保留最新成绩，还是保留最好成绩。
- 排序逻辑要和排行榜规则一致，例如分数高优先，同分时通关时间短优先。

## 后续课程方向

基础写法可以完成练习，但随着字段和数据类型变多，会出现很多重复问题：

- key 拼接分散，容易写错
- 保存和读取逻辑重复
- List、Dictionary、自定义类型都需要手动拆分
- 数据规则散落在不同脚本里，不方便复用

后续课程会开始对 PlayerPrefs 进行封装。封装的目标不是为了“高级”，而是把重复的保存、读取、key 管理和复杂数据拆分逻辑集中起来，让以后使用时更清晰、更不容易出错。

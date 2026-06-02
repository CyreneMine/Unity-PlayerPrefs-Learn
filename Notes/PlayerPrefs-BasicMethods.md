# PlayerPrefs 基本方法笔记

## 本节目标

本节练习的重点是理解 PlayerPrefs 的基础存取方式，并尝试把多个字段拆成多个 key 保存。

练习对象包括：

- 玩家信息：名字、年龄、攻击力、防御力
- 装备信息：id、数量
- 玩家拥有的装备列表：`List<Item>`

## PlayerPrefs 基础理解

PlayerPrefs 本质上适合保存简单的本地数据。它不是直接保存一个完整对象，而是按 key-value 的方式保存单个值。

例如玩家数据可以拆成：

```text
name
age
atk
def
```

装备列表可以拆成：

```text
ItemCount
itemId0
itemNum0
itemId1
itemNum1
```

读取时再根据 `ItemCount` 循环，把每一组 `itemId + itemNum` 重新组装成一个 `Item` 对象。

## 本次遇到的问题

最开始读取装备列表时，把 `Item item = new Item()` 写在了循环外。

这样会出现一个容易忽略的问题：`Item` 是 class，属于引用类型。List 里保存的是对象引用，不是对象数据的独立复制。

如果循环里一直复用同一个 `Item` 对象：

```csharp
Item item = new Item();

for (...)
{
    item.id = PlayerPrefs.GetInt("itemId" + i);
    item.num = PlayerPrefs.GetInt("itemNum" + i);
    items.Add(item);
}
```

那么 `items` 里多个位置会指向同一个对象。循环最后一次读取的数据会覆盖这个对象之前的数据，导致重新进游戏读取时，多个装备可能都显示成最后一个装备。

正确思路是每次循环都创建新的对象：

```csharp
for (...)
{
    Item item = new Item();
    item.id = PlayerPrefs.GetInt("itemId" + i);
    item.num = PlayerPrefs.GetInt("itemNum" + i);
    items.Add(item);
}
```

## 关键理解

- PlayerPrefs 中保存的 key-value 数据没有因为这个问题丢失。
- 问题发生在读取数据后重新组装对象的阶段。
- class 对象是引用类型，List 存进去的是引用地址。
- 如果没有在循环中 `new` 新对象，就会反复修改同一个对象。
- 保存列表时常见思路是先保存数量，再用下标保存每个元素的字段。

## 后续注意

- 测试读取逻辑时，要区分“刚手动添加后的打印”和“停止运行后重新进入游戏读取出来的打印”。
- 如果脚本没有挂载到场景中的 GameObject 上，`Start()` 不会执行。
- 当前测试逻辑是先 `Load()` 旧装备，再手动 `Add()` 两件装备，最后再次 `Save()`。如果重复运行，装备数量会继续累计；正式逻辑中要区分“首次创建数据”和“读取已有数据后展示”。
- 后续数据变多后，key 最好加统一前缀，例如 `Player_Name`、`Player_Item_0_Id`，避免不同数据之间 key 冲突。

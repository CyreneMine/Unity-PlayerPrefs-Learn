# Unity PlayerPrefs Learn

这是一个 Unity 学习项目，用于跟随唐老狮教程学习 `PlayerPrefs`，并在后续教程中继续保留独立练习内容。

项目重点不是快速做出完整成品，而是记录学习过程、理解 Unity API 的使用方式、保存练习代码，方便之后复盘。

## 学习目标

- 理解 `PlayerPrefs` 的基本用途、适用场景和局限性
- 掌握 `PlayerPrefs` 常用保存、读取、删除和持久化方法
- 理解 `PlayerPrefs` 在不同平台上的数据存储位置
- 通过练习题巩固 API 使用、需求分析和调试思路
- 创建可复用的数据管理类，减少散乱的键名和重复逻辑
- 结合反射保存和读取常用数据类型、`List`、`Dictionary` 和自定义数据类型
- 理解简单加密思路，知道如何降低明文存储带来的风险
- 完成打包前后的验证，总结 PlayerPrefs 在实际项目中的使用边界

## 项目信息

- Unity 版本：6000.3.10f1
- 项目类型：Unity 学习工程
- 当前主题：PlayerPrefs
- 远程仓库：<https://github.com/CyreneMine/Unity-PlayerPrefs-Learn>

## 目录说明

```text
Assets/              Unity 资源、场景和练习脚本
Packages/            Unity 包管理配置
ProjectSettings/     Unity 项目设置
Notes/               知识点笔记和问题复盘
AGENTS.md            Codex 协作规则
README.md            项目说明和学习记录入口
LearningProgress.md  课程目录和学习进度
```

以下目录由 Unity 自动生成，不提交到 Git：

```text
Library/
Temp/
Logs/
UserSettings/
```

## 学习记录计划

后续可以逐步维护这些内容：

- [LearningProgress.md](LearningProgress.md)：记录每个阶段的学习进度
- [Notes/](Notes/)：整理 PlayerPrefs 和其他 Unity API 的知识点
- [Screenshots/](Screenshots/)：保存练习运行效果截图

每次阶段总结建议记录：

- 学习日期
- 学习主题
- 涉及 API
- 完成的练习
- 遇到的问题
- 解决方案
- 我的理解总结

## 当前状态

已完成 PlayerPrefs 基础阶段和后续封装所需的反射必备知识点学习。当前开始进入 PlayerPrefs 数据管理类的需求分析与封装阶段。

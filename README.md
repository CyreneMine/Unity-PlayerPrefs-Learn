# Unity PlayerPrefs Learn

这是一个 Unity 学习项目，用于跟随唐老狮教程学习 `PlayerPrefs`，并在后续教程中继续保留独立练习内容。

项目重点不是快速做出完整成品，而是记录学习过程、理解 Unity API 的使用方式、保存练习代码，方便之后复盘。

## 学习目标

- 理解 `PlayerPrefs` 的基本用途和适用场景
- 掌握 `PlayerPrefs.SetInt`、`SetFloat`、`SetString` 等保存方法
- 掌握 `PlayerPrefs.GetInt`、`GetFloat`、`GetString` 等读取方法
- 理解默认值、键名管理、数据覆盖和删除的基本逻辑
- 通过小练习熟悉 Unity 中本地简单数据存储的使用方式

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
AGENTS.md            Codex 协作规则
README.md            项目说明和学习记录入口
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

- `LearningProgress.md`：记录每个阶段的学习进度
- `Notes/`：整理 PlayerPrefs 和其他 Unity API 的知识点
- `Screenshots/`：保存练习运行效果截图

每次阶段总结建议记录：

- 学习日期
- 学习主题
- 涉及 API
- 完成的练习
- 遇到的问题
- 解决方案
- 我的理解总结

## Git 首次绑定说明

本项目首次推送到 GitHub 时，需要完成：

```powershell
git init
git branch -M main
git remote add origin https://github.com/CyreneMine/Unity-PlayerPrefs-Learn.git
git add .
git commit -m "Initial Unity PlayerPrefs learning project"
git push -u origin main
```

如果推送时提示认证失败，需要提供可访问该仓库的 GitHub 登录凭据或 Personal Access Token。

## 当前状态

项目已创建基础 Unity 工程结构，后续会随着 PlayerPrefs 教程练习逐步补充代码、笔记和阶段复盘。

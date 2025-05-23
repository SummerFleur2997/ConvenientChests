﻿## 历史版本

[English](VersionHistory.md) | 简体中文

### 1.8.0

**新功能**
- 新增锁定物品功能，可锁定物品栏内的物品，防止其被堆叠至附近的箱子中；
- 禁止将工具堆叠至箱子中（可配置）。

**现有功能优化**
- 修改模组配置文件后，配置会立即生效（在之前的版本中，需要退出并重新进入存档后，修改了的配置才会生效）；
- 修改分类菜单内物品的排序，使用游戏内的排序方法；
- 修改分类菜单的排序，常用的分类标签将更加靠前。

### 1.7.0

**Bug 修复**
- 修复了使用 mod “堆叠” 功能，一次性将多于 999 个物品放入箱子中时，会导致物品数量错误的 bug（详见原项目 _[issue #30](https://github.com/aEnigmatic/ConvenientChests/issues/30)_ ）；
- 修复了在带有滚动条的物品分类标签页点击左上角的“全选”按钮时，只会选中当前所显示的物品的 bug。

**新功能**
- 新增 i18n 支持；
- 新增了一个配置选项，用于禁止按照字母顺序排序物品分类标签页标题，建议中文用户不要启用此选项。
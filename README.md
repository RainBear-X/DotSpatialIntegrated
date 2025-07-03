# DotSpatialIntegrated

本项目是一个基于 **DotSpatial 1.7** 的 GIS 示例程序，采用模块化设计，提供矢量与栅格数据的基本操作、投影转换及分析等功能。解决方案包含三层结构：

- **App**：桌面主程序，负责加载模块并提供统一的用户界面。
- **Core**：核心库，封装地图上下文 `MapContext`、绘图服务 `DrawService`、投影服务 `ProjectionService` 等公共功能。
- **Modules**：功能模块集合，每个模块实现 `IRunnableModule` 接口，通过反射自动加载并出现在菜单中。

## 功能概述

### 地图基础操作
- 加载 Shapefile 或栅格文件
- 平移、缩放、选择要素
- 打印布局

### 矢量数据处理
- 创建并保存点、线、面 Shapefile
- 属性表增删列、查询与高亮
- 查看和同步属性表

### 栅格数据处理
- 加载栅格、栅格重分类、乘法运算
- 生成 Hillshade
- 点击地图读取栅格值

### 投影与坐标
- 显示当前图层投影信息
- 投影比较与面积计算
- 图层重投影（结果另存为新的 Shapefile）

### 其他分析
- 简易的徒步路径剖面绘制

## 项目结构

```
DotSpatialIntegrated.sln        解决方案文件
src/
  App/                          主程序项目
  Core/                         核心库
  Modules/                      功能模块
ProjectionExplorerForm/         额外的投影演示程序
```

各模块目录大致对应菜单分类，例如 `MapBasics`、`RasterOps`、`Shapefile`、`Projection` 等。运行时 `MainForm` 会扫描程序集并按模块的 `Category` 构建菜单。

## 使用方法

1. 安装 Visual Studio 2022（或兼容的 .NET Framework 4.7.2 开发环境）。
2. 克隆仓库并在 VS 中打开 `DotSpatialIntegrated.sln`。
3. 还需准备 `DotSpatial_Full.1.7` 及相关依赖放置在项目引用的相对路径（`4/` 目录）。GDAL 库位于 `Data Extensions/gdal/x86`，启动时由 `GdalBoot.Init()` 进行配置。
4. 编译并启动 `App` 项目，即可看到包含各模块的主窗口。
5. 通过菜单加载栅格或矢量文件，尝试重分类、绘制 Shapefile 等功能。

由于示例项目主要面向教学演示，部分类（如 `LayerManager`、`SymbologyAttr/ShapeModule`）尚未实现，可根据需要自行扩展。

## 许可

代码采用 MIT 协议发布，欢迎在学习和研究中自由使用。

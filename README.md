# DotSpatialIntegrated

该项目是一个基于 **DotSpatial** 开发的 GIS 桌面应用，使用 C# (.NET Framework 4.7.2) 编写。程序以模块化方式组织，通过反射自动加载实现 `IRunnableModule` 接口的功能模块，并在主窗口的菜单中呈现。

## 功能概述

- **图层管理与基本操作**：支持加载矢量/栅格数据、清空图层、平移、缩放、信息查询、框选等基本 GIS 操作【F:src/Modules/MapBasics/LoadShapefileModule.cs†L1-L28】【F:src/Modules/MapBasics/ZoomInModule.cs†L1-L15】。
- **Shapefile 创建与保存**：提供绘制点、线、面并保存为 Shapefile 的工具【F:src/Modules/Shapefile/CreatePointShpModule.cs†L1-L21】。
- **栅格处理**：支持加载栅格、计算 Hillshade、按倍数相乘、重分类等操作，同时可在地图上点击查看栅格像元值【F:src/Modules/RasterOps/LoadRasterModule.cs†L1-L28】【F:src/Modules/RasterOps/ReclassifyRasterModule.cs†L1-L159】。
- **属性表操作**：查看/同步属性表、添加或删除字段、条件查询并高亮、唯一值着色以及导出到 Excel 等功能【F:src/Modules/Attribute/ViewAttributeTableModule.cs†L1-L31】【F:src/Modules/Attribute/AddColumnModule.cs†L1-L62】【F:src/Modules/Attribute/ExportAttributeModule.cs†L1-L47】。
- **投影与面积计算**：可查看当前地图投影、重投影图层、比较投影及计算总面积【F:src/Modules/Projection/ReprojectLayerModule.cs†L1-L41】【F:src/Core/ProjectionService.cs†L1-L75】。
- **空间分析**：集成 Fast‑DBSCAN 点/线聚类算法，以及登山路径海拔剖面分析【F:src/Modules/Analysis/FastDbscan/FastDbscanModule.cs†L1-L94】【F:src/Modules/Analysis/HikingPathModule.cs†L1-L18】。
- **地图打印**：可调用 DotSpatial 的布局窗口进行打印【F:src/Modules/Printing/PrintLayoutModule.cs†L1-L20】。

## 项目结构

```
DotSpatialIntegrated/
├── src/
│   ├── App/      # 应用入口及主窗体
│   ├── Core/     # 核心服务与算法库
│   └── Modules/  # 各类功能模块
└── packages/     # NuGet 依赖包（离线）
```

- **App**：`Program.cs` 启动 `MainForm`，在窗体加载时扫描并构建菜单【F:src/App/MainForm.cs†L26-L91】。
- **Core**：提供 `MapContext`、`DrawService`、GDAL 初始化、投影及面积计算、聚类算法等核心功能【F:src/Core/DrawService.cs†L1-L151】【F:src/Core/GdalBoot.cs†L1-L39】。
- **Modules**：实现 `IRunnableModule` 接口的各类独立功能，编译后由主程序反射加载。

## 使用方法

1. **准备环境**：项目基于 .NET Framework 4.7.2，可在 Visual Studio 2017 及以上版本打开解决方案 `DotSpatialIntegrated.sln`。首次打开需还原 NuGet 包（已在 `packages` 目录提供）。
2. **GDAL 配置**：运行程序前请确保 `Data Extensions/gdal/x64` 或 `x86` 目录存在，或通过 `GDAL_HOME` 环境变量指定路径，程序会在启动时调用 `GdalBoot.Init()` 设置 GDAL【F:src/Core/GdalBoot.cs†L11-L34】。
3. **运行程序**：编译并启动 `App` 项目，主窗体包含地图控件、图例和属性表区域。菜单根据已加载的模块生成，可按需选择使用。
4. **常见流程示例**：
   - 通过“File/加载矢量”或“Raster/加载栅格”打开数据。
   - 使用“Shapefile/Point(Create/Save)”等命令绘制并保存要素。
   - 在“Attribute”菜单下查看属性表、添加字段或导出至 Excel。
   - 选中图层后，执行“Analysis/Fast DBSCAN”进行聚类分析。
   - 需要打印时，可在“Map/打印地图”中打开布局窗口。

## 许可

本项目源代码仅用于学习交流，暂无明确授权协议。

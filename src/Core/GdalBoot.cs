using DotSpatial.Data.Rasters.GdalExtension;
using System.IO;
using System;

namespace Core
{
    public static class GdalBoot
    {
        private static bool _inited;

        /// <summary>确保 GDAL 只初始化一次</summary>
        public static void Init()
        {
            if (_inited) return;

            // 设置正确的 GDAL DLL 路径
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var arch = Environment.Is64BitProcess ? "x64" : "x86";
            var gdalPath = Path.Combine(baseDir, "Data Extensions", "gdal", arch);
            if (!Directory.Exists(gdalPath))
            {
                // 允许从配置或环境变量获取备用目录
                gdalPath = Environment.GetEnvironmentVariable("GDAL_HOME") ?? gdalPath;
            }
            if (!Directory.Exists(gdalPath))
                throw new DirectoryNotFoundException($"GDAL 目录未找到：{gdalPath}");

            if (Directory.Exists(gdalPath))
            {
                // 更新 PATH 环境变量
                Environment.SetEnvironmentVariable("PATH",
                    Environment.GetEnvironmentVariable("PATH") + ";" + gdalPath);

                // 设置 GDAL 特定的环境变量
                Environment.SetEnvironmentVariable("GDAL_DATA",
                    Path.Combine(gdalPath, "gdal-data"));
                Environment.SetEnvironmentVariable("PROJ_LIB",
                    Path.Combine(gdalPath, "projlib"));
            }
            else
            {
                throw new DirectoryNotFoundException(
                    $"GDAL 目录未找到：{gdalPath}");
            }

            // GDAL 配置
            DotSpatial.Data.Rasters.GdalExtension.GdalConfiguration.ConfigureGdal();
            DotSpatial.Data.Rasters.GdalExtension.GdalConfiguration.ConfigureOgr();

            _inited = true;
        }
    }
}

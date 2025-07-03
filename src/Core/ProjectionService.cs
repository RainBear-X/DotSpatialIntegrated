using System.Data;
using System.Linq;
using DotSpatial.Data;
using DotSpatial.Projections;
using DotSpatial.Topology;

namespace Core
{
    /// <summary>
    /// 投影与面积服务：手动复制要素集并重投影，不使用 ICloneable 或 Copy&lt;T&gt;。
    /// </summary>
    public static class ProjectionService
    {
        /// <summary>
        /// 手动复制并重投影一个 IFeatureSet 到指定投影。  
        /// </summary>
        /// <param name="src">源要素集</param>
        /// <param name="target">目标投影</param>
        /// <returns>新的、已重投影的 FeatureSet</returns>
        public static FeatureSet ReprojectCopy(IFeatureSet src, ProjectionInfo target)
        {
            // 1. 新建空 FeatureSet，指定几何类型与投影
            var fs = new FeatureSet(src.FeatureType)
            {
                Projection = src.Projection
            };

            // 2. 复制字段结构（列名与类型）
            foreach (DataColumn col in src.DataTable.Columns)
            {
                fs.DataTable.Columns.Add(col.ColumnName, col.DataType);
            }

            // 3. 复制每个要素：克隆几何 + 拷贝属性
            foreach (var feat in src.Features)
            {
                // DotSpatial.Data.IFeature 用 BasicGeometry 取得 DotSpatial.Topology.IGeometry
                var geom = feat.BasicGeometry as IGeometry;
                var geomClone = (IGeometry)geom.Copy();

                var newFeat = fs.AddFeature(geomClone);
                for (int i = 0; i < src.DataTable.Columns.Count; i++)
                {
                    newFeat.DataRow[i] = feat.DataRow[i];
                }
            }

            // 4. 对新 FeatureSet 应用目标投影
            fs.Reproject(target);
            fs.Projection = target;
            return fs;
        }

        /// <summary>计算整个要素集的总面积（投影坐标系下，单位²）。</summary>
        public static double TotalArea(IFeatureSet fs)
        {
            return fs.Features.Sum(f => f.Area());
        }

        /// <summary>计算所选要素的面积之和。</summary>
        public static double SelectedArea(
            System.Collections.Generic.IEnumerable<IFeature> feats)
        {
            return feats.Sum(f => f.Area());
        }
    }
}

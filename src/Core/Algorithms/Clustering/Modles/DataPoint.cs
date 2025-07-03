using System;

namespace Core.Algorithms.Clustering.Models
{
    /// <summary>
    /// 通用数据点模型，支持任意维度
    /// </summary>
    public class DataPoint
    {
        public int Index { get; }          // 原始顺序，回写用
        public double[] Values { get; }    // 特征向量

        public DataPoint(int index, double[] values)
        {
            Index = index;
            Values = values ?? throw new ArgumentNullException(nameof(values));
        }
    }
}

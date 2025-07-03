using static MathNet.Numerics.SpecialFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Algorithms.Clustering.Models;

namespace Core.Algorithms.Clustering
{
    /// <summary>论文公式：根据超体积和 MinPts 估算最优邻域半径</summary>
    public static class EpsEstimator
    {
        public static double Compute(IEnumerable<DataPoint> points, int minPts)
        {
            var arr = points.Select(p => p.Values).ToArray();
            int rows = arr.Length;
            int cols = arr[0].Length;

            // 1) 计算每维最大值、最小值 → 超体积
            double volume = 1d;
            for (int c = 0; c < cols; c++)
            {
                double max = arr.Max(v => v[c]);
                double min = arr.Min(v => v[c]);
                volume *= (max - min);
            }

            // 2) 论文推导公式
            double gamma = Gamma(0.5 * cols + 1);
            double eps = Math.Pow(
                (volume * minPts * gamma) / (rows * Math.Pow(Math.PI, cols / 2d)),
                1d / cols);

            return eps;
        }
    }
}

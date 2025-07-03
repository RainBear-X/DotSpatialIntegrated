using Core.Algorithms.Clustering.Models;
using System;
using System.Collections.Generic;

namespace Core.Algorithms.Clustering.NeighborSearch
{
    /// <summary>朴素 O(n²) 邻域搜索</summary>
    internal class BruteForceSearch
    {
        private readonly IList<DataPoint> _pts;
        private readonly double _eps;

        public BruteForceSearch(IList<DataPoint> pts, double eps)
        {
            _pts = pts;
            _eps = eps;
        }

        public List<int> Query(int idx)
        {
            var res = new List<int>();
            var src = _pts[idx];

            for (int j = 0; j < _pts.Count; j++)
            {
                double dist = Euclidean(src, _pts[j]);
                if (dist <= _eps) res.Add(j);
            }
            return res;
        }

        private static double Euclidean(DataPoint a, DataPoint b)
        {
            double sum = 0;
            for (int i = 0; i < a.Values.Length; i++)
            {
                double d = a.Values[i] - b.Values[i];
                sum += d * d;
            }
            return Math.Sqrt(sum);
        }
    }
}

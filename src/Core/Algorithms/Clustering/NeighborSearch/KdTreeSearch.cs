using KdTree;
using KdTree.Math;
using System.Collections.Generic;
using Core.Algorithms.Clustering.Models;

namespace Core.Algorithms.Clustering.NeighborSearch
{
    /// <summary>基于 NuGet KdTree.Core 的 KD-Tree 邻域搜索</summary>
    internal class KdTreeSearch
    {
        private readonly KdTree<double, int> _tree;
        private readonly double _eps;

        public KdTreeSearch(IList<DataPoint> pts, double eps)
        {
            _eps = eps;
            _tree = new KdTree<double, int>(
                        pts[0].Values.Length,      // ← 直接传维度
                        new DoubleMath());

            for (int i = 0; i < pts.Count; i++)
                _tree.Add(pts[i].Values, i);
        }

        public List<int> Query(int idx, IList<DataPoint> pts)
        {
            var src = pts[idx];
            var results = _tree.RadialSearch(src.Values, _eps);   // 数组
            var list = new List<int>(results.Length);          // ⬅ Length

            foreach (var node in results)
                list.Add(node.Value);

            return list;
        }
    }
}

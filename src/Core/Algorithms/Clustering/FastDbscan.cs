using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Algorithms.Clustering.Extensions;
using Core.Algorithms.Clustering.Models;
using Core.Algorithms.Clustering.NeighborSearch;


namespace Core.Algorithms.Clustering
{
    /// <summary>论文版 Fast-DBSCAN 主实现</summary>
    public static class FastDbscan
    {
        public static ClusterResult Cluster(
            IList<DataPoint> pts,
            int minPts,
            double? epsOverride = null,
            bool useKdTreeSearch = false,
            bool parallel = true)
        {
            if (pts.Count == 0) throw new ArgumentException("points empty");

            // 1) 估算(或覆盖) Eps
            double eps = epsOverride ?? EpsEstimator.Compute(pts, minPts);

            // 2) 选择邻域搜索策略
            Func<int, List<int>> neighbor;
            if (useKdTreeSearch)
            {
                var kd = new KdTreeSearch(pts, eps);
                neighbor = i => kd.Query(i, pts);
            }
            else
            {
                var bf = new BruteForceSearch(pts, eps);
                neighbor = bf.Query;
            }

            int n = pts.Count;
            var labels = new int[n];   // 0: 未访问, -1: 噪声
            var touched = new bool[n];
            int clusterId = 1;

            Action<int> visit = i =>
            {
                if (touched[i]) return;
                var nbr = neighbor(i);
                if (nbr.Count < minPts + 1)
                {
                    labels[i] = -1;
                    touched[i] = true;
                    return;
                }

                labels[i] = clusterId;
                var queue = new Queue<int>(nbr);
                while (queue.Count > 0)
                {
                    int cur = queue.Dequeue();
                    if (touched[cur]) continue;
                    touched[cur] = true;

                    var nbr2 = neighbor(cur);
                    if (nbr2.Count >= minPts + 1)
                        foreach (int nb2 in nbr2)
                            if (!queue.Contains(nb2))   // 避免重复
                                queue.Enqueue(nb2);

                    foreach (int nb in nbr2)
                        if (labels[nb] == 0) labels[nb] = clusterId;
                }
                clusterId++;
            };

            // 3) 主循环（可并行）。并行时要保证 clusterId 自增串行，故采用 for+lock
            if (parallel)
            {
                object locker = new object();
                Parallel.For(0, n, i =>
                {
                    lock (locker)
                    {
                        if (!touched[i]) visit(i);
                    }
                });
            }
            else
            {
                for (int i = 0; i < n; i++)
                    if (!touched[i]) visit(i);
            }

            // 4) 把仍为 0 的标成噪声
            for (int i = 0; i < n; i++)
                if (labels[i] == 0) labels[i] = -1;

            return new ClusterResult(labels, clusterId - 1, eps, minPts);
        }
    }
}

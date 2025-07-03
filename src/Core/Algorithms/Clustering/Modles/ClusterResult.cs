using System.Linq;

namespace Core.Algorithms.Clustering.Models
{
    /// <summary>DBSCAN 聚类结果容器</summary>
    public class ClusterResult
    {
        public int[] Labels { get; }
        public int ClusterCount { get; }
        public double Eps { get; }
        public int MinPts { get; }
        public int NoiseCount => Labels.Count(l => l == -1);

        public ClusterResult(int[] labels, int clusterCount, double eps, int minPts)
        {
            Labels = labels;
            ClusterCount = clusterCount;
            Eps = eps;
            MinPts = minPts;
        }
    }
}

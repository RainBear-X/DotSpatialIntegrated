using System.Collections.Generic;

namespace Core.Algorithms.Clustering.Extensions
{
    /// <summary>常用集合扩展</summary>
    public static class EnumerableExtensions
    {
        /// <summary>将 newItems 加入 list，且避免重复</summary>
        public static void AddUnique<T>(this IList<T> list, IEnumerable<T> newItems)
        {
            var set = new HashSet<T>(list);
            foreach (var item in newItems)
                if (set.Add(item)) list.Add(item);
        }
    }
}

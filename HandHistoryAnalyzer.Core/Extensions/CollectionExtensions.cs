using System.Collections.Generic;

namespace HandHistoryAnalyzer.Core.Extensions
{
    /// <summary>
    /// Extensions for `ICollection<T>`.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Adds every item from `range` to `collection`.
        /// </summary>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> range)
        {
            foreach (var item in range)
            {
                collection.Add(item);
            }
        }
    }
}

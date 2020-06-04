using System;
using System.Collections.Generic;

namespace ClearlyAgile.TimeTracker.Shared
{
    public static class CollectionExtensions
    {
        public static void AddRange<TType>(this ICollection<TType> destination, IEnumerable<TType> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            List<TType> list = destination as List<TType>;

            if (list != null) // try and take advantage of the List's AddRange() functionality, otherwise.... loop time :D
            {
                list.AddRange(source);
            }
            else
            {
                foreach (TType item in source)
                {
                    destination.Add(item);
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace PEngine
{
    public static class Utilities
    {
        internal static void TryUpdateCounterOrAdd<T>(this Dictionary<T, Int32> countersMap, T item)
        {
            Int32 counter;

            if (countersMap.TryGetValue(item, out counter))
            {
                countersMap[item] = counter + 1;
            }
            else
            {
                countersMap.Add(item, 1);
            }
        }
    }
}

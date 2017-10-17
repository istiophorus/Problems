using System;
using System.Collections.Generic;

namespace Ranges
{
    public static class ListItemTools
    {
        public static T[] ToArray<T>(this ListItem<T> item)
        {
            if (null == item)
            {
                return new T[0];
            }

            List<T> results = new List<T>();

            while (item != null)
            {
                results.Add(item.Data);

                item = item.Next;
            }

            return results.ToArray();
        }

        public static ListItem<T> ToList<T>(this T[] input)
        {
            if (null == input)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (input.Length <= 0)
            {
                return null;
            }

            ListItem<T> item = null;

            for (int q = input.Length - 1; q >= 0; q--)
            {
                ListItem<T> old = item;

                item = new ListItem<T>(old);
                item.Data = input[q];
            }

            return item;
        }
    }
}

using System;
using System.Collections.Generic;

namespace Coursework2021Api.Utils
{
    public static class Utils
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (action == null) throw new ArgumentNullException("action");

            foreach (T item in source)
            {
                action(item);
            }
        }

        public static int ToInt(this bool value)
        {
            return value ? 1 : 0;
        }

        public static int? ToInt(this bool? value)
        {
            if (value == null) return null;
            return value.Value ? 1 : 0;
        }

        public static bool? ToBool(this int? value)
        {
            if (value == null) return null;
            return value.Value == 1;
        }

    }
}
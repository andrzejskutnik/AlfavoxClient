using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AlfavoxClient
{
    public static class ExString
    {
        public static IEnumerable<int> ParseModel(this string model)
        {
            if (string.IsNullOrEmpty(model)) return null;
            var toParse = model.Split(',');
            var tempList = new List<int>();

            toParse.ForEach(m => { if (int.TryParse(m, out int i)) tempList.Add(i); });

            if (tempList.Count() == 1)
            {
                return new List<int> { tempList.First() };
            }
            return tempList;
        }
    }

    public static class ExEnumerable
    {
        [DebuggerStepThrough]
        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
                action(item);
        }
    }
}
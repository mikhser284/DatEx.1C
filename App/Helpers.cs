using System;
using System.Collections.Generic;
using System.Text;

namespace App
{
    public static class Ext_Linq
    {
        public static IEnumerable<IEnumerable<T>> Paginate<T>(this IEnumerable<T> source, Int32 pageSize)
        {
            IEnumerator<T> enumerator = source.GetEnumerator();
            while (enumerator.MoveNext()) yield return NextPartition(enumerator, pageSize);
        }

        private static IEnumerable<T> NextPartition<T>(IEnumerator<T> enumerator, Int32 blockSize)
        {
            do
            {
                yield return enumerator.Current;
            } while (--blockSize > 0 && enumerator.MoveNext());
        }

        public static IEnumerable<ObjType> ObjsWithDistinctValues<ObjType, ValType>(this IEnumerable<ObjType> source, Func<ObjType, ValType> keySelector)
        {
            HashSet<ValType> seenKeys = new HashSet<ValType>();
            foreach (ObjType element in source)
                if (element != null && seenKeys.Add(keySelector(element)))
                    yield return element;
        }

        public static IEnumerable<ValType> DistinctValues<ObjType, ValType>(this IEnumerable<ObjType> source, Func<ObjType, ValType> keySelector)
        {
            HashSet<ValType> values = new HashSet<ValType>();
            foreach (ObjType element in source)
            {
                ValType val = keySelector(element);
                if (element != null && values.Add(keySelector(element))) yield return val;
            }
        }

        public static IEnumerable<ValType> DistinctValuesExcluding<ObjType, ValType>(this IEnumerable<ObjType> source, IEnumerable<ValType> excludableValues, Func<ObjType, ValType> keySelector)
        {
            HashSet<ValType> excludableVals = new HashSet<ValType>(excludableValues);
            HashSet<ValType> values = new HashSet<ValType>();
            foreach (ObjType element in source)
            {
                ValType val = keySelector(element);
                if (element != null && !excludableVals.Contains(val) && values.Add(keySelector(element))) yield return val;
            }
        }

        public static IEnumerable<ValType> DistinctValuesExcluding<ObjType, ValType>(this IEnumerable<ObjType> source, ValType excludableValue, Func<ObjType, ValType> keySelector)
        {
            HashSet<ValType> excludableVals = new HashSet<ValType>() { excludableValue };
            HashSet<ValType> values = new HashSet<ValType>();
            foreach (ObjType element in source)
            {
                ValType val = keySelector(element);
                if (element != null && !excludableVals.Contains(val) && values.Add(keySelector(element))) yield return val;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OneC = DatEx.OneC.DataModel;

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

        public static Dictionary<TKey, List<TValue>> GroupToDictionaryBy<TKey, TValue>(this IEnumerable<TValue> source, Func<TValue, TKey> keySelector)
        {
            Dictionary<TKey, List<TValue>> groupedSource = new Dictionary<TKey, List<TValue>>();
            foreach(TValue value in source)
            {
                TKey key = keySelector(value);
                if (!groupedSource.ContainsKey(key)) groupedSource.Add(key, new List<TValue> { value });
                else groupedSource[key].Add(value);
            }
            return groupedSource;
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T item in source) action(item);
            return source;
        }

        public static HashSet<Guid> SelectValuableGuids<T>(this IEnumerable<T> source, Func<T, Guid> guidSelector)
        {
            HashSet<Guid> valueableGuids = new HashSet<Guid>();

            foreach(var item in source)
            {
                Guid guid = guidSelector(item);
                if (guid == default(Guid)) continue;
                valueableGuids.Add(guid);
            }

            return valueableGuids;
        }

        public static HashSet<Guid> SelectValuableGuids<T>(this IEnumerable<T> source, Func<T, Guid?> guidSelector)
        {
            HashSet<Guid> valueableGuids = new HashSet<Guid>();

            foreach (var item in source)
            {
                Guid? guid = guidSelector(item);
                if (guid == null || guid == default(Guid)) continue;
                valueableGuids.Add((Guid)guid);
            }

            return valueableGuids;
        }
    }


    public static class Ext_Guid
    {
        private static readonly Guid DefaultValue = default(Guid);

        public static Boolean IsNotNullOrDefault(this Guid? guid) => guid != null && guid != DefaultValue;

        public static Boolean IsNotDefault(this Guid guid) => guid != DefaultValue;
    }

    public static class Ext_DateTime
    {
        private static readonly DateTime DefaultValue = default(DateTime);

        public static Boolean IsNotNullOrDefault(this DateTime? value) => value != null && value != DefaultValue;

        public static Boolean IsNotDefault(this DateTime value) => value != DefaultValue;

        public static DateTime? GetNotDefaultValue(this DateTime? value) => value.IsNotNullOrDefault() ? value : null;

        public static DateTime? GetNotDefaultValue(this DateTime value) => value.IsNotDefault() ? (DateTime?)value : null;
    }

    public static class Ext_Task
    {
        public static Task[] StartAndWaitForAll(this Task[] tasks)
        {
            tasks.ForEach(task => task.Start());
            Task.WaitAll(tasks);
            return tasks;
        }
    }

    public static class Ext_OneCObject
    {
        public static List<T> ShowOneCObjects<T>(this List<T> objects) where T : OneC.OneCObject
        {
            foreach (T obj in objects)
            {
                obj.Show();
                Console.WriteLine($"\n\n");
            }
            return objects;
        }
    }
}

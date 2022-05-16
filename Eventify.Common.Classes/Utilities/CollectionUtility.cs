using System;
using System.Collections.Generic;
using System.Linq;

namespace Eventify.Common.Classes.Utilities
{
    public static class CollectionUtility
    {
        public static IEnumerable<T> MakeCollection<T>(this T value)
        {
            if (!value.IsNullOrDefault())
            {
                yield return value;
            }
        }

        public static IList<T> MakeList<T>(this T value)
        {
            return value.MakeCollection().ToList();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            if (collection != null)
            {
                return !collection.Any();
            }

            return true;
        }

        public static bool AllHaveTheSameValue<T, TPropertyType>(this IEnumerable<T> entities, Func<T, TPropertyType> selector)
        {
            IList<TPropertyType> differentValues;
            return entities.AllHaveTheSameValue(selector, out differentValues);
        }

        public static bool AllHaveTheSameValue<T, TPropertyType>(this IEnumerable<T> entities, Func<T, TPropertyType> selector, out IList<TPropertyType> differentValues)
        {
            if (entities == null)
            {
                differentValues = Enumerable.Empty<TPropertyType>().ToList();
                return true;
            }

            differentValues = entities.Select(selector).Distinct().ToList();
            return differentValues.Count == 1;
        }

        public static IEnumerable<List<T>> SplitList<T>(this List<T> locations, int nSize = 30)
        {
            for (int i = 0; i < locations.Count; i += nSize)
            {
                yield return locations.GetRange(i, Math.Min(nSize, locations.Count - i));
            }
        }

        public static IEnumerable<IEnumerable<T>> ToEnumerableBatch<T>(this IEnumerable<T> source, int size)
        {
            T[]? array = null;
            int num = 0;
            foreach (T item in source)
            {
                if (array == null)
                {
                    array = new T[size];
                }

                array[num++] = item;
                if (num == size)
                {
                    yield return array.Select((x) => x);
                    array = null;
                    num = 0;
                }
            }

            if (array != null && num > 0)
            {
                yield return array.Take(num);
            }
        }

        public static bool CompareCollections<T>(this IEnumerable<T> first, IEnumerable<T> second) where T : IComparable<T>
        {
            if (first == null || second == null)
            {
                return false;
            }

            List<T> list = first.OrderBy((x) => x).ToList();
            List<T> list2 = second.OrderBy((x) => x).ToList();
            if (list.Count != list2.Count)
            {
                return false;
            }

            for (int i = 0; i < list.Count(); i++)
            {
                if (!list[i].Equals(list2[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public static IEnumerable<T> TakeAndRemove<T>(this IList<T> list, int count)
        {
            count = Math.Min(list.Count, count);
            for (int i = 0; i < count; i++)
            {
                T val = list.First();
                list.Remove(val);
                yield return val;
            }
        }

        public static T TakeAndRemove<T>(this IList<T> list)
        {
            T val = list.First();
            list.Remove(val);
            return val;
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource item in source)
            {
                if (seenKeys.Add(keySelector(item)))
                {
                    yield return item;
                }
            }
        }

        public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.OrderBy(keySelector).FirstOrDefault();
        }

        public static TSource MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.OrderBy(keySelector).LastOrDefault();
        }
    }
}

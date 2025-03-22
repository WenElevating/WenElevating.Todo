using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WenElevating.Todo.Extensions
{
    public static class ObservableCollectionExtension
    {
        /// <summary>
        /// 交换集合中两个元素的位置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="first"></param>
        /// <param name="second"></param>
        public static void Swap<T>(this ObservableCollection<T> collection, int first, int second)
        {
            if (first < 0 || first >= collection.Count || second < 0 || second >= collection.Count)
            {
                return;
            }
            T temp = collection[first];
            collection[first] = collection[second];
            collection[second] = temp;
        }

        public static void Swap<T>(this ObservableCollection<T> collection, T firstData, T secondData)
        {
            if (firstData == null || secondData == null)
            {
                return;
            }

            var first = collection.IndexOf(firstData);
            var second = collection.IndexOf(secondData);

            if (first == -1 || second == -1)
            {
                return;
            }

            T temp = collection[first];
            collection[first] = collection[second];
            collection[second] = temp;
        }
    }
}

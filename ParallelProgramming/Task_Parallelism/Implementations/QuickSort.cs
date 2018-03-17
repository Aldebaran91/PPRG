//#define PARALLEL
#define THRESHHOLD

namespace QuickSort
{
    using System;
    using System.Threading.Tasks;

    public class QuickSort<T> where T : IComparable
    {
        public void Sort(T[] entries)
        {
            Sort(entries, 0, entries.Length - 1);
        }

        public void Sort(T[] entries, Int32 first, Int32 last)
        {
            var length = last + 1 - first;

            if (length > 1)
            {
                var median = GetMedian(entries, first, last);

                var left = first;
                var right = last;
                partition(entries, median, ref left, ref right);

                var leftLength = right + 1 - first;
                var rightLength = last + 1 - left;

#if PARALLEL
                Task t1 = Task.Factory.StartNew(() =>
                {
                    Sort(entries, first, right);
                });
                Task t2 = Task.Factory.StartNew(() =>
                {
                    Sort(entries, left, last);
                });
                Task.WaitAll(new Task[] { t1, t2 });
#elif THRESHHOLD
                if (length < entries.Length / 4)
                {
                    Sort(entries, first, right);
                    Sort(entries, left, last);
                }
                else
                {
                    Task t1 = Task.Factory.StartNew(() =>
                    {
                        Sort(entries, first, right);
                    });
                    Task t2 = Task.Factory.StartNew(() =>
                    {
                        Sort(entries, left, last);
                    });
                    Task.WaitAll(new Task[] { t1, t2 });
                }
#else
                Sort(entries, first, right);
                Sort(entries, left, last);
#endif
            }
        }

        private T GetMedian(T[] entries, Int32 first, Int32 last)
        {
            return entries[(first + last) / 2];
        }

        private void partition(T[] entries, T median, ref Int32 left, ref Int32 right)
        {
            var first = left;
            var last = right;

            while (true)
            {
                while (median.CompareTo(entries[left]) > 0) left++;
                while (median.CompareTo(entries[right]) < 0) right--;

                if (right <= left) break;

                Swap(entries, left, right);
                left++;
                right--;
            }

            if (left == right)
            {
                left++;
                right--;
            }
        }

        public void Swap(T[] entries, Int32 index1, Int32 index2)
        {
            if (index1 != index2)
            {
                var entry = entries[index1];
                entries[index1] = entries[index2];
                entries[index2] = entry;
            }
        }
    }
}
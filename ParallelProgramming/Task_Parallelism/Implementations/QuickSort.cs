//#define PARALLEL
//#define THRESHHOLD

namespace QuickSort
{
    using System;
    using System.Threading.Tasks;

    static class InsertionSort<T> where T : IComparable
    {
        public static void Sort(T[] entries, Int32 first, Int32 last)
        {
            for (var index = first + 1; index <= last; index++)
                insert(entries, first, index);
        }

        private static void insert(T[] entries, Int32 first, Int32 index)
        {
            var entry = entries[index];
            while (index > first && entries[index - 1].CompareTo(entry) > 0)
                entries[index] = entries[--index];
            entries[index] = entry;
        }
    }

    public class QuickSort<T> where T : IComparable
    {
        private const int MinLength = 30;
        private const Int32 insertionLimitDefault = 12;
        public Int32 InsertionLimit { get; set; }
        protected Random Random { get; set; }

        public QuickSort()
        {
            Random = new Random();
        }

        public void Sort(T[] entries)
        {
            Sort(entries, 0, entries.Length - 1);
        }

        public void Sort(T[] entries, Int32 first, Int32 last)
        {
            var length = last + 1 - first;
            while (length > 1)
            {
                if (length < InsertionLimit)
                {
                    InsertionSort<T>.Sort(entries, first, last);
                    return;
                }


                var median = pivot(entries, first, last);

                var left = first;
                var right = last;
                partition(entries, median, ref left, ref right);

                var leftLength = right + 1 - first;
                var rightLength = last + 1 - left;

                if (leftLength < rightLength)
                {
#if PARALLEL
                    Task.Factory.StartNew(() =>
                    {
                        int newFirst = first;
                        int newRight = right;
                        Sort(entries, newFirst, newRight);
                    });
                    Task.WaitAll();
#elif THRESHHOLD
                    if (entries.Length < MinLength)
                        Sort(entries, first, right);
                    else
                        Task.Factory.StartNew(() => Sort(entries, first, right));
#else
                    Sort(entries, first, right);
#endif

                    first = left;
                    length = rightLength;
                }
                else
                {
#if PARALLEL
                    Task.Factory.StartNew(() => 
                    {
                        int newLeft = left;
                        int newLast = last;
                        Sort(entries, newLeft, newLast);
                    });
                    Task.WaitAll();
#elif THRESHHOLD
                    if (entries.Length < MinLength)
                        Sort(entries, left, right);
                    else
                        Task.Factory.StartNew(() => Sort(entries, left, last));
#else
                    Sort(entries, left, last);
#endif

                    last = right;
                    length = leftLength;
                }
            }
        }

        private T pivot(T[] entries, Int32 first, Int32 last)
        {
            var length = last + 1 - first;
            var logLen = (Int32)Math.Log10(length);
            var pivotSamples = 2 * logLen + 1;
            var sampleSize = Math.Min(pivotSamples, length);
            var right = first + sampleSize - 1;

            for (var left = first; left <= right; left++)
            {
                var random = Random.Next(left, last + 1);
                Swap(entries, left, random);
            }

            InsertionSort<T>.Sort(entries, first, right);

            var median = entries[first + sampleSize / 2];
            return median;
        }

        private static void partition(T[] entries, T median, ref Int32 left, ref Int32 right)
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

        public static void Swap(T[] entries, Int32 index1, Int32 index2)
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
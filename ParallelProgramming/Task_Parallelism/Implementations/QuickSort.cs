#define PARALLEL
//#define THRESHHOLD

namespace QuickSort
{
    using System;
    using System.Threading.Tasks;

    public class QuickSort<T> where T : IComparable
    {
        private const int MinLength = 20;

        public void Sort(T[] entries)
        {
            Sort(entries, 0, entries.Length - 1, 0);

            Task.WaitAll();
        }

        public void Sort(T[] entries, Int32 first, Int32 last, int recursion = 0)
        {
            ++recursion;

            var length = last + 1 - first;
            while (length > 1)
            {
                var median = this.GetMedian(entries, first, last);

                var left = first;
                var right = last;
                partition(entries, median, ref left, ref right);

                var leftLength = right + 1 - first;
                var rightLength = last + 1 - left;

                if (leftLength < rightLength)
                {
#if PARALLEL
                    var t = Task.Run(() =>
                    {
                        int newFirst = first;
                        int newRight = right;
                        Sort(entries, newFirst, newRight, recursion);
                    });
                    t.Wait();
#elif THRESHHOLD
                    if (entries.Length < MinLength)
                        Sort(entries, first, right);
                    else
                    {
                        var t = Task.Run(() =>
                        {
                            int newFirst = first;
                            int newRight = right;
                            Sort(entries, newFirst, newRight, recursion);
                        });
                        t.Wait();
                    }
#else
                    Sort(entries, first, right, recursion);
#endif

                    first = left;
                    length = rightLength;
                }
                else
                {
#if PARALLEL
                    var t = Task.Run(() => 
                    {
                        int newLeft = left;
                        int newLast = last;
                        Sort(entries, newLeft, newLast, recursion);
                    });
                    t.Wait();
#elif THRESHHOLD
                    if (entries.Length < MinLength)
                        Sort(entries, left, right);
                    else
                    {
                        var t = Task.Run(() =>
                        {
                            int newLeft = left;
                            int newLast = last;
                            Sort(entries, newLeft, newLast, recursion);
                        });
                        t.Wait();
                    }
#else
                    Sort(entries, left, last, recursion);
#endif

                    last = right;
                    length = leftLength;
                }
            }
        }

        private T GetMedian(T[] entries, Int32 first, Int32 last)
        {
            return entries[(first + last) / 2];
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
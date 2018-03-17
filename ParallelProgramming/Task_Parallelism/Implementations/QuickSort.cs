namespace QuickSort
{
    using System;
    
    #region Insertion Sort
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
    #endregion

    public class QuickSort<T> where T : IComparable
    {
        #region Constants
        private const Int32 insertionLimitDefault = 12;
        #endregion

        #region Properties
        public Int32 InsertionLimit { get; set; }
        protected Random Random { get; set; }
        #endregion

        #region Constructors
        public QuickSort(Int32 insertionLimit, Random random)
        {
            InsertionLimit = insertionLimit;
            Random = random;
        }

        public QuickSort(Int32 insertionLimit)
          : this(insertionLimit, new Random())
        {
        }

        public QuickSort()
          : this(insertionLimitDefault)
        {
        }
        #endregion

        #region Sort Methods
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
                    Sort(entries, first, right);
                    first = left;
                    length = rightLength;
                }
                else
                {
                    Sort(entries, left, last);
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

        private static void swapOut(T[] entries, T median, Int32 left, Int32 right,
                                    ref Int32 leftMedian, ref Int32 rightMedian)
        {
            if (median.CompareTo(entries[left]) == 0) Swap(entries, leftMedian++, left);
            if (median.CompareTo(entries[right]) == 0) Swap(entries, right, rightMedian--);
        }

        private static void swapIn(T[] entries, ref Int32 left, ref Int32 right,
                                   Int32 leftMedian, Int32 rightMedian, Int32 first, Int32 last)
        {
            for (var prefix = first; prefix < leftMedian;)
                Swap(entries, prefix++, right--);
            for (var suffix = last; rightMedian < suffix;)
                Swap(entries, left++, suffix--);
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
    #endregion
}
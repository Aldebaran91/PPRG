using System;
using System.Threading.Tasks;
using static Task_Parallelism.Program;

namespace QuickSort
{
    public class QuickSort<T> where T : IComparable
    {
        public Modus Mode = Modus.Sequential;
        public int THRESHHOLD = int.MaxValue;

        public void Sort(T[] entries)
        {
            if (Mode == Modus.Threshhold && THRESHHOLD == int.MaxValue)
                THRESHHOLD = entries.Length / 4;

            Sort(entries, 0, entries.Length - 1);
        }

        public void Sort(T[] entries, long first, long last)
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

                switch (Mode)
                {
                    case Modus.Parallel:
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
                            break;
                        }
                    case Modus.Threshhold:
                        {
                            if (length < THRESHHOLD)
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
                            break;
                        }
                    case Modus.Sequential:
                        {
                            Sort(entries, first, right);
                            Sort(entries, left, last);
                            break;
                        }
                }
            }
        }

        private T GetMedian(T[] entries, long first, long last)
        {
            return entries[(first + last) / 2];
        }

        private void partition(T[] entries, T median, ref long left, ref long right)
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

        public void Swap(T[] entries, long index1, long index2)
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
//#define PARALLEL
//#define THRESHHOLD

namespace MergeSort
{
    using System;
    using System.Threading.Tasks;
    using static Task_Parallelism.Program;

    public class MergeSort<T> where T : IComparable
    {
        public Modus Mode = Modus.Sequential;
        public int THRESHHOLD = int.MaxValue;

        public void Sort(T[] entries)
        {
            if (Mode == Modus.Threshhold && THRESHHOLD == int.MaxValue)
                THRESHHOLD = entries.Length / 4;

            Sort(entries, 0, entries.Length - 1);
        }

        // Top-Down K-way Merge Sort
        public void Sort(T[] entries1, long first, long last)
        {
            var length = last + 1 - first;
            if (length < 2)
                return;
            
            var left = first;
            var leftLast = (first + last) / 2;
            var right = leftLast + 1;
            var rightLast = last;

            switch (Mode)
            {
                case Modus.Parallel:
                    {
                        Task t1 = Task.Factory.StartNew(() =>
                        {
                            Sort(entries1, left, leftLast);
                        });
                        Task t2 = Task.Factory.StartNew(() =>
                        {
                            Sort(entries1, right, rightLast);
                        });
                        Task.WaitAll(new Task[] { t1, t2 });
                        break;
                    }
                case Modus.Threshhold:
                    {
                        if (length < THRESHHOLD)
                        {
                            Sort(entries1, left, leftLast);
                            Sort(entries1, right, rightLast);
                        }
                        else
                        {
                            Task t1 = Task.Factory.StartNew(() =>
                            {
                                Sort(entries1, left, leftLast);
                            });
                            Task t2 = Task.Factory.StartNew(() =>
                            {
                                Sort(entries1, right, rightLast);
                            });
                            Task.WaitAll(new Task[] { t1, t2 });
                        }
                        break;
                    }
                case Modus.Sequential:
                    {
                        Sort(entries1, left, leftLast);
                        Sort(entries1, right, rightLast);
                        break;
                    }
            }

            Merge(entries1, left, leftLast, right, rightLast);
        }

        public void Merge(T[] entries, long left, long leftLast, long right, long rightLast)
        {
            T[] newArray = new T[rightLast - left + 1];
            long leftPoint = left, rightPoint = right, newPoint = 0;

            while (leftPoint <= leftLast || rightPoint <= rightLast)
            {
                if (leftPoint <= leftLast && rightPoint <= rightLast)
                {
                    if (entries[leftPoint].CompareTo(entries[rightPoint]) < 0)
                        newArray[newPoint++] = entries[leftPoint++];
                    else
                        newArray[newPoint++] = entries[rightPoint++];
                    continue;
                }
                else if (leftPoint <= leftLast)
                {
                    newArray[newPoint++] = entries[leftPoint++];
                    continue;
                }
                else
                {
                    newArray[newPoint++] = entries[rightPoint++];
                }
            }

            for (long i = left; i <= rightLast; i++)
            {
                entries[i] = newArray[i - left];
            }
        }
    }
}
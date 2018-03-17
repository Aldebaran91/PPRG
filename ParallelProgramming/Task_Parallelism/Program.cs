using MergeSort;
using QuickSort;
using System;
using System.Linq;
using System.Diagnostics;

namespace Task_Parallelism
{
    class Program
    {
        const int lenght = 5;
        const int laps = 100;

        static void Main(string[] args)
        {
            byte[] testArray = new byte[]
                { 2, 5, 0, 7, 0};
            //byte[] testArray = new byte[]
            //    { 228, 199, 20, 30, 113, 15, 125, 5, 242, 112, 55, 213, 92, 136, 167 };
            Random rdm = new Random();
            //rdm.NextBytes(testArray);
            var sorted = testArray.OrderBy(x => x).ToArray();

            var firstQuickSort = (byte[])testArray.Clone();
            var quickSort = new QuickSort<byte>();
            quickSort.Sort(firstQuickSort);

            if (!firstQuickSort.SequenceEqual(sorted))
            {
                throw new Exception();
            }

            var firstMergeSort = (byte[])testArray.Clone();
            var mergeSort = new MergeSort<byte>();
            mergeSort.Sort(firstMergeSort);

            if (!firstMergeSort.SequenceEqual(sorted))
            {
                throw new Exception();
            }

            Stopwatch quickSortTime = new Stopwatch();
            Stopwatch mergeSortTime = new Stopwatch();

            for (int i = 0; i < laps; i++)
            {
                byte[] temp = new byte[lenght];
                rdm.NextBytes(temp);
                quickSortTime.Start();
                quickSort.Sort(temp);
                quickSortTime.Stop();
            }

            for (int i = 0; i < laps; i++)
            {
                byte[] temp = new byte[lenght];
                rdm.NextBytes(temp);
                mergeSortTime.Start();
                mergeSort.Sort(temp);
                mergeSortTime.Stop();
            }

            Console.WriteLine($"QuickSort time= {quickSortTime.ElapsedMilliseconds:N0} ms");
            Console.WriteLine($"MergeSort time= {mergeSortTime.ElapsedMilliseconds:N0} ms");

            Console.ReadKey(false);
        }
    }
}

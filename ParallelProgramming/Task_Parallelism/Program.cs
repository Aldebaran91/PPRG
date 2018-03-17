using MergeSort;
using QuickSort;
using System;
using System.Diagnostics;

namespace Task_Parallelism
{
    class Program
    {
        static void Main(string[] args)
        {
            const int lenght = 15;
            const int laps = 1_000_000;

            byte[] testArray = new byte[lenght];
            Random rdm = new Random();
            rdm.NextBytes(testArray);

            var firstQuickSort = (byte[])testArray.Clone();
            var quickSort = new QuickSort<byte>();
            quickSort.Sort(firstQuickSort);

            var firstMergeSort = (byte[])testArray.Clone();
            var mergeSort = new MergeSort<byte>();
            mergeSort.Sort(firstMergeSort);

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

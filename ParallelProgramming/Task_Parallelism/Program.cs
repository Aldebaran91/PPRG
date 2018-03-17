using MergeSort;
using QuickSort;
using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;

namespace Task_Parallelism
{
    public class Program
    {
        public enum Modus
        {
            Parallel,
            Threshhold,
            Sequential
        }

        const int length = 1_000_000;
        const int loops = 5;

        static void Main(string[] args)
        {
            Random rdm = new Random();
            byte[] testArray = new byte[]
                { 228, 199, 20, 30, 113, 15, 125, 5, 242, 112, 55, 213, 92, 136, 167 };
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
            
            //##################################
            //#####                        #####
            //##### TEST STARTS RIGHT HERE #####
            //#####                        #####
            //##################################   

            Stopwatch quickSortTime = new Stopwatch();
            Stopwatch mergeSortTime = new Stopwatch();
            List<Modus> ModesToTest = new List<Modus>() { Modus.Parallel, Modus.Threshhold, Modus.Sequential };

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("> Author: Philip Burggasser (SE17M003, Paul Vonbank (SE17M025)");
            Console.ResetColor();
            Console.WriteLine($"> Size of array {length:N0}");
            Console.WriteLine($"> {loops}x loops per test mode");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("> QuickSort");
            Console.ResetColor();
            Console.WriteLine("{0,15}{1,15}", "Mode", "Time in ms");
            foreach (var mode in ModesToTest)
            {
                // Setup
                quickSort.Mode = mode;
                byte[] temp = new byte[length];
                rdm.NextBytes(temp);
                
                for (int i = 0; i < loops; i++)
                {
                    // Test
                    quickSortTime.Start();
                    quickSort.Sort(temp);
                    quickSortTime.Stop();
                }

                Console.WriteLine("{0,15}{1,15}", mode, quickSortTime.ElapsedMilliseconds.ToString("N0"));
                quickSortTime.Reset();
            }

            Console.WriteLine();

            //for (int i = 0; i < laps; i++)
            //{
            //    byte[] temp = new byte[length];
            //    rdm.NextBytes(temp);
            //    mergeSortTime.Start();
            //    mergeSort.Sort(temp);
            //    mergeSortTime.Stop();
            //}
            
            Console.ReadKey(false);
        }
    }
}

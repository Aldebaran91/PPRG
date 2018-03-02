using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace DiningPhilosophers
{
    public class Philosopher
    {
        public long WaitForFork = 0;

        public void Eat(object leftFork, object rightFork, int leftForkNr, int rightForkNr,
            int philosopherNumber, int maxThinkingTime, int maxEatingTime, CancellationToken token)
        {
            Stopwatch watch = new Stopwatch();
            Random rm = new Random();

            while (!token.IsCancellationRequested)
            {
                watch.Reset();
                watch.Start();
                // Philosopher is thinking
                Thread.Sleep(rm.Next() % maxThinkingTime);
                Console.WriteLine($"\t{philosopherNumber} philosopher finished thinking");
                lock (leftFork)
                {
                    Console.WriteLine($"\t\t{philosopherNumber} philosopher took first fork {leftForkNr}");
                    lock (rightFork)
                    {
                        watch.Stop();
                        WaitForFork += watch.ElapsedMilliseconds;
                        Console.WriteLine($"\t\t\t{philosopherNumber} philosopher took second fork {rightForkNr}");
                        Thread.Sleep(rm.Next() % maxEatingTime);
                        Console.WriteLine($"\t\t\t{philosopherNumber} philosopher finished eating");
                    }
                    Console.WriteLine($"\t\t{philosopherNumber} philosopher put fork {rightForkNr} back");
                }
                Console.WriteLine($"\t{philosopherNumber} philosopher put fork {leftForkNr} back");
            }

            Console.WriteLine($"{philosopherNumber} philosopher left task");
        }
    }


    class Program
    {
        public static object[] forks;

        static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();
            int n, thinkingTime, eatingTime;
            int waitedForFork = 0;
            string consoleInput;

            Console.Write("Enter number of philosophers: ");
            consoleInput = Console.ReadLine();
            n = int.Parse(consoleInput);

            Console.Write("Enter max thinking time in ms: ");
            consoleInput = Console.ReadLine();
            thinkingTime = int.Parse(consoleInput);

            Console.Write("Enter max eating time in ms: ");
            consoleInput = Console.ReadLine();
            eatingTime = int.Parse(consoleInput);

            Console.WriteLine();

            watch.Start();
            CancellationTokenSource source = new CancellationTokenSource();

            forks = new object[n];
            Task[] pTasks = new Task[n];

            #region Deadlock
            //for (int i = 0; i < n; i++)
            //{
            //    forks[i] = new object();
            //}

            //pTasks[0] = new Task(() =>
            //{
            //    Philosopher p = new Philosopher();
            //    p.Eat(forks[n - 1], forks[0],
            //        n - 1, 0, 0, thinkingTime, eatingTime, source.Token);
            //});
            //pTasks[0].Start();

            //for (int i = 1; i < n; i++)
            //{
            //    int ix = i;
            //    pTasks[i] = new Task(() =>
            //    {
            //        Philosopher p = new Philosopher();
            //        p.Eat(forks[ix - 1], forks[ix],
            //            ix - 1, ix, ix, thinkingTime, eatingTime, source.Token);
            //    });
            //    pTasks[i].Start();
            //}
            #endregion

            #region NoCircularWait
            for (int i = 0; i < n; i++)
            {
                forks[i] = new object();
            }

            pTasks[0] = new Task(() =>
            {
                Philosopher p = new Philosopher();
                p.Eat(forks[0], forks[n - 1],
                    n - 1, 0, 0, thinkingTime, eatingTime, source.Token);

                Interlocked.Add(ref waitedForFork, (int)p.WaitForFork);
            });
            pTasks[0].Start();

            for (int i = 1; i < n; i++)
            {
                int ix = i;
                pTasks[i] = new Task(() =>
                {
                    Philosopher p = new Philosopher();

                    if (n % 2 == 1)
                    {
                        p.Eat(forks[ix - 1], forks[ix],
                            ix - 1, ix, ix, thinkingTime, eatingTime, source.Token);
                    }
                    else
                    {
                        p.Eat(forks[ix], forks[ix - 1],
                            ix - 1, ix, ix, thinkingTime, eatingTime, source.Token);
                    }

                    Interlocked.Add(ref waitedForFork, (int)p.WaitForFork);
                });
                pTasks[i].Start();
            }
            #endregion

            // Let them eat
            Thread.Sleep(1000);

            // Stop eating
            source.Cancel();

            // Wait till all finished
            Task.WaitAll(pTasks);

            watch.Stop();

            Console.WriteLine($"\n{watch.ElapsedMilliseconds}ms waittime\n{waitedForFork}ms sum waited for fork\nPress any key to exit.");
            Console.ReadLine();

        }
    }
}
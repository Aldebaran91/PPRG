using System;
using System.Threading;
using System.Threading.Tasks;

namespace DiningPhilosophers
{
    public class Philosopher
    {
        public void Eat(object leftFork, object rightFork, int leftForkNr, int rightForkNr,
            int philosopherNumber, int maxThinkingTime, int maxEatingTime, CancellationToken token)
        {
            Random rm = new Random();

            while (!token.IsCancellationRequested)
            {
                // Philosopher is thinking
                Thread.Sleep(rm.Next() % maxThinkingTime);
                Console.WriteLine($"\t{philosopherNumber} philosopher finished thinking");

                lock (leftFork)
                {
                    Console.WriteLine($"\t\t{philosopherNumber} philosopher took first fork {leftForkNr}");
                    lock (rightFork)
                    {
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
            int n, thinkingTime, eatingTime;
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

            CancellationTokenSource source = new CancellationTokenSource();

            forks = new object[n];
            for (int i = 0; i < n; i++)
            {
                forks[i] = new object();
            }

            Task[] pTasks = new Task[n];
            pTasks[0] = new Task(() =>
            {
                Philosopher p = new Philosopher();
                p.Eat(forks[n - 1], forks[0],
                    n - 1, 0, 0, thinkingTime, eatingTime, source.Token);
            });
            pTasks[0].Start();

            for (int i = 1; i < n; i++)
            {
                int ix = i;
                pTasks[i] = new Task(() =>
                {
                    Philosopher p = new Philosopher();
                    p.Eat(forks[ix - 1], forks[ix],
                        ix - 1, ix, ix, thinkingTime, eatingTime, source.Token);
                });
                pTasks[i].Start();
            }

            // Let them eat
            Thread.Sleep(1000);

            // Stop eating
            source.Cancel();

            // Wait till all finished
            Task.WaitAll(pTasks);

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadLine();
        }
    }
}
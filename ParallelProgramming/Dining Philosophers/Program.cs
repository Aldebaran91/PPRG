using System;
using System.Threading;
using System.Threading.Tasks;

namespace DiningPhilosophers
{
    public class Philosopher
    {
        public static void Eat(object leftFork, object rightFork, int leftForkNr, int rightForkNr,
            int philosopherNumber, int maxThinkingTime, int maxEatingTime, CancellationToken token)
        {
            Random rm = new Random();

            while (!token.IsCancellationRequested)
            {
                // Philosopher is thinking
                Thread.Sleep(rm.Next() % maxThinkingTime);
                Console.WriteLine($"{philosopherNumber} philosopher finished thinking");

                lock (leftFork)
                {
                    Console.WriteLine($"{philosopherNumber} philosopher took first fork {leftFork}");
                    lock (rightFork)
                    {
                        Console.WriteLine($"{philosopherNumber} philosopher took second fork {rightFork}");
                        Thread.Sleep(rm.Next() % maxEatingTime);
                        Console.WriteLine($"{philosopherNumber} philosopher finished eating");
                    }
                }
            }
            
            Console.WriteLine($"{philosopherNumber} philosopher left task");
        }
    }
    
    class Program
    {
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

            CancellationTokenSource source = new CancellationTokenSource();

            object[] forks = new object[n];
            for (int i = 0; i < n; i++)
            {
                forks[i] = new object();
            }

            Task[] pTasks = new Task[n];
            pTasks[0] = new Task(() => Philosopher.Eat(forks[n - 1], forks[0],
                    n- 1, 0, 1, thinkingTime, eatingTime, source.Token));
            pTasks[0].Start();

            for (int i = 1; i < n; i++)
            {
                pTasks[i] = new Task(() => Philosopher.Eat(forks[i - 1], forks[i % n],
                    i - 1, i, i + 1, thinkingTime, eatingTime, source.Token));
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
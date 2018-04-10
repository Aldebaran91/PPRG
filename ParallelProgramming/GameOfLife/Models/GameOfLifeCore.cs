using GameOfLife.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class GameOfLifeCore
    {
        public BWPicture CurrentGeneration { get; set; }
        private BWPicture NextGeneration { get; set; }

        public GameOfLifeCore(BWPicture InitialField)
        {
            CurrentGeneration = InitialField;
            NextGeneration = new BWPicture(InitialField.Width, InitialField.Height);
        }

        private void FlipStates()
        {
            BWPicture temp = CurrentGeneration;
            CurrentGeneration = NextGeneration;
            NextGeneration = temp;
        }

        public void BeginnGeneration()
        {
            Step();
        }

        public void UpdateGeneration()
        {
            FlipStates();
            Step();
        }

        public string Benchmark()
        {
            StringBuilder BenchmarkReport = new StringBuilder();
            Stopwatch sw = new Stopwatch();

            //run 20 iterations for optimization
            for (int i = 0; i < 20; i++)
                Step(1);

            BenchmarkReport.AppendLine(String.Format("Benchmark GoL: {0}x{1} / {2} Iterations", CurrentGeneration.Width, CurrentGeneration.Height, "1000"));

            //test for cores
            for (int cores = 1; cores<= 10; cores++)
            {
                sw.Reset();
                sw.Start();
                for (int i = 0; i < 200; i++)
                {
                    Step(cores);
                }
                sw.Stop();
                BenchmarkReport.AppendLine(String.Format("{0} threads: {1}", cores, sw.ElapsedMilliseconds));
            }

            return BenchmarkReport.ToString();
            
        }

        public void Step(int MaxThreads = 4)
        {
            ParallelOptions pOptions = new ParallelOptions
            {
                MaxDegreeOfParallelism = MaxThreads
            };

            Parallel.For(0, CurrentGeneration.Height, pOptions, y =>
            {
                for (int x = 0; x < CurrentGeneration.Width; x++)
                {
                    int AliveNeighbors = CurrentGeneration.GetAliveNeighbours(x, y);
                    bool isAlive = CurrentGeneration.IsAlive(x, y);

                    if (isAlive && (AliveNeighbors == 2 || AliveNeighbors == 3))
                        NextGeneration.SetPixel(x, y, true);
                    else if (!isAlive && AliveNeighbors == 3)
                        NextGeneration.SetPixel(x, y, true);
                    else
                        NextGeneration.SetPixel(x, y, false);
                }
            });
        }
    }
}

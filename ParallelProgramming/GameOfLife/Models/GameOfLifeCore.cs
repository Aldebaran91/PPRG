using GameOfLife.Models;
using System;
using System.Collections.Generic;
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

        public void Step()
        {
            ParallelOptions pOptions = new ParallelOptions();
            pOptions.MaxDegreeOfParallelism = 4;

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

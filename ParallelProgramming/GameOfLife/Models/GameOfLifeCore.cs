using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class GameOfLifeCore
    {
        public GameOfLife.Models.BWPicture CurrentState { get; set; }
        private GameOfLife.Models.BWPicture FutureState { get; set; }

        public GameOfLifeCore(GameOfLife.Models.BWPicture InitialField)
        {
            CurrentState = InitialField;
            FutureState = new Models.BWPicture(InitialField.Width, InitialField.Height);
        }

        private void FlipStates()
        {
            GameOfLife.Models.BWPicture temp = CurrentState;
            CurrentState = FutureState;
            FutureState = temp;
        }

        public void Step()
        {
            ParallelOptions pOptions = new ParallelOptions();

            //Parallel.For(0, CurrentState.Width - 1, pOptions, delegate(int x)
            //{
            //    for (int y = 0; y < CurrentState.Height; y++)
            //    {
            //        int AliveNeighbors = CurrentState.getAliveNeighbours(x,y);

            //        if (CurrentState.isAlive(x, y))
            //        {
            //            if (AliveNeighbors < 2)
            //                FutureState.SetPixel(x, y, false);
            //            else if (AliveNeighbors > 3)
            //                FutureState.SetPixel(x, y, false);
            //            else
            //                FutureState.SetPixel(x, y, true);
            //        }
            //        else
            //        {
            //            if (AliveNeighbors == 3)
            //                FutureState.SetPixel(x, y, true);
            //        }
            //    }
            //});

            for (int x = 0; x < CurrentState.Width; x++)
            {
                for (int y = 0; y < CurrentState.Height; y++)
                {
                    int AliveNeighbors = CurrentState.getAliveNeighbours(x, y);

                    if (CurrentState.isAlive(x, y))
                    {
                        if (AliveNeighbors < 2)
                            FutureState.SetPixel(x, y, false);
                        else if (AliveNeighbors > 3)
                            FutureState.SetPixel(x, y, false);
                        else
                            FutureState.SetPixel(x, y, true);
                    }
                    else
                    {
                        if (AliveNeighbors == 3)
                            FutureState.SetPixel(x, y, true);
                    }
                }
            }
                this.FlipStates();

        }
    }
}

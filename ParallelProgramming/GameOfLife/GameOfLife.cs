using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class GameOfLifeCore
    {
        public bool[,] CurrentState { get; set; }

        public GameOfLifeCore(int SizeX, int SizeY, int Iterations)
        {

        }

        public GameOfLifeCore(bool[,] InitialField, int Iterations)
        {

        }
    }
}

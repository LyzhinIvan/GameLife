using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GameLife
{
    public class LifeBoard
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public bool[,] Board;

        public LifeBoard(int width=10, int height=10)
        {
            Width = width;
            Height = height;
            Board = new bool[width,height];
        }

        public void GoToNext()
        {
            bool[,] newBoard = new bool[Width,Height];
            for(int i=0; i<Width; ++i)
                for (int j = 0; j < Height; ++j)
                {
                    int nearCount = NearCount(new Cell(i, j));
                    if (Board[i, j])
                    {
                        if (nearCount == 2 || nearCount == 3)
                            newBoard[i, j] = true;
                    }
                    else if (nearCount == 3)
                        newBoard[i, j] = true;
                }
            Board = newBoard;
        }

        private int NearCount(Cell cell)
        {
            int count = 0;
            for(int i=-1; i<=1; ++i)
                for (int j = -1; j <= 1; ++j)
                {
                    if (i == 0 && j == 0) continue;
                    if (CheckBounds(new Cell(cell.X+i, cell.Y+j)) && Board[cell.X + i, cell.Y + j])
                        count++;
                }
            return count;
        }

        private bool CheckBounds(Cell cell)
        {
            return cell.X >= 0 && cell.X < Width && cell.Y >= 0 && cell.Y < Height;
        }

        public class Cell
        {
            public int X { get; private set; }
            public int Y { get; private set; }

            public Cell(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
    }
}

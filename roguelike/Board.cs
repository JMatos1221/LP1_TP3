using System;

namespace roguelike
{
    public class Board
    {
        int x, y;
        Space[,] coordinates;

        public Board(int row, int col)
        {
            this.x = row;
            this.y = col;

            coordinates = new Space[x,  y];

            for (int i = 0; i < x; i ++)
            {
                for (int j = 0; j < y; j++)
                {
                    this.coordinates[i, j] = new Space(State.Empty);
                }
            }

            int rRow = new Random().Next(0, x);

            this.coordinates[rRow, 0] = new Space(State.Player);

            rRow = new Random().Next(0, x);

            this.coordinates[rRow, y - 1] = new Space(State.Exit);
        }

        public void Print()
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (coordinates[i, j].state == State.Empty) Console.Write("0 ");

                    else if (coordinates[i, j].state == State.Player) Console.Write("1 ");

                    else if (coordinates[i, j].state == State.Enemy) Console.Write("2 ");

                    else if (coordinates[i, j].state == State.Exit) Console.Write("9");
                }
                Console.WriteLine();
            }
        }
    }
}
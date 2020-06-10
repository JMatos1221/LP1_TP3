using System;

namespace roguelike
{
    public class Board
    {
        int row, col;
        int[] player = new int[2];
        ushort playerMoves = 0;
        Space[,] coordinates;

        public Board(int row, int col)
        {
            this.row = row;
            this.col = col;

            coordinates = new Space[row, col];

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    this.coordinates[i, j] = new Space(State.Empty);
                }
            }

            int rNum = new Random().Next(0, row);

            player[0] = rNum;
            player[1] = 0;

            this.coordinates[rNum, 0] = new Space(State.Player);

            rNum = new Random().Next(0, row);

            this.coordinates[rNum, col - 1] = new Space(State.Exit);

            if (this.row <= this.col) rNum = new Random().Next(0, this.row);
            else rNum = new Random().Next(0, this.col);

            int obstacles = rNum;

            for (int i = 0; i < obstacles; i++)
            {
                int rx = new Random().Next(0, this.row);
                int ry = new Random().Next(0, this.col);

                if (this.coordinates[rx, ry].State == State.Empty) coordinates[rx, ry].State = State.Obstacle;

                else i--;
            }
        }

        public void Print()
        {
            for (int i = 0; i < this.row; i++)
            {
                for (int j = 0; j < this.col; j++)
                {
                    if (coordinates[i, j].State == State.Empty) Console.Write("0 ");

                    else if (coordinates[i, j].State == State.Player) Console.Write("1 ");

                    else if (coordinates[i, j].State == State.Enemy) Console.Write("2 ");

                    else if (coordinates[i, j].State == State.Obstacle) Console.Write("X ");

                    else if (coordinates[i, j].State == State.Exit) Console.Write("9");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void PlayerMove(int x, int y)
        {
            if (this.player[0] + x >= 0 && this.player[0] + x < this.row
                && this.player[1] + y >= 0 && this.player[1] + y < this.col &&
                this.coordinates[this.player[0] + x, this.player[1] + y].State != State.Enemy &&
                this.coordinates[this.player[0] + x, this.player[1] + y].State != State.Obstacle)
            {
                this.coordinates[this.player[0], this.player[1]].State = State.Empty;
                this.coordinates[this.player[0] + x, this.player[1] + y].State = State.Player;

                this.player[0] += x;
                this.player[1] += y;

                this.playerMoves += 1;
            }
            if (playerMoves == 2)
            {
                EnemyMove();
                playerMoves = 0;
            }
        }

        public void EnemyMove()
        {

        }
    }
}
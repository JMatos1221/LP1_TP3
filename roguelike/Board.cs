using System;

namespace roguelike
{
    public class Board
    {
        int row, col, level, minions, bosses, hp;
        int[] player = new int[2];
        ushort playerMoves = 0;
        Space[,] coordinates;

        public Board(int row, int col, int level)
        {
            this.row = row;
            this.col = col;
            this.level = level;
            this.minions = this.row * this.col / 25 + level;
            this.bosses = this.level / 3;
            this.hp = this.row * this.col / 4;

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

            for (int i = 0; i < minions; i++)
            {
                int rx = new Random().Next(0, this.row);
                int ry = new Random().Next(0, this.col);

                if (this.coordinates[rx, ry].State == State.Empty) coordinates[rx, ry].State = State.Minion;

                else i--;
            }

            for (int i = 0; i < bosses; i++)
            {
                int rx = new Random().Next(0, this.row);
                int ry = new Random().Next(0, this.col);

                if (this.coordinates[rx, ry].State == State.Empty) coordinates[rx, ry].State = State.Boss;

                else i--;
            }
        }

        public void Print()
        {
            for (int i = 0; i < this.row; i++)
            {
                for (int j = 0; j < this.col; j++)
                {
                    Console.Write($"{(int)coordinates[i, j].State} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void PlayerMove(int x, int y)
        {
            if (this.player[0] + x >= 0 && this.player[0] + x < this.row
                && this.player[1] + y >= 0 && this.player[1] + y < this.col &&
                this.coordinates[this.player[0] + x, this.player[1] + y].State != State.Minion &&
                this.coordinates[this.player[0] + x, this.player[1] + y].State != State.Obstacle &&
                this.coordinates[this.player[0] + x, this.player[1] + y].State != State.Boss)
            {
                this.coordinates[this.player[0], this.player[1]].State = State.Empty;
                if (this.coordinates[this.player[0] + x, this.player[1] + y].State == State.PowerupS) hp += 4;
                if (this.coordinates[this.player[0] + x, this.player[1] + y].State == State.PowerupM) hp += 8;
                if (this.coordinates[this.player[0] + x, this.player[1] + y].State == State.PowerupL) hp += 16;
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

            if (this.hp > (this.row * this.col / 4)) this.hp = this.row * this.col / 4;
            
            Console.Clear();
        }

        public void EnemyMove()
        {

        }
    }
}
using System;
using System.Collections.Generic;

namespace roguelike
{
    public class Board
    {
        int row, col, minions, bosses, skipPos, powerups, level;
        public static int HP { get; set; }
        int[] player = new int[2];
        int playerMoves = 0;
        Space[,] coordinates;

        public Board(int row, int col, int level)
        {
            this.row = row;
            this.col = col;
            this.level = level;
            this.minions = 3;
            this.bosses = 3;
            this.powerups = 3;
            if (level == 1) HP = row * col / 4;

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

            for (int i = 0; i < obstacles;)
            {
                int rx = new Random().Next(0, this.row);
                int ry = new Random().Next(0, this.col);

                if (this.coordinates[rx, ry].State == State.Empty) 
                {
                    coordinates[rx, ry].State = State.Obstacle;
                    i++;
                }
            }

            for (int i = 0; i < this.minions;)
            {
                int rx = new Random().Next(0, this.row);
                int ry = new Random().Next(0, this.col);

                if (this.coordinates[rx, ry].State == State.Empty) 
                {
                    coordinates[rx, ry].State = State.Minion;
                    i++;
                }
            }

            for (int i = 0; i < this.bosses; i++)
            {
                int rx = new Random().Next(0, this.row);
                int ry = new Random().Next(0, this.col);

                if (this.coordinates[rx, ry].State == State.Empty) 
                {
                    coordinates[rx, ry].State = State.Boss;
                    i++;
                }
            }

            for (int i = 0; i < this.powerups;)
            {
                int chosenOne = new Random().Next(0, 10);

                if (chosenOne > 7) chosenOne = 6;

                else if (chosenOne > 4) chosenOne = 5;

                else chosenOne = 4;

                int rx = new Random().Next(0, this.row);
                int ry = new Random().Next(0, this.col);

                if (this.coordinates[rx, ry].State == State.Empty) 
                {
                    coordinates[rx, ry].State = (State)chosenOne;
                    i++;
                }
            }
        }

        public void Print()
        {
            Console.WriteLine($"HP: {HP}\nLevel: {this.level}\nMinions: {this.minions}\nBosses: {this.bosses}\nPowerups: {this.powerups}\n");
            for (int i = 0; i < this.row; i++)
            {
                for (int j = 0; j < this.col; j++)
                {
                    if (coordinates[i, j].State.HasFlag(State.Minion)) Console.Write($"{(int)State.Minion} ");

                    else if (coordinates[i, j].State.HasFlag(State.Boss)) Console.Write($"{(int)State.Boss} ");

                    else Console.Write($"{(int)coordinates[i, j].State} ");
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
                if (this.coordinates[this.player[0] + x, this.player[1] + y].State == State.PowerupS) HP += 4;
                if (this.coordinates[this.player[0] + x, this.player[1] + y].State == State.PowerupM) HP += 8;
                if (this.coordinates[this.player[0] + x, this.player[1] + y].State == State.PowerupL) HP += 16;
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

            if (HP > (this.row * this.col / 4)) HP = this.row * this.col / 4;

            Console.Clear();
        }

        public void EnemyMove()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (this.coordinates[i, j].State.HasFlag(State.Minion) || this.coordinates[i, j].State.HasFlag(State.Boss))
                    {
                        State enemy;

                        if (this.coordinates[i, j].State.HasFlag(State.Minion)) enemy = State.Minion;
                        else enemy = State.Boss;

                        if (j == skipPos)
                        {
                            skipPos = -1;
                            continue;
                        }

                        int xDiff = player[0] - i;
                        int yDiff = player[1] - j;

                        if (Math.Abs(xDiff) > Math.Abs(yDiff))
                        {
                            xDiff /= Math.Abs(xDiff);
                            yDiff = 0;
                        }

                        else
                        {
                            yDiff /= Math.Abs(yDiff);
                            xDiff = 0;
                        }

                        if (this.coordinates[i + xDiff, j + yDiff].State == State.Empty)
                        {
                            this.coordinates[i + xDiff, j + yDiff].State = enemy;
                            this.coordinates[i, j].State &= ~enemy;

                            if (yDiff == 1) j++;

                            if (xDiff == 1) skipPos = j;
                        }

                        else if (this.coordinates[i + xDiff, j + yDiff].State == State.PowerupS ||
                                this.coordinates[i + xDiff, j + yDiff].State == State.PowerupM ||
                                this.coordinates[i + xDiff, j + yDiff].State == State.PowerupL)
                        {
                            this.coordinates[i + xDiff, j].State |= enemy;
                            this.coordinates[i, j].State &= ~enemy;

                            if (yDiff == 1) j++;

                            if (xDiff == 1) skipPos = j;
                        }

                        else
                        {
                            List<int> possibleMoves = new List<int>();

                            if (i < row - 1)
                                if (coordinates[i + 1, j].State == State.Empty ||
                                    coordinates[i + 1, j].State == State.PowerupS ||
                                    coordinates[i + 1, j].State == State.PowerupM ||
                                    coordinates[i + 1, j].State == State.PowerupL)
                                {
                                    possibleMoves.Add(i + 1);
                                    possibleMoves.Add(j);
                                }

                            if (i > 0)
                                if (coordinates[i - 1, j].State == State.Empty ||
                                    coordinates[i - 1, j].State == State.PowerupS ||
                                    coordinates[i - 1, j].State == State.PowerupM ||
                                    coordinates[i - 1, j].State == State.PowerupL)
                                {
                                    possibleMoves.Add(i - 1);
                                    possibleMoves.Add(j);
                                }

                            if (j < col - 1)
                                if (coordinates[i, j + 1].State == State.Empty ||
                                    coordinates[i, j + 1].State == State.PowerupS ||
                                    coordinates[i, j + 1].State == State.PowerupM ||
                                    coordinates[i, j + 1].State == State.PowerupL)
                                {
                                    possibleMoves.Add(i);
                                    possibleMoves.Add(j + 1);
                                }

                            if (j > 0)
                                if (coordinates[i, j - 1].State == State.Empty ||
                                coordinates[i, j - 1].State == State.PowerupS ||
                                coordinates[i, j - 1].State == State.PowerupM ||
                                coordinates[i, j - 1].State == State.PowerupL)
                                {
                                    possibleMoves.Add(i);
                                    possibleMoves.Add(j - 1);
                                }

                            if (possibleMoves.Count > 0)
                            {
                                int posPick = new Random().Next(0, (possibleMoves.Count / 2));

                                posPick *= 2;

                                int posX = possibleMoves[posPick], posY = possibleMoves[posPick + 1];

                                if (this.coordinates[posX, posY].State == State.Empty)
                                {
                                    this.coordinates[posX, posY].State = enemy;
                                    this.coordinates[i, j].State &= ~enemy;
                                }

                                else
                                {
                                    this.coordinates[posX, posY].State |= enemy;
                                    this.coordinates[i, j].State &= ~enemy;
                                }

                                if (posX - i == 1) skipPos = j;

                                else if (posY - j == 1) j++;
                            }

                            else continue;
                        }
                    }
                }
            }

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (this.coordinates[i, j].State == State.Minion ||
                        this.coordinates[i, j].State == State.Boss)
                    {
                        if (i < row - 1)
                            if (coordinates[i + 1, j].State == State.Player) DamagePlayer(i, j);

                        if (i > 0)
                            if (coordinates[i - 1, j].State == State.Player) DamagePlayer(i, j);

                        if (j < col - 1)
                            if (coordinates[i, j + 1].State == State.Player) DamagePlayer(i, j);

                        if (j > 0)
                            if (coordinates[i, j - 1].State == State.Player) DamagePlayer(i, j);
                    }
                }
            }
        }

        public void DamagePlayer(int row, int col)
        {
            if (coordinates[row, col].State == State.Minion) HP -= 5;

            else if (coordinates[row, col].State == State.Boss) HP -= 10;
        }

        public bool GameCheck(Board board)
        {
            if (Board.HP <= 0)
            {
                board.Print();
                Console.WriteLine("You lost the game");
                return false;
            }

            else
            {
                for (int i = 0; i < row; i++)
                {
                    for (int j = 0; j < col; j++)
                    {
                        if (coordinates[i, j].State == State.Exit) return true;
                    }
                }
            }
            return false;
        }
    }
}
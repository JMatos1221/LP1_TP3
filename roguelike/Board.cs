using System;
using System.Collections.Generic;

namespace roguelike
{
    /// <summary>
    /// Board Class containing most of the Game Info
    /// </summary>
    public class Board
    {
        int row, col, minions, bosses, skipPos, powerups, counter = 0, level;
        public static int HP { get; set; }
        public static int Score { get; set; }
        public static string Name { get; set; }
        int[] player = new int[2];
        int playerMoves = 0;
        Space[,] coordinates;
        List<string> actions = new List<string>();
        Random rnd = new Random();

        /// <summary>
        /// Board constructor
        /// </summary>
        /// <param name="row">Number of Rows</param>
        /// <param name="col">Number of Columns</param>
        /// <param name="level">Level (Default is 1)</param>
        public Board(int row, int col, int level)
        {
            this.row = row;
            this.col = col;
            this.level = level;
            this.minions = row * col / 36 + level;
            if (this.minions > row * col / 2) this.minions = row * col / 2;
            this.bosses = level / 3;
            this.powerups = row * col / 25 - level / 3;
            if (this.powerups < 1) this.powerups = 1;
            if (level == 1) HP = row * col / 4;

            coordinates = new Space[row, col];

            // Created Empty Board
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    this.coordinates[i, j] = new Space(State.Empty);
                }
            }

            int rNum = rnd.Next(0, row);

            player[0] = rNum;
            player[1] = 0;

            // Places Player
            this.coordinates[rNum, 0] = new Space(State.Player);

            rNum = rnd.Next(0, row);

            // Places Exit
            this.coordinates[rNum, col - 1] = new Space(State.Exit);

            if (this.row <= this.col) rNum = rnd.Next(0, this.row);
            else rNum = rnd.Next(0, this.col);

            int obstacles = rNum;

            // Places Obstacles
            while (counter < obstacles)
            {
                int rx = rnd.Next(0, this.row);
                int ry = rnd.Next(0, this.col);

                if (this.coordinates[rx, ry].State == State.Empty)
                {
                    coordinates[rx, ry].State = State.Obstacle;
                    counter++;
                }
            }

            counter = 0;

            // Places Minions according to levels
            while (counter < this.minions)
            {
                int rx = rnd.Next(0, this.row);
                int ry = rnd.Next(0, this.col);

                if (this.coordinates[rx, ry].State == State.Empty)
                {
                    coordinates[rx, ry].State = State.Minion;
                    counter++;
                }
            }

            counter = 0;

            // Places Bosses according to level
            while (counter < this.bosses)
            {
                int rx = rnd.Next(0, this.row);
                int ry = rnd.Next(0, this.col);

                if (this.coordinates[rx, ry].State == State.Empty)
                {
                    coordinates[rx, ry].State = State.Boss;
                    counter++;
                }
            }

            counter = 0;

            // Places Powerups according to level
            while (counter < this.powerups)
            {
                int chosenOne = rnd.Next(0, 10);

                if (chosenOne > 7) chosenOne = 32;

                else if (chosenOne > 4) chosenOne = 16;

                else chosenOne = 8;

                int rx = rnd.Next(0, this.row);
                int ry = rnd.Next(0, this.col);

                if (this.coordinates[rx, ry].State == State.Empty)
                {
                    coordinates[rx, ry].State = (State)chosenOne;
                    counter++;
                }
            }
        }

        /// <summary>
        /// Prints the Game Board using Flagged Enum with Binary values
        /// </summary>
        public void Print()
        {
            Console.WriteLine($"Level: {this.level}    HP: {HP}\n\nMinions: {this.minions}\n" +
                                $"Bosses: {this.bosses}\nPowerups: {this.powerups}\n");
            for (int i = 0; i < this.row; i++)
            {
                for (int j = 0; j < this.col; j++)
                {
                    if (coordinates[i, j].State.HasFlag(State.Player)) Console.Write("1 ");

                    else if (coordinates[i, j].State.HasFlag(State.Minion)) Console.Write("2 ");

                    else if (coordinates[i, j].State.HasFlag(State.Boss)) Console.Write("3 ");

                    else if (coordinates[i, j].State.HasFlag(State.PowerupS)) Console.Write("4 ");

                    else if (coordinates[i, j].State.HasFlag(State.PowerupM)) Console.Write("5 ");

                    else if (coordinates[i, j].State.HasFlag(State.PowerupL)) Console.Write("6 ");

                    else if (coordinates[i, j].State.HasFlag(State.Obstacle)) Console.Write("8 ");

                    else if (coordinates[i, j].State.HasFlag(State.Exit)) Console.Write("9 ");

                    else if (coordinates[i, j].State.HasFlag(State.Empty)) Console.Write("0 ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("1. Player\n2. Minion\n3. Boss\n4. Small Powerup\n5. Medium Powerup\n" +
                                "6. Large Powerup\n8. Obstacle\n9. Exit\n");

            foreach (string i in actions)
            {
                Console.WriteLine(i);
            }

            actions.Clear();

            Console.Write("\nUse [W][A][S][D] | [Up][Down][Left][Right] to move | [Escape] to exit");
        }

        /// <summary>
        /// Moves the Player if the new Space is Empty or a Powerup
        /// </summary>
        /// <param name="x">x distance to move</param>
        /// <param name="y">y distance to move</param>
        public void PlayerMove(int x, int y)
        {
            if (this.player[0] + x >= 0 && this.player[0] + x < this.row
                && this.player[1] + y >= 0 && this.player[1] + y < this.col &&
                this.coordinates[this.player[0] + x, this.player[1] + y].State != State.Minion &&
                this.coordinates[this.player[0] + x, this.player[1] + y].State != State.Boss &&
                this.coordinates[this.player[0] + x, this.player[1] + y].State != State.Obstacle)
            {
                this.coordinates[this.player[0], this.player[1]].State = State.Empty;
                if (this.coordinates[this.player[0] + x, this.player[1] + y].State == State.PowerupS)
                {
                    HP += 4;
                    actions.Add("Player healed for 4.");
                }
                if (this.coordinates[this.player[0] + x, this.player[1] + y].State == State.PowerupM)
                {
                    HP += 8;
                    actions.Add("Player healed for 8.");
                }
                if (this.coordinates[this.player[0] + x, this.player[1] + y].State == State.PowerupL)
                {
                    HP += 16;
                    actions.Add("Player healed for 16.");
                }
                this.coordinates[this.player[0] + x, this.player[1] + y].State = State.Player;



                this.player[0] += x;
                this.player[1] += y;

                this.playerMoves += 1;

                Board.HP -= 1;
            }
            if (playerMoves == 2)
            {
                EnemyMove();
                playerMoves = 0;
            }

            if (HP > (this.row * this.col / 4)) HP = this.row * this.col / 4;

            Console.Clear();
        }

        /// <summary>
        /// Moves the Enemy to shorten the it's distance to the Player, if the desired position is blocked
        /// moves to a random possible position
        /// </summary>
        public void EnemyMove()
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    if (this.coordinates[i, j].State.HasFlag(State.Minion) ||
                        this.coordinates[i, j].State.HasFlag(State.Boss))
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

                        // If near the player already, doesn't move, damages
                        if (this.coordinates[i + xDiff, j + yDiff].State == State.Player)
                            DamagePlayer(i, j);

                        // Moves if not near the Player to shorten it's distance to him
                        else if (this.coordinates[i + xDiff, j + yDiff].State == State.Empty)
                        {
                            this.coordinates[i + xDiff, j + yDiff].State = enemy;
                            this.coordinates[i, j].State &= ~enemy;

                            actions.Add($"Enemy moved from [{i + 1},{j + 1}] to [{i + 1 + xDiff},{j + 1 + yDiff}]");

                            if (yDiff == 1) j++;

                            if (xDiff == 1) skipPos = j;

                            EnemyTryAttack(i, j);
                        }

                        // Moves if not near to the Player as the above, but onto a Powerup
                        else if (this.coordinates[i + xDiff, j + yDiff].State == State.PowerupS ||
                                this.coordinates[i + xDiff, j + yDiff].State == State.PowerupM ||
                                this.coordinates[i + xDiff, j + yDiff].State == State.PowerupL)
                        {
                            this.coordinates[i + xDiff, j + yDiff].State |= enemy;
                            this.coordinates[i, j].State &= ~enemy;

                            actions.Add($"Enemy moved from [{i + 1},{j + 1}] to [{i + 1 + xDiff},{j + 1 + yDiff}] " +
                                        "onto a Powerup");

                            if (yDiff == 1) j++;

                            if (xDiff == 1) skipPos = j;

                            EnemyTryAttack(i, j);
                        }

                        // Moves to random Position
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
                                int posPick = rnd.Next(0, (possibleMoves.Count / 2));

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
        }

        /// <summary>
        /// Checks if the Enemy can attack, attacks if so
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public void EnemyTryAttack(int i, int j)
        {

            if (i < row - 1)
            {
                if (coordinates[i + 1, j].State == State.Player) DamagePlayer(i, j);
            }

            else if (i > 0)
            {
                if (coordinates[i - 1, j].State == State.Player) DamagePlayer(i, j);
            }

            else if (j < col - 1)
            {
                if (coordinates[i, j + 1].State == State.Player) DamagePlayer(i, j);
            }

            else if (j > 0)
            {
                if (coordinates[i, j - 1].State == State.Player) DamagePlayer(i, j);
            }

        }

        /// <summary>
        /// Damages the Player, depending on the enemy
        /// </summary>
        /// <param name="row">Enemy row</param>
        /// <param name="col">Enemy column</param>
        public void DamagePlayer(int row, int col)
        {
            if (coordinates[row, col].State == State.Minion)
            {
                HP -= 5;
                actions.Add($"Enemy minion damage player for 5");
            }

            else if (coordinates[row, col].State == State.Boss)
            {
                HP -= 10;
                actions.Add($"Enemy boss damage player for 10");
            }
        }

        /// <summary>
        /// Checks if the game has ended or if the level has been bested
        /// </summary>
        /// <param name="board">Game Board</param>
        /// <returns> True if alive, false if dead or level bested</returns>
        public bool GameCheck(Board board)
        {
            if (Board.HP <= 0)
            {
                Score = this.level;

                board.Print();
                Console.WriteLine($"\n\nYou lost the game with a score of {Score}\n");
                Console.Write("Insert your name: ");
                Board.Name = Console.ReadLine();


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
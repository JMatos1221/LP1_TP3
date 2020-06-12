using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace roguelike
{
    class Program
    {
        /// <summary>
        /// Main function, the program starts here
        /// </summary>
        /// <param name="args">Command lines arguments</param>
        static void Main(string[] args)
        {
            // Setting Encoding to UTF8
            Console.OutputEncoding = Encoding.UTF8;
            // Declaring variables
            int row = 0, col = 0, level = 1;
            bool run = true, win = false;
            Board gameBoard;
            ConsoleKeyInfo userIn;
            List<string> highScores = new List<string>();

            // For cycle to read command line arguments
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-r")
                {
                    row = Convert.ToInt32(args[i + 1]);
                    i += 1;
                }

                else if (args[i] == "-c")
                {
                    col = Convert.ToInt32(args[i + 1]);
                    i += 1;
                }
            }

            // If arguments were skipped or invalid, show how to use and quit
            if (row <= 0 || col <= 0)
            {
                Console.WriteLine("Invalid grid size.");
                Console.WriteLine("Please run  the program with the arguments -r [rows] -c [columns]");
                Environment.Exit(0);
            }

            if (File.Exists($@"highscores{row}x{col}.txt")) highScores = File.ReadAllLines($@"highscores{row}x{col}.txt").ToList();

            gameBoard = new Board(row, col, level);

            /// <summary>
            /// While Cycle for the menu options
            /// </summary>
            /// <value>While cycle is true until Quit or New Game is selected</value>
            do
            {
                Console.Clear();
                PrintMenu();

                userIn = Console.ReadKey(true);

                switch (userIn.Key)
                {
                    // Starts a new game
                    case ConsoleKey.D1:
                        Console.Clear();
                        break;

                    // Prints high scores
                    case ConsoleKey.D2:
                        Console.Clear();

                        for (int i = 0; i < highScores.Count; i += 2)
                        {
                            if (highScores[i] == null) break;
                            Console.Write($"{i / 2 + 1}. ");
                            Console.Write(highScores[i] + " - ");
                            Console.WriteLine(highScores[i + 1]);
                        }
                        Console.ReadKey(true);
                        break;

                    // Prints the Game Instructions
                    case ConsoleKey.D3:
                        Console.Clear();
                        PrintInstructions();
                        Console.ReadKey(true);
                        break;

                    // Prints the Game Credits
                    case ConsoleKey.D4:
                        Console.Clear();
                        PrintCredits();
                        Console.ReadKey(true);
                        break;

                    // Closes the Game
                    case ConsoleKey.D5:
                        Environment.Exit(0);
                        break;

                    // Closes the Game
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;

                    default:
                        break;
                }
            } while (userIn.Key != ConsoleKey.D1);

            while (run)
            {
                gameBoard.Print();

                userIn = Console.ReadKey(true);

                switch (userIn.Key)
                {
                    case ConsoleKey.W:
                        gameBoard.PlayerMove(-1, 0);
                        break;

                    case ConsoleKey.A:
                        gameBoard.PlayerMove(0, -1);
                        break;

                    case ConsoleKey.S:
                        gameBoard.PlayerMove(1, 0);
                        break;

                    case ConsoleKey.D:
                        gameBoard.PlayerMove(0, 1);
                        break;

                    case ConsoleKey.UpArrow:
                        gameBoard.PlayerMove(-1, 0);
                        break;

                    case ConsoleKey.LeftArrow:
                        gameBoard.PlayerMove(0, -1);
                        break;

                    case ConsoleKey.DownArrow:
                        gameBoard.PlayerMove(1, 0);
                        break;

                    case ConsoleKey.RightArrow:
                        gameBoard.PlayerMove(0, 1);
                        break;

                    case ConsoleKey.Escape:
                        Board.HP = 0;
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Invalid control. [W][A][S][D] | [Up][Down][Left][Right] | [Escape] to exit\n");
                        break;
                }

                // Checking if the level is still going or not
                run = gameBoard.GameCheck(gameBoard);

                win = (Board.HP > 0 && run == false);

                if (win)
                {
                    level += 1;

                    gameBoard = new Board(row, col, level);

                    run = true;
                }
            }

            string nameAux = Board.Name;
            bool added = false;

            // Places the high score in the list or adds it to the end
            for (int i = 0; i < highScores.Count; i += 2)
            {
                if (Board.Score > Convert.ToInt32(highScores[i + 1]))
                {
                    highScores.Insert(i, Board.Score.ToString());
                    highScores.Insert(i, Board.Name);
                    added = true;
                    break;
                }
            }

            if (highScores.Count < 20 && !added)
            {
                highScores.Add(Board.Name);
                highScores.Add(Board.Score.ToString());
            }

            File.WriteAllLines($@"highscores{row}x{col}.txt", highScores);
        }

        /// <summary>
        /// Prints the Game Menu
        /// /// </summary>
        private static void PrintMenu()
        {
            Console.WriteLine("1. New game");
            Console.WriteLine("2. High scores");
            Console.WriteLine("3. Instructions");
            Console.WriteLine("4. Credits");
            Console.WriteLine("5. Quit\n");
        }

        /// <summary>
        /// Prints the Game Credits
        /// </summary>
        private static void PrintCredits()
        {
            Console.WriteLine("Game developed by:");
            Console.WriteLine("André Figueira");
            Console.WriteLine("João Matos");
            Console.WriteLine("Miguel Feliciano");
        }

        /// <summary>
        /// Prints the Game Instructions
        /// </summary>
        private static void PrintInstructions()
        {
            Console.WriteLine("Welcome to Roguelike!");
            Console.WriteLine("You can move using the arrow keys or WASD");
            Console.WriteLine("You're objective is to pass as many levels as you can");
            Console.WriteLine("You spawn on the left collumn of the board");
            Console.WriteLine("Every turn you need to move two spaces directly up, down, left or right");
            Console.WriteLine("Everytime you move you lose 1 hp");
            Console.WriteLine("The game ends when your hp is equal to 0");
            Console.WriteLine("You can pickup small, medium and large Power-Ups");
            Console.WriteLine("If an enemy is directly up, down, left or right of you, you'll take damage");
            Console.WriteLine("You can't move to spaces that contain obstacles or enemies");
            Console.WriteLine("To finish the level you need to reach the exit");
            Console.WriteLine("You can leave the game by pressing Escape\n");
        }
    }
}
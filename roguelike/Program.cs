using System;
using System.Collections;

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
            // Declaring variables
            ushort row = 0, col = 0;
            bool run = true;
            Board gameBoard;
            ConsoleKeyInfo userIn;
            string[] highScores = new string[10];

            // For cycle to read command line arguments
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-r")
                {
                    row = Convert.ToUInt16(args[i + 1]);
                    i += 1;
                }

                else if (args[i] == "-c")
                {
                    col = Convert.ToUInt16(args[i + 1]);
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

            gameBoard = new Board(row, col);

            PrintInstructions();
            PrintMenu();

            /// <summary>
            /// While Cycle for the menu options
            /// </summary>
            /// <value>While cycle is true until Quit or New Game is selected</value>
            do
            {
                userIn = Console.ReadKey(true);

                switch (userIn.Key)
                {
                    case ConsoleKey.D1:
                        gameBoard = new Board(row, col);
                        break;

                    case ConsoleKey.D2:
                        for (int i = 0; i < highScores.Length; i++)
                        {
                            if (highScores[i] == null) break;
                            Console.WriteLine(highScores[i]);
                        }
                        break;

                    case ConsoleKey.D3:
                        PrintInstructions();
                        break;

                    case ConsoleKey.D4:
                        PrintCredits();
                        break;

                    case ConsoleKey.D5:
                        Environment.Exit(0);
                        break;

                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Insert a valid option.switch [1][2][3][4][5]");
                        break;
                }
            } while (userIn.Key != ConsoleKey.D1);

            gameBoard.Print();

            while (run)
            {
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

                    default:
                        Console.WriteLine("Invalid control. [W][A][S][D] | [Up][Down][Left][Right]\n");
                        break;
                }
                gameBoard.Print();
                GameCheck(gameBoard);
            }
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

        private static void GameCheck(Board board)
        {

        }
    }
}
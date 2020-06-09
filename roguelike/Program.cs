using System;

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

            // For cycle to read command line aguments
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
                return;
            }

            
        }

        /// <summary>
        /// Prints the Game Menu
        /// /// </summary>
        private void PrintMenu()
        {
            Console.WriteLine("1. New game");
            Console.WriteLine("2. High scores");
            Console.WriteLine("3. Instructions");
            Console.WriteLine("4. Credits");
            Console.WriteLine("5. Quit");
        }

        /// <summary>
        /// Prints the Game Credits
        /// </summary>
        private void PrintCredits()
        {
            Console.WriteLine("Game developed by:");
            Console.WriteLine("André Figueira");
            Console.WriteLine("João Matos");
            Console.WriteLine("Miguel Feliciano");
        }

        /// <summary>
        /// Prints the Game Instructions
        /// </summary>
        private void PrintInstructions()
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
            Console.WriteLine("You can leave the game by pressing Escape");
        }
    }
}
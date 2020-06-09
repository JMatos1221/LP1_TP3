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
            
        }

        /// <summary>
        /// Prints the Game Menu
        /// </summary>
        private void PrintMenu()
        {
            Console.WriteLine("1. New game");
            Console.WriteLine("2. High scores");
            Console.WriteLine("3. Instructions");
            Console.WriteLine("4. Credits");
            Console.WriteLine("5. Quit");
        }
    }
}
using System;

namespace roguelike
{
    public class Board
    {
        Space[,] coordinates;

        Board(int row, int col)
        {
            coordinates = new Space[row, col];
        }
    }
}
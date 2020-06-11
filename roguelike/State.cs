using System;

namespace roguelike
{
    /// <summary>
    /// Enum defining every board square
    /// </summary>
    [Flags]
    public enum State
    {
        Empty = 0,
        Player = 1,
        Minion = 2,
        Boss = 3,
        PowerupS = 4,
        PowerupM = 5,
        PowerupL = 6,
        Obstacle = 8,
        Exit = 9
    }
}
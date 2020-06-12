using System;

namespace roguelike
{
    /// <summary>
    /// Enum defining every board square with Binary Flags to manage Spaces
    /// </summary>
    [Flags]
    public enum State
    {
        Empty = 0,
        Player = 1,
        Minion = 2,
        Boss = 4,
        PowerupS = 8,
        PowerupM = 16,
        PowerupL = 32,
        Obstacle = 64,
        Exit = 128
    }
}
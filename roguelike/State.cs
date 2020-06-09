using System;

namespace roguelike
{
    /// <summary>
    /// Enum defining every board square
    /// </summary>
    public enum State
    {
        Empty = 0,
        Player,
        Enemy,
        Powerup,
        Obstacle,
        Exit
    }
}
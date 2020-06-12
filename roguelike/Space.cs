using System;

namespace roguelike
{
    /// <summary>
    /// Space to create a row x columns Board
    /// </summary>
    public class Space
    {
        public State State { get; set; }

        public Space(State State)
        {
            this.State = State;
        }
    }
}
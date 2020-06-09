using System;

namespace roguelike
{
    public class Space
    {
        public State state { get; set; }

        public Space(State state)
        {
            this.state = state;
        }
    }
}
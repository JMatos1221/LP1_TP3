using System;

namespace roguelike
{
    public class Space
    {
        public State State { get; set; }

        public Space(State State)
        {
            this.State = State;
        }
    }
}
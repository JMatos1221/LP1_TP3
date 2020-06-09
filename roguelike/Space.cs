using System;

namespace roguelike
{
    public class Space
    {
        State state { get; set; }

        Space(State state)
        {
            this.state = state;
        }
    }
}
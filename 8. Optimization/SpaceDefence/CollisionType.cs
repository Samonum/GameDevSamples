
using System;

namespace SpaceDefence
{
    [Flags]
    public enum CollisionType : int
    {
        Team1 = 1,
        Team2 = 2,
        Teams = 3,
        Solid = 4,
    }
}
﻿using System;

namespace Core
{
    [Flags]
    public enum ConnectionDirection
    {
        None = 0,
        Up = 1, 
        Down = 2,
        Right = 4,
        Left = 8,
        All = Up | Down | Right | Left
    }
}
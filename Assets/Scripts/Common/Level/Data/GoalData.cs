﻿using System;
using Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Level.Data
{
    [Serializable]
    public class GoalData
    {
        public Vector2Int pos;
        public TeamColor teamColor;
    }
}
﻿using System;
using Core;
using Core.LevelEditing;
using UnityEngine;

namespace Game.Common.Level.Data
{
    [Serializable]
    public class GoalData : BaseTileData
    {
        public TeamColor teamColor;

        public GoalData(Vector2Int position, TeamColor teamColor)
        {
            this.position = position;
            this.teamColor = teamColor;
        }
    }
}
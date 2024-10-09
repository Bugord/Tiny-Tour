using System;
using Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Level.Data
{
    [Serializable]
    public struct GoalData
    {
        public Vector2Int position;
        public TeamColor teamColor;

        public GoalData(Vector2Int position, TeamColor teamColor)
        {
            this.position = position;
            this.teamColor = teamColor;
        }
    }
}
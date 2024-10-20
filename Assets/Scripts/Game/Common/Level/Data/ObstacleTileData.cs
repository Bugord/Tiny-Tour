using System;
using Core;
using Core.LevelEditing;
using Game.Common.Obstacles;

namespace Game.Common.Level.Data
{
    [Serializable]
    public class ObstacleTileData : BaseTileData
    {
        public TeamColor color;
        public ObstacleType obstacleType;
    }
}
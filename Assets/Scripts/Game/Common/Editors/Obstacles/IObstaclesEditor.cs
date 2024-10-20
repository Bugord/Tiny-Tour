using Core;
using Core.LevelEditing;
using Game.Common.Level.Data;
using Game.Common.Obstacles;
using UnityEngine;

namespace Common.Editors.Obstacles
{
    public interface IObstaclesEditor : ILevelEditor<ObstacleTileData>
    {
        void SetObstacleTile(Vector2Int position, TeamColor teamColor, ObstacleType obstacleType);
    }
}
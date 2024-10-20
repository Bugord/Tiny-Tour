using Cars;
using Core;
using Game.Common.Cars.Core;
using Game.Common.Obstacles;
using Tiles;
using Tiles.Ground;
using UnityEngine.Tilemaps;

namespace Level
{
    public interface ITileLibrary
    {
        public RoadTile GetRoadTile(ConnectionDirection connectionDirection);
        public TerrainTile GetTerrainTileByType(TerrainType terrainType);
        public Tile GetIntermediatePointTile(TeamColor teamColor);
        public Tile GetSpawnPointTile(CarType carType, TeamColor teamColor, Direction direction);
        Tile GetObstacleTile(TeamColor color, ObstacleType obstacleType);
        TileBase GetGoalTile(TeamColor teamColor);
    }
}
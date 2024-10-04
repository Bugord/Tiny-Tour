using Cars;
using Core;
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
        Tile[] GetObstacleTiles();
        Tile GetObstacleTile(int id);
        TileBase GetGoalTile(TeamColor teamColor);
    }
}
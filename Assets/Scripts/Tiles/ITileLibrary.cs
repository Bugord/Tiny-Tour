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
        public UITile GetUIType(UITileType uiTileType);
    }
}
using System;
using Core.LevelEditing;
using Tiles.Ground;

namespace Level
{
    [Serializable]
    public class TerrainTileData : BaseTileData
    {
        public TerrainType terrainType;
    }
}
using System;

namespace Level
{
    [Serializable]
    public class LevelData
    {
        public string levelName;
        public TerrainTileData[] terrainTilesData;
        public RoadTileData[] roadTileData;
        public UITileData[] uiTilesData;
    }
}
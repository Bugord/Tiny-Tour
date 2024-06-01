using System;
using System.Collections.Generic;

namespace Level
{
    [Serializable]
    public class LevelData
    {
        public string levelName;
        public List<TerrainTileData> terrainTileData;
        public List<RoadTileData> roadTileData;
    }
}
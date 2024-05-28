using System;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    [Serializable]
    public class LevelData
    {
        public string levelName;
        public List<TerrainTileData> terrainTileData;
    }
}
using System;

namespace Level.Data
{
    [Serializable]
    public class LevelData
    {
        public string levelName;
        public TerrainTileData[] terrainTilesData;
        public LogisticData logisticData;
        public ObstacleTileData[] obstaclesData;
        public CarSpawnData[] carSpawnData;
    }
}
﻿using System;

namespace Level.Data
{
    [Serializable]
    public class LevelData
    {
        public string levelName;
        public TerrainTileData[] terrainTilesData;
        public RoadTileData[] roadTileData;
        public LogisticData logisticData;
        public ObstacleData[] obstaclesData;
    }
}
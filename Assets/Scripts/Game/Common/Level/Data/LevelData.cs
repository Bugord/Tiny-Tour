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

        public LevelData()
        {
            levelName = "epmty_name";
            obstaclesData = Array.Empty<ObstacleTileData>();
            terrainTilesData = Array.Empty<TerrainTileData>();
            logisticData = new LogisticData {
                roadTileData = Array.Empty<RoadTileData>(),
                goalsData = Array.Empty<GoalData>(),
                intermediatePointsData = Array.Empty<IntermediatePointData>()
            };
        }

        public LevelData(string levelName)
        {
            this.levelName = levelName;
            obstaclesData = Array.Empty<ObstacleTileData>();
            terrainTilesData = Array.Empty<TerrainTileData>();
            logisticData = new LogisticData {
                roadTileData = Array.Empty<RoadTileData>(),
                goalsData = Array.Empty<GoalData>(),
                intermediatePointsData = Array.Empty<IntermediatePointData>()
            };
        }

        public LevelData Copy()
        {
            var copy = new LevelData();
            copy.SetData(this);
            
            return copy;
        }
        
        public void SetData(LevelData levelData)
        {
            levelName = levelData.levelName;
            terrainTilesData = levelData.terrainTilesData;
            logisticData = new LogisticData {
                roadTileData = levelData.logisticData.roadTileData,
                intermediatePointsData = levelData.logisticData.intermediatePointsData,
                goalsData = levelData.logisticData.goalsData,
            };
            obstaclesData = levelData.obstaclesData;
            carSpawnData = levelData.carSpawnData;
        }
    }
}
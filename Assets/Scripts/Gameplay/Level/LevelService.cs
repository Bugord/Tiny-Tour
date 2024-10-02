using Common.Editors;
using Common.Level.Core;
using Gameplay.Cars;
using Gameplay.Logistic;
using Level.Data;

namespace Gameplay
{
    public class LevelService : ILevelService
    {
        private readonly ITerrainLoader terrainLoader;
        private readonly IObstaclesEditor obstaclesEditor;
        private readonly ILogisticService logisticService;
        private readonly ICarsService carsService;

        private LevelData currentLevelData;
        
        public LevelService(ITerrainLoader terrainLoader, IObstaclesEditor obstaclesEditor, ILogisticService logisticService, ICarsService carsService)
        {
            this.terrainLoader = terrainLoader;
            this.obstaclesEditor = obstaclesEditor;
            this.logisticService = logisticService;
            this.carsService = carsService;
        }

        public void LoadLevel(LevelData levelData)
        {
            currentLevelData = levelData;
            
            terrainLoader.LoadTerrain(levelData.terrainTilesData);
            obstaclesEditor.LoadObstacles(levelData.obstaclesData);
            logisticService.LoadLogistic(levelData.logisticData);
            carsService.SpawnCars(levelData.carSpawnData);
        }

        public void ResetLevel()
        {
            logisticService.Reset();
        }
    }
}
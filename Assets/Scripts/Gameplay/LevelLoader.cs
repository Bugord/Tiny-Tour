using Common;
using Common.Editors;
using Gameplay.Cars;
using Level.Data;

namespace Gameplay
{
    public class LevelLoader : ILevelLoader
    {
        private readonly ITerrainLoader terrainLoader;
        private readonly IObstaclesEditor obstaclesEditor;
        private readonly ILogisticLoader logisticLoader;
        private readonly ICarsService carsService;

        public LevelLoader(ITerrainLoader terrainLoader, IObstaclesEditor obstaclesEditor, ILogisticLoader logisticLoader, ICarsService carsService)
        {
            this.terrainLoader = terrainLoader;
            this.obstaclesEditor = obstaclesEditor;
            this.logisticLoader = logisticLoader;
            this.carsService = carsService;
        }

        public void LoadLevel(LevelData levelData)
        {
            terrainLoader.LoadTerrain(levelData.terrainTilesData);
            obstaclesEditor.LoadObstacles(levelData.obstaclesData);
            logisticLoader.LoadLogistic(levelData.logisticData);
            carsService.SpawnCars(levelData.carSpawnData);
        }
    }
}
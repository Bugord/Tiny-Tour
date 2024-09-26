using Common;
using Gameplay.Cars;
using Level.Data;

namespace Gameplay
{
    public class LevelLoader : ILevelLoader
    {
        private readonly ITerrainEditor terrainEditor;
        private readonly IObstaclesEditor obstaclesEditor;
        private readonly ILogisticEditor logisticEditor;
        private readonly ICarsService carsService;

        public LevelLoader(ITerrainEditor terrainEditor, IObstaclesEditor obstaclesEditor, ILogisticEditor logisticEditor, ICarsService carsService)
        {
            this.terrainEditor = terrainEditor;
            this.obstaclesEditor = obstaclesEditor;
            this.logisticEditor = logisticEditor;
            this.carsService = carsService;
        }

        public void LoadLevel(LevelData levelData)
        {
            terrainEditor.LoadTerrain(levelData.terrainTilesData);
            obstaclesEditor.LoadObstacles(levelData.obstaclesData);
            logisticEditor.LoadLogistic(levelData.logisticData);
            carsService.SpawnCars(levelData.carSpawnData);
        }
    }
}
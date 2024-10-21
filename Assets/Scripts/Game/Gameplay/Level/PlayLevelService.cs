using Common.Editors;
using Common.Editors.Obstacles;
using Common.Editors.Terrain;
using Game.Common.Level.Data;
using Gameplay.Cars;
using Gameplay.Logistic;

namespace Game.Gameplay.Level
{
    public class PlayLevelService : ILevelService
    {
        private readonly ITerrainEditor terrainService;
        private readonly IRoadService roadService;
        private readonly IObstaclesEditor obstaclesEditor;
        private readonly ILogisticService logisticService;
        private readonly ICarsService carsService;
        
        public PlayLevelService(ITerrainEditor terrainService, IRoadService roadService, IObstaclesEditor obstaclesEditor, ILogisticService logisticService, ICarsService carsService)
        {
            this.terrainService = terrainService;
            this.roadService = roadService;
            this.obstaclesEditor = obstaclesEditor;
            this.logisticService = logisticService;
            this.carsService = carsService;
        }

        public void LoadLevel(LevelData levelData)
        {
            terrainService.Load(levelData.terrainTilesData);
            roadService.LoadRoad(levelData.logisticData.roadTileData);
            obstaclesEditor.Load(levelData.obstaclesData);
            logisticService.LoadLogistic(levelData.logisticData);
            carsService.SpawnCars(levelData.carSpawnData);
        }

        public void ResetLevel()
        {
            roadService.Reset();
        }
    }
}
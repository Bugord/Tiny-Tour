using Common.Editors.Terrain;
using Game.Common.Editors.Goals;
using Game.Common.Editors.Road;
using Game.Workshop.Editing.Editors;
using Level;
using Level.Data;
using LevelEditor.Level.Core;

namespace Game.Workshop.Level.Core
{
    public class WorkshopService : IWorkshopService
    {
        private readonly ITerrainLevelEditor terrainLevelEditor;
        private readonly IRoadLevelEditor roadLevelEditor;
        private readonly ISpawnPointLevelEditor spawnPointLevelEditor;
        private readonly IGoalLevelEditor goalLevelEditor;
        private readonly LevelManager levelManager;

        private LevelData currentLevelData;

        public WorkshopService(ITerrainLevelEditor terrainLevelEditor, IRoadLevelEditor roadLevelEditor, 
            ISpawnPointLevelEditor spawnPointLevelEditor, IGoalLevelEditor goalLevelEditor, LevelManager levelManager)
        {
            this.terrainLevelEditor = terrainLevelEditor;
            this.roadLevelEditor = roadLevelEditor;
            this.spawnPointLevelEditor = spawnPointLevelEditor;
            this.goalLevelEditor = goalLevelEditor;
            this.levelManager = levelManager;
        }

        public void LoadCurrentLevel()
        {
            var levelData = levelManager.GetSelectedLevel();
            LoadLevel(levelData);
        }

        public void LoadLevel(LevelData levelData)
        {
            currentLevelData = levelData;
            
            terrainLevelEditor.Load(levelData.terrainTilesData);
            roadLevelEditor.Load(levelData.logisticData.roadTileData);
            spawnPointLevelEditor.Load(levelData.carSpawnData);
            goalLevelEditor.Load(levelData.logisticData.goalsData);
        }

        public void SaveLevel()
        {
            currentLevelData.terrainTilesData = terrainLevelEditor.GetTilesData();
            currentLevelData.logisticData.roadTileData = roadLevelEditor.GetTilesData();
            currentLevelData.carSpawnData = spawnPointLevelEditor.GetTilesData();
            currentLevelData.logisticData.goalsData = goalLevelEditor.GetTilesData();
            
            levelManager.SaveLevel(currentLevelData);
            LoadLevel(currentLevelData);
        }

        public void ResetLevel()
        {
            roadLevelEditor.Reset();
            terrainLevelEditor.Reset();
            spawnPointLevelEditor.Reset();
            goalLevelEditor.Reset();
        }
    }
}
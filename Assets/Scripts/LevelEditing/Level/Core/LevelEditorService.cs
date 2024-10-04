using Common.Editors;
using Common.Editors.Logistic;
using Common.Editors.Terrain;
using Level;
using Level.Data;
using LevelEditing.Editing.Editors;
using UnityEngine;

namespace LevelEditor.Level.Core
{
    public class LevelEditorService : ILevelEditorService
    {
        private readonly ITerrainService terrainService;
        private readonly IRoadService roadService;
        private readonly ISpawnPointEditor spawnPointEditor;
        private readonly IGoalEditor goalEditor;
        private readonly LevelManager levelManager;

        private LevelData currentLevelData;

        public LevelEditorService(ITerrainService terrainService, IRoadService roadService, ISpawnPointEditor spawnPointEditor, IGoalEditor goalEditor, LevelManager levelManager)
        {
            this.terrainService = terrainService;
            this.roadService = roadService;
            this.spawnPointEditor = spawnPointEditor;
            this.goalEditor = goalEditor;
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
            
            terrainService.LoadTerrain(levelData.terrainTilesData);
            roadService.LoadRoad(levelData.logisticData.roadTileData);
            spawnPointEditor.Load(levelData.carSpawnData);
            goalEditor.Load(levelData.logisticData.goalsData);
        }

        public void SaveLevel()
        {
            currentLevelData.terrainTilesData = terrainService.SaveTerrain();
            currentLevelData.logisticData.roadTileData = roadService.SaveRoad();
            currentLevelData.carSpawnData = spawnPointEditor.GetCarsSpawnData();
            currentLevelData.logisticData.goalsData = goalEditor.GetGoalPoints();
            
            levelManager.SaveLevel(currentLevelData);
            
            LoadLevel(currentLevelData);
        }

        public void ResetLevel()
        {
            roadService.Reset();
            terrainService.Reset();
            spawnPointEditor.Reset();
            goalEditor.Reset();
        }
    }
}
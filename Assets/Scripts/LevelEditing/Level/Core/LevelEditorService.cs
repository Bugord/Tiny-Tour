using Common.Editors;
using Common.Editors.Terrain;
using Level;
using Level.Data;
using UnityEngine;

namespace LevelEditor.Level.Core
{
    public class LevelEditorService : ILevelEditorService
    {
        private readonly ITerrainService terrainService;
        private readonly IRoadService roadService;
        private readonly LevelManager levelManager;

        private LevelData currentLevelData;

        public LevelEditorService(ITerrainService terrainService, IRoadService roadService, LevelManager levelManager)
        {
            this.terrainService = terrainService;
            this.roadService = roadService;
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
        }

        public void SaveLevel()
        {
            currentLevelData.terrainTilesData = terrainService.SaveTerrain();
            currentLevelData.logisticData.roadTileData = roadService.SaveRoad();
            
            Debug.Log(currentLevelData.logisticData.roadTileData.Length);

            levelManager.SaveLevel(currentLevelData);
            
            LoadLevel(currentLevelData);
        }

        public void ResetLevel()
        {
            roadService.Reset();
            terrainService.Reset();
        }
    }
}
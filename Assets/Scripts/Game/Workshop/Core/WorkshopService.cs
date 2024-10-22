using System;
using Common.Editors.Obstacles;
using Common.Editors.Terrain;
using Game.Common.Editors.Goals;
using Game.Common.Editors.Road;
using Game.Common.Level.Data;
using Game.Main.Session.Core;
using Game.Workshop.Editing.Editors;
using Level;

namespace Game.Workshop.Core
{
    public class WorkshopService : IWorkshopService
    {
        private readonly ITerrainEditor terrainEditor;
        private readonly IRoadEditor roadEditor;
        private readonly IObstaclesEditor obstaclesEditor;
        private readonly ISpawnPointEditor spawnPointEditor;
        private readonly IGoalLevelEditor goalLevelEditor;
        private readonly ISessionManger sessionManger;
        private readonly LevelManager levelManager;

        private LevelData currentLevelData;

        public WorkshopService(ITerrainEditor terrainEditor, IRoadEditor roadEditor, IObstaclesEditor obstaclesEditor,
            ISpawnPointEditor spawnPointEditor, IGoalLevelEditor goalLevelEditor, ISessionManger sessionManger, LevelManager levelManager)
        {
            this.terrainEditor = terrainEditor;
            this.roadEditor = roadEditor;
            this.obstaclesEditor = obstaclesEditor;
            this.spawnPointEditor = spawnPointEditor;
            this.goalLevelEditor = goalLevelEditor;
            this.sessionManger = sessionManger;
            this.levelManager = levelManager;
        }

        public void LoadCurrentLevel()
        {
            var levelData = sessionManger.CurrentSession.LevelData;
            LoadLevel(levelData);
        }

        public void LoadLevel(LevelData levelData)
        {
            currentLevelData = levelData;
            
            terrainEditor.Load(levelData.terrainTilesData);
            roadEditor.Load(levelData.logisticData.roadTileData);
            obstaclesEditor.Load(levelData.obstaclesData);
            spawnPointEditor.Load(levelData.carSpawnData);
            goalLevelEditor.Load(levelData.logisticData.goalsData);
        }

        public LevelData GetLevelData()
        {
            return new LevelData {
                levelName = currentLevelData.levelName,
                terrainTilesData = terrainEditor.GetTilesData(),
                logisticData = new LogisticData {
                    roadTileData = roadEditor.GetTilesData(),
                    goalsData = goalLevelEditor.GetTilesData(),
                    intermediatePointsData = Array.Empty<IntermediatePointData>()
                },
                carSpawnData = spawnPointEditor.GetTilesData(),
                obstaclesData = obstaclesEditor.GetTilesData(),
            };
        }

        public void SaveLevel()
        {
            var levelData = GetLevelData();
            levelManager.SaveLevel(levelData);
            LoadLevel(levelData);
        }

        public void ResetLevel()
        {
            roadEditor.Reset();
            terrainEditor.Reset();
            obstaclesEditor.Reset();
            spawnPointEditor.Reset();
            goalLevelEditor.Reset();
        }
    }
}
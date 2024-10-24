using System;
using Common.Editors.Obstacles;
using Common.Editors.Terrain;
using Game.Common.Editors.Goals;
using Game.Common.Editors.Road;
using Game.Common.Level.Core;
using Game.Common.Level.Data;
using Game.Main.Session.Core;
using Game.Main.UI.Screens;
using Game.Workshop.Editing.Editors;
using Game.Workshop.UI;
using Level;
using Zenject;

namespace Game.Workshop.Core
{
    public class WorkshopEditorService : IWorkshopEditorService, IInitializable, IDisposable
    {
        public event Action<LevelData> TestLeveStarted;
        public event Action LevelEditingEnded;

        private readonly ITerrainEditor terrainEditor;
        private readonly IRoadEditor roadEditor;
        private readonly IObstaclesEditor obstaclesEditor;
        private readonly IWorkshopUIProvider workshopUIProvider;
        private readonly ISpawnPointEditor spawnPointEditor;
        private readonly IGoalLevelEditor goalLevelEditor;
        private readonly LevelManager levelManager;

        private LevelData currentLevelData;

        public WorkshopEditorService(ITerrainEditor terrainEditor, IRoadEditor roadEditor,
            IObstaclesEditor obstaclesEditor, IWorkshopUIProvider workshopUIProvider,
            ISpawnPointEditor spawnPointEditor, IGoalLevelEditor goalLevelEditor, LevelManager levelManager)
        {
            this.terrainEditor = terrainEditor;
            this.roadEditor = roadEditor;
            this.obstaclesEditor = obstaclesEditor;
            this.workshopUIProvider = workshopUIProvider;
            this.spawnPointEditor = spawnPointEditor;
            this.goalLevelEditor = goalLevelEditor;
            this.levelManager = levelManager;
        }

        public void Initialize()
        {
            workshopUIProvider.EditLevelScreen.BackPressed += OnBackPressed;
            workshopUIProvider.EditLevelScreen.PlayPressed += OnPlayPressed;
        }

        public void Dispose()
        {
            workshopUIProvider.EditLevelScreen.BackPressed -= OnBackPressed;
            workshopUIProvider.EditLevelScreen.PlayPressed -= OnPlayPressed;
        }

        public void EditLevel(LevelData levelData)
        {
            currentLevelData = levelData;

            terrainEditor.Load(levelData.terrainTilesData);
            roadEditor.Load(levelData.logisticData.roadTileData);
            obstaclesEditor.Load(levelData.obstaclesData);
            spawnPointEditor.Load(levelData.carSpawnData);
            goalLevelEditor.Load(levelData.logisticData.goalsData);
        }

        public void ClearLevel()
        {
            roadEditor.Clear();
            terrainEditor.Clear();
            obstaclesEditor.Clear();
            spawnPointEditor.Clear();
            goalLevelEditor.Clear();
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
            EditLevel(levelData);
        }

        public void ResetLevel()
        {
            roadEditor.Reset();
            terrainEditor.Reset();
            obstaclesEditor.Reset();
            spawnPointEditor.Reset();
            goalLevelEditor.Reset();
        }

        private void OnPlayPressed()
        {
            TestLeveStarted?.Invoke(GetLevelData());
        }

        private void OnBackPressed()
        {
            LevelEditingEnded?.Invoke();
        }
    }
}
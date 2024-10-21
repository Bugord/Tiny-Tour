using System;
using Common.Editors.Obstacles;
using Common.Editors.Terrain;
using Game.Common.Editors.Goals;
using Game.Common.Editors.Road;
using Game.Common.UI.Editing.EditorOption;
using Game.Gameplay.Editing.Options.Data;
using Game.Gameplay.Editing.Options.Model;
using Game.Workshop.Editing.Core;
using Game.Workshop.Editing.Editors;
using UnityEngine;

namespace Game.Workshop.Editing.Options
{
    public class ErasingWorkshopEditorOption : BaseEditorOption
    {
        private readonly ITerrainEditor terrainEditor;
        private readonly IGoalLevelEditor goalLevelEditor;
        private readonly IObstaclesEditor obstaclesEditor;
        private readonly ISpawnPointEditor spawnPointEditor;
        private readonly IRoadEditor roadEditor;

        private EraseType eraseType;
        private EditorOptionUI editorOptionUI;

        public ErasingWorkshopEditorOption(EditorOptionUI editorOptionUI, ISpawnPointEditor spawnPointEditor,
            EditorOptionDataLibrary editorOptionDataLibrary, IRoadEditor roadEditor,
            ITerrainEditor terrainEditor, IGoalLevelEditor goalLevelEditor, IObstaclesEditor obstaclesEditor)
            : base(editorOptionUI, editorOptionDataLibrary.EraseEditorOptionData)
        {
            this.roadEditor = roadEditor;
            this.terrainEditor = terrainEditor;
            this.goalLevelEditor = goalLevelEditor;
            this.obstaclesEditor = obstaclesEditor;
            this.spawnPointEditor = spawnPointEditor;

            EditorOptionUI.SetBorders(editorOptionDataLibrary.EraseEditorOptionData.ActiveBorderSprite,
                editorOptionDataLibrary.EraseEditorOptionData.InactiveBorderSprite);
        }

        public override void OnTileDown(Vector2Int position)
        {
            SelectEraseType(position);
            EraseTile(position);
        }

        public override void OnAltTileDown(Vector2Int position)
        {
            SelectEraseType(position);
            EraseTile(position);
        }

        public override void OnTileDrag(Vector2Int position)
        {
            EraseTile(position);
        }

        public override void OnAltTileDrag(Vector2Int position)
        {
            EraseTile(position);
        }

        private void SelectEraseType(Vector2Int position)
        {
            eraseType = EraseType.Terrain;

            if (goalLevelEditor.HasTile(position)) {
                eraseType = EraseType.Goal;
                return;
            }

            if (spawnPointEditor.HasTile(position)) {
                eraseType = EraseType.SpawnPoint;
                return;
            }

            if (roadEditor.HasTile(position)) {
                eraseType = EraseType.Road;
                return;
            }  
            
            if (obstaclesEditor.HasTile(position)) {
                eraseType = EraseType.Obstacles;
                return;
            }

            if (terrainEditor.HasTile(position)) {
                eraseType = EraseType.Terrain;
            }
        }

        private void EraseTile(Vector2Int position)
        {
            switch (eraseType) {
                case EraseType.Goal:
                    goalLevelEditor.EraseTile(position);
                    break;       
                case EraseType.Obstacles:
                    obstaclesEditor.EraseTile(position);
                    break;
                case EraseType.SpawnPoint:
                    spawnPointEditor.EraseTile(position);
                    break;
                case EraseType.Road:
                    roadEditor.EraseTile(position);
                    spawnPointEditor.EraseTile(position);
                    break;
                case EraseType.Terrain:
                    goalLevelEditor.EraseTile(position);
                    terrainEditor.EraseTile(position);
                    roadEditor.EraseTile(position);
                    spawnPointEditor.EraseTile(position);
                    obstaclesEditor.EraseTile(position);
                    break;
                case EraseType.None:
                    terrainEditor.EraseTile(position);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private enum EraseType
        {
            None,
            Terrain,
            Road,
            SpawnPoint,
            Obstacles,
            Goal
        }
    }
}
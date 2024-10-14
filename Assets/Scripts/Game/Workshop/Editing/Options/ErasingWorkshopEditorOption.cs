using System;
using System.Linq;
using Common.Editors.Terrain;
using Game.Common.Editors.Goals;
using Game.Common.Editors.Road;
using Game.Common.UI.Editing.EditorOption;
using Game.Gameplay.Editing.Options.Data;
using Game.Gameplay.Editing.Options.Model;
using Game.Workshop.Editing.Core;
using Game.Workshop.Editing.Editors;
using Game.Workshop.LevelEditor.Editors;
using UnityEngine;

namespace LevelEditing.LevelEditor.Options
{
    public class ErasingWorkshopEditorOption : BaseEditorOption
    {
        private readonly ITerrainLevelEditor terrainLevelEditor;
        private readonly IGoalLevelEditor goalLevelEditor;
        private readonly ISpawnPointLevelEditor spawnPointLevelEditor;
        private readonly IRoadLevelEditor roadLevelEditor;

        private EraseType eraseType;
        private EditorOptionUI editorOptionUI;

        private EraseEditorOptionData EraseEditorOptionData => (EraseEditorOptionData)EditorOptionData;

        public ErasingWorkshopEditorOption(EditorOptionDataLibrary editorOptionDataLibrary,
            IRoadLevelEditor roadLevelEditor,
            ITerrainLevelEditor terrainLevelEditor, IGoalLevelEditor goalLevelEditor,
            ISpawnPointLevelEditor spawnPointLevelEditor,
            ILevelEditorController levelEditorController)
        {
            this.roadLevelEditor = roadLevelEditor;
            this.terrainLevelEditor = terrainLevelEditor;
            this.goalLevelEditor = goalLevelEditor;
            this.spawnPointLevelEditor = spawnPointLevelEditor;
            EditorOptionData = editorOptionDataLibrary.EraseEditorOptionData;

            SetupUI(levelEditorController);
        }

        private void SetupUI(ILevelEditorController levelEditorController)
        {
            editorOptionUI = levelEditorController.AddEditorOptionUI(Id);
            editorOptionUI.SetVisuals(EraseEditorOptionData.DefaultIcon,
                EraseEditorOptionData.ActiveBorderSprite,
                EraseEditorOptionData.InactiveBorderSprite);
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
            eraseType = EraseType.None;

            if (goalLevelEditor.HasTile(position)) {
                eraseType = EraseType.Goal;
                return;
            }

            if (spawnPointLevelEditor.HasTile(position)) {
                eraseType = EraseType.SpawnPoint;
                return;
            }

            if (roadLevelEditor.HasTile(position)) {
                eraseType = EraseType.Road;
                return;
            }

            if (terrainLevelEditor.HasTile(position)) {
                eraseType = EraseType.Terrain;
            }
        }

        private void EraseTile(Vector2Int position)
        {
            switch (eraseType) {
                case EraseType.Goal:
                    goalLevelEditor.EraseTile(position);
                    break;
                case EraseType.SpawnPoint:
                    spawnPointLevelEditor.EraseTile(position);
                    break;
                case EraseType.Road:
                    roadLevelEditor.EraseTile(position);
                    spawnPointLevelEditor.EraseTile(position);
                    break;
                case EraseType.Terrain:
                    goalLevelEditor.EraseTile(position);
                    terrainLevelEditor.EraseTile(position);
                    roadLevelEditor.EraseTile(position);
                    spawnPointLevelEditor.EraseTile(position);
                    break;
                case EraseType.None:
                    terrainLevelEditor.EraseTile(position);
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
            Goal
        }
    }
}
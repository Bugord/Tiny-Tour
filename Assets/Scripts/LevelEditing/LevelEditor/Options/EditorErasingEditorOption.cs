using System;
using Common.Editors.Logistic;
using Common.Editors.Road;
using Common.Editors.Terrain;
using Gameplay.Editing.Editors;
using Gameplay.Editing.Options.Data;
using LevelEditing.Editing.Editors;
using UnityEngine;

namespace LevelEditing.LevelEditor.Options
{
    public class EditorErasingEditorOption : BaseEditorOption
    {
        private readonly ITerrainEditor terrainEditor;
        private readonly IGoalEditor goalEditor;
        private readonly ISpawnPointEditor spawnPointEditor;
        private readonly IRoadEditor roadEditor;

        private EraseType eraseType;

        public EditorErasingEditorOption(EditorOptionDataLibrary editorOptionDataLibrary, IRoadEditor roadEditor,
            ITerrainEditor terrainEditor, IGoalEditor goalEditor, ISpawnPointEditor spawnPointEditor)
        {
            this.roadEditor = roadEditor;
            this.terrainEditor = terrainEditor;
            this.goalEditor = goalEditor;
            this.spawnPointEditor = spawnPointEditor;
            EditorOptionData = editorOptionDataLibrary.EraseEditorOptionData;
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

            if (goalEditor.HasTile(position)) {
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

            if (terrainEditor.HasTile(position)) {
                eraseType = EraseType.Terrain;
            }
        }

        private void EraseTile(Vector2Int position)
        {
            switch (eraseType) {
                case EraseType.Goal:
                    goalEditor.EraseTile(position);
                    break;      
                case EraseType.SpawnPoint:
                    spawnPointEditor.EraseTile(position);
                    break;        
                case EraseType.Road:
                    roadEditor.EraseTile(position);
                    spawnPointEditor.EraseTile(position);
                    break;
                case EraseType.Terrain:
                    goalEditor.EraseTile(position);
                    terrainEditor.EraseTile(position);
                    roadEditor.EraseTile(position);
                    spawnPointEditor.EraseTile(position);
                    break;
                case EraseType.None:
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
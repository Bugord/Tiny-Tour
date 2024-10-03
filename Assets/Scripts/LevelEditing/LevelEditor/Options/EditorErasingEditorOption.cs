using System;
using Common.Editors.Road;
using Common.Editors.Terrain;
using Gameplay.Editing.Editors;
using Gameplay.Editing.Options.Data;
using UnityEngine;

namespace LevelEditing.LevelEditor.Options
{
    public class EditorErasingEditorOption : BaseEditorOption
    {
        private readonly ITerrainEditor terrainEditor;
        private readonly IRoadEditor roadEditor;

        private EraseType eraseType;

        public EditorErasingEditorOption(EditorOptionDataLibrary editorOptionDataLibrary, IRoadEditor roadEditor,
            ITerrainEditor terrainEditor)
        {
            this.roadEditor = roadEditor;
            this.terrainEditor = terrainEditor;
            EditorOptionData = editorOptionDataLibrary.EraseEditorOptionData;
        }

        public override void OnTileDown(Vector3Int position)
        {
            SelectEraseType(position);
            EraseTile(position);
        }

        public override void OnAltTileDown(Vector3Int position)
        {
            SelectEraseType(position);
            EraseTile(position);
        }

        public override void OnTileDrag(Vector3Int position)
        {
            EraseTile(position);
        }

        public override void OnAltTileDrag(Vector3Int position)
        {
            EraseTile(position);
        }

        private void SelectEraseType(Vector3Int position)
        {
            eraseType = EraseType.None;

            if (roadEditor.HasRoad(position)) {
                eraseType = EraseType.Road;
                return;
            }

            if (terrainEditor.HasTile(position)) {
                eraseType = EraseType.Terrain;
            }
        }

        private void EraseTile(Vector3Int position)
        {
            switch (eraseType) {
                case EraseType.Road:
                    roadEditor.EraseRoad(position);
                    break;
                case EraseType.Terrain:
                    terrainEditor.EraseTile(position);
                    roadEditor.EraseRoad(position);

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
            Road
        }
    }
}
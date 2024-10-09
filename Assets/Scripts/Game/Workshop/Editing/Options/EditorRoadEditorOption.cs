using Common.Editors.Terrain;
using Game.Common.Editors.Road;
using Gameplay.Editing.Editors;
using Gameplay.Editing.Options.Data;
using UnityEngine;

namespace LevelEditor.LevelEditor.Options
{
    public class EditorRoadEditorOption : BaseEditorOption
    {
        private readonly IRoadEditor roadEditor;
        private readonly ITerrainLevelEditor terrainLevelEditor;

        private Vector2Int? previousRoadPosition;

        public EditorRoadEditorOption(EditorOptionDataLibrary editorOptionDataLibrary, IRoadEditor roadEditor,
            ITerrainLevelEditor terrainLevelEditor)
        {
            this.roadEditor = roadEditor;
            this.terrainLevelEditor = terrainLevelEditor;
            EditorOptionData = editorOptionDataLibrary.RoadEditorOptionData;
        }

        public override void OnTileDown(Vector2Int position)
        {
            if (CanBePlaced(position)) {
                roadEditor.SetRoadTile(position);
                previousRoadPosition = position;
            }
        }

        public override void OnTileDrag(Vector2Int position)
        {
            AddRoadPath(position);
        }

        public override void OnAltTileDown(Vector2Int position)
        {
            if (roadEditor.HasTile(position)) {
                roadEditor.EraseTile(position);
            }
        }

        public override void OnAltTileDrag(Vector2Int position)
        {
            if (roadEditor.HasTile(position)) {
                roadEditor.EraseTile(position);
            }
        }

        public override void OnTileUp(Vector2Int position)
        {
            previousRoadPosition = null;
        }

        private void AddRoadPath(Vector2Int selectedPosition)
        {
            if (!CanBePlaced(selectedPosition)) {
                return;
            }

            if (previousRoadPosition.HasValue &&
                Vector2Int.Distance(selectedPosition, previousRoadPosition.Value) > 1f) {
                return;
            }

            if (!roadEditor.HasTile(selectedPosition)) {
                roadEditor.SetRoadTile(selectedPosition);
            }

            if (previousRoadPosition.HasValue) {
                roadEditor.ConnectRoads(previousRoadPosition.Value, selectedPosition);
            }

            previousRoadPosition = selectedPosition;
        }

        private bool CanBePlaced(Vector2Int position)
        {
            var canBePlaced = terrainLevelEditor.HasSolidTile(position);
            return canBePlaced;
        }
    }
}

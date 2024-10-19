using Common.Editors.Terrain;
using Game.Common.Editors.Road;
using Game.Common.UI.Editing.EditorOption;
using Game.Gameplay.Editing.Options.Data;
using Game.Gameplay.Editing.Options.Model;
using UnityEngine;

namespace Game.Workshop.Editing.Options
{
    public class EditorRoadEditorOption : BaseEditorOption
    {
        private readonly IRoadLevelEditor roadLevelEditor;
        private readonly ITerrainLevelEditor terrainLevelEditor;

        private Vector2Int? previousRoadPosition;

        public EditorRoadEditorOption(EditorOptionUI editorOptionUI, EditorOptionDataLibrary editorOptionDataLibrary,
            IRoadLevelEditor roadLevelEditor, ITerrainLevelEditor terrainLevelEditor)
            : base(editorOptionUI, editorOptionDataLibrary.RoadEditorOptionData)
        {
            this.roadLevelEditor = roadLevelEditor;
            this.terrainLevelEditor = terrainLevelEditor;
        }

        public override void OnTileDown(Vector2Int position)
        {
            if (CanBePlaced(position)) {
                roadLevelEditor.SetRoadTile(position);
                previousRoadPosition = position;
            }
        }

        public override void OnTileDrag(Vector2Int position)
        {
            AddRoadPath(position);
        }

        public override void OnAltTileDown(Vector2Int position)
        {
            if (roadLevelEditor.HasTile(position)) {
                roadLevelEditor.EraseTile(position);
            }
        }

        public override void OnAltTileDrag(Vector2Int position)
        {
            if (roadLevelEditor.HasTile(position)) {
                roadLevelEditor.EraseTile(position);
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

            if (!roadLevelEditor.HasTile(selectedPosition)) {
                roadLevelEditor.SetRoadTile(selectedPosition);
            }

            if (previousRoadPosition.HasValue) {
                roadLevelEditor.ConnectRoads(previousRoadPosition.Value, selectedPosition);
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
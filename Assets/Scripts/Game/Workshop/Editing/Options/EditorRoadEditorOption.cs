using Common.Editors.Obstacles;
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
        private readonly IRoadEditor roadEditor;
        private readonly ITerrainEditor terrainEditor;
        private readonly IObstaclesEditor obstaclesEditor;

        private Vector2Int? previousRoadPosition;

        public EditorRoadEditorOption(EditorOptionUI editorOptionUI, EditorOptionDataLibrary editorOptionDataLibrary,
            IRoadEditor roadEditor, ITerrainEditor terrainEditor, IObstaclesEditor obstaclesEditor)
            : base(editorOptionUI, editorOptionDataLibrary.RoadEditorOptionData)
        {
            this.roadEditor = roadEditor;
            this.terrainEditor = terrainEditor;
            this.obstaclesEditor = obstaclesEditor;
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
            var canBePlaced = terrainEditor.HasSolidTile(position);
            canBePlaced = canBePlaced && !obstaclesEditor.HasTile(position);
            return canBePlaced;
        }
    }
}
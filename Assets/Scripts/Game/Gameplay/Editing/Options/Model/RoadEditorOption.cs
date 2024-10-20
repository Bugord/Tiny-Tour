using Common.Editors.Obstacles;
using Common.Editors.Terrain;
using Core.LevelEditing;
using Game.Common.Editors.Road;
using Game.Common.UI.Editing.EditorOption;
using Game.Gameplay.Editing.Options.Data;
using Game.Workshop.Editing.Core;
using UnityEngine;

namespace Game.Gameplay.Editing.Options.Model
{
    public class RoadEditorOption : BaseEditorOption
    {
        private readonly IRoadLevelEditor roadLevelEditor;
        private readonly ITerrainLevelEditor terrainLevelEditor;
        private readonly IObstaclesEditor obstaclesEditor;

        private Vector2Int? previousRoadPosition;

        public RoadEditorOption(EditorOptionUI editorOptionUI, EditorOptionDataLibrary editorOptionDataLibrary,
            IRoadLevelEditor roadLevelEditor, ITerrainLevelEditor terrainLevelEditor, IObstaclesEditor obstaclesEditor)
            : base(editorOptionUI, editorOptionDataLibrary.RoadEditorOptionData)
        {
            this.roadLevelEditor = roadLevelEditor;
            this.terrainLevelEditor = terrainLevelEditor;
            this.obstaclesEditor = obstaclesEditor;
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
            canBePlaced = canBePlaced && !obstaclesEditor.HasTile(position);
            return canBePlaced;
        }
    }
}
using Common.Editors.Terrain;
using Core;
using Game.Common.Editors.Road;
using Gameplay.Editing.Editors;
using Gameplay.Editing.Options.Data;
using Tiles.Ground;
using UnityEngine;

namespace Gameplay.Editing.Options
{
    public class RoadEditorOption : BaseEditorOption
    {
        private readonly IRoadLevelEditor roadLevelEditor;
        private readonly ITerrainLevelEditor terrainLevelEditor;

        private Vector2Int? previousRoadPosition;

        public RoadEditorOption(EditorOptionDataLibrary editorOptionDataLibrary, IRoadLevelEditor roadLevelEditor, ITerrainLevelEditor terrainLevelEditor)
        {
            this.roadLevelEditor = roadLevelEditor;
            this.terrainLevelEditor = terrainLevelEditor;
            EditorOptionData = editorOptionDataLibrary.RoadEditorOptionData;
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
using Core;
using Gameplay.Editing.Editors;
using Gameplay.Editing.Editors.Terrain;
using Tiles.Ground;
using UnityEngine;

namespace Gameplay.Editing.Options
{
    public class RoadEditorOption : BaseEditorOption
    {
        private readonly IRoadEditor roadEditor;
        private readonly ITerrainEditor terrainEditor;

        private Vector3Int? previousRoadPosition;

        public RoadEditorOption(IRoadEditor roadEditor, ITerrainEditor terrainEditor)
        {
            this.roadEditor = roadEditor;
            this.terrainEditor = terrainEditor;
        }

        public override void OnTileDown(Vector3Int position)
        {
            if (CanBePlaced(position)) {
                roadEditor.SetRoadTile(position);
                previousRoadPosition = position;
            }
        }

        public override void OnTileDrag(Vector3Int position)
        {
            AddRoadPath(position);
        }

        public override void OnTileUp(Vector3Int position)
        {
            previousRoadPosition = null;
        }

        private void AddRoadPath(Vector3Int selectedPosition)
        {
            if (!CanBePlaced(selectedPosition)) {
                return;
            }

            if (previousRoadPosition.HasValue &&
                Vector3Int.Distance(selectedPosition, previousRoadPosition.Value) > 1f) {
                return;
            }

            if (!roadEditor.HasRoad(selectedPosition)) {
                roadEditor.SetRoadTile(selectedPosition);
            }

            if (previousRoadPosition.HasValue) {
                roadEditor.ConnectRoads(previousRoadPosition.Value, selectedPosition);
            }

            previousRoadPosition = selectedPosition;
        }

        private bool CanBePlaced(Vector3Int position)
        {
            var canBePlaced = terrainEditor.HasTileOfType(position, TerrainType.Ground);
            return canBePlaced;
        }
    }
}
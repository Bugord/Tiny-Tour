﻿using Common.Editors.Road;
using Common.Editors.Terrain;
using Gameplay.Editing.Editors;
using Gameplay.Editing.Options.Data;
using UnityEngine;

namespace LevelEditor.LevelEditor.Options
{
    public class EditorRoadEditorOption : BaseEditorOption
    {
        private readonly IRoadEditor roadEditor;
        private readonly ITerrainEditor terrainEditor;

        private Vector3Int? previousRoadPosition;

        public EditorRoadEditorOption(EditorOptionDataLibrary editorOptionDataLibrary, IRoadEditor roadEditor,
            ITerrainEditor terrainEditor)
        {
            this.roadEditor = roadEditor;
            this.terrainEditor = terrainEditor;
            EditorOptionData = editorOptionDataLibrary.RoadEditorOptionData;
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

        public override void OnAltTileDown(Vector3Int position)
        {
            if (roadEditor.HasRoad(position)) {
                roadEditor.EraseRoad(position);
            }
        }

        public override void OnAltTileDrag(Vector3Int position)
        {
            if (roadEditor.HasRoad(position)) {
                roadEditor.EraseRoad(position);
            }
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
            var canBePlaced = terrainEditor.HasSolidTile(position);
            return canBePlaced;
        }
    }
}
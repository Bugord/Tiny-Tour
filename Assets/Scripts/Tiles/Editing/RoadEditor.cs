using System.Collections.Generic;
using System.Linq;
using Core;
using Level;
using Tiles.Editing.Options;
using Tiles.Ground;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility;

namespace Tiles.Editing
{
    public class RoadEditor : ITileEditor
    {
        private readonly Tilemap roadTilemap;
        private readonly Tilemap terrainTilemap;
        private readonly ITileLibrary tileLibrary;
        private readonly bool isEditorMode;

        private readonly Dictionary<Vector3Int, RoadTileInfo> roadTiles;

        private Vector3Int? previousSelectedTile;
        private Vector3Int currentSelectedTile;

        public RoadEditor(Tilemap terrainTilemap, Tilemap roadTilemap, ITileLibrary tileLibrary, bool isEditorMode)
        {
            this.terrainTilemap = terrainTilemap;
            this.roadTilemap = roadTilemap;
            this.tileLibrary = tileLibrary;
            this.isEditorMode = isEditorMode;

            roadTiles = new Dictionary<Vector3Int, RoadTileInfo>();
        }

        public void OnTileDown(Vector3Int pos)
        {
            AddRoadPath(pos);
        }

        public void OnTileMove(Vector3Int pos)
        {
            if (pos == previousSelectedTile) {
                return;
            }

            AddRoadPath(pos);
        }

        public void OnTileEraseDown(Vector3Int pos)
        {
            EraseRoad(pos);
        }

        public void OnTileEraseMove(Vector3Int pos)
        {
            if (pos == previousSelectedTile) {
                return;
            }

            EraseRoad(pos);
        }

        public List<BaseEditorOption> GetOptions()
        {
            return new List<BaseEditorOption>() {
                new RoadEditorOption {
                    TileEditor = this,
                    Icon = tileLibrary.GetRoadTile(ConnectionDirection.Right).defaultSprite
                }
            };
        }

        public void SetOption(BaseEditorOption baseEditorOption)
        {
        }

        public void OnTileUp()
        {
            Clear();
        }

        private void Clear()
        {
            previousSelectedTile = null;
        }

        public void Load(RoadTileData[] roadTilesData)
        {
            roadTiles.Clear();
            roadTilemap.ClearAllTiles();

            foreach (var roadTileData in roadTilesData) {
                roadTilemap.SetTile(roadTileData.position, tileLibrary.GetRoadTile(roadTileData.connectionDirection));
                var roadTileInfo = new RoadTileInfo(roadTileData.connectionDirection, true);
                roadTiles.Add(roadTileData.position, roadTileInfo);
            }
        }

        public RoadTileData[] Save()
        {
            return roadTiles.Select(tile => new RoadTileData {
                connectionDirection = tile.Value.ConnectionDirection,
                position = tile.Key
            }).ToArray();
        }

        private void EraseRoad(Vector3Int pos)
        {
            currentSelectedTile = pos;

            if (!CanEraseRoad(pos) && !isEditorMode) {
                return;
            }

            var neighbourTilePositions = GridHelpers.GetNeighborPos(pos);
            foreach (var neighbourTilePos in neighbourTilePositions) {
                if (roadTiles.TryGetValue(neighbourTilePos, out var roadTileInfo)) {
                    roadTileInfo.TurnOffDirection(GridHelpers.GetPathDirection(neighbourTilePos, pos));
                    roadTilemap.SetTile(neighbourTilePos, tileLibrary.GetRoadTile(roadTileInfo.ConnectionDirection));
                }
            }

            roadTiles.Remove(pos);
            roadTilemap.SetTile(pos, null);
        }

        public void AddRoadPath(Vector3Int pos)
        {
            currentSelectedTile = pos;

            if (previousSelectedTile.HasValue &&
                Vector3Int.Distance(currentSelectedTile, previousSelectedTile.Value) > 1f) {
                return;
            }

            if (!CanPlaceRoad(pos)) {
                return;
            }

            if (previousSelectedTile.HasValue) {
                var previousTileDirection =
                    GridHelpers.GetPathDirection(previousSelectedTile.Value, currentSelectedTile);
                var previousTile = roadTiles[previousSelectedTile.Value];
                previousTile.TurnOnDirection(previousTileDirection);
                roadTilemap.SetTile(previousSelectedTile.Value,
                    tileLibrary.GetRoadTile(previousTile.ConnectionDirection));
            }

            previousSelectedTile = previousSelectedTile ?? currentSelectedTile;

            var newTileDirection = GridHelpers.GetPathDirection(currentSelectedTile, previousSelectedTile.Value);
            if (roadTiles.TryGetValue(pos, out var roadTileInfo)) {
                roadTileInfo.TurnOnDirection(newTileDirection);
                roadTilemap.SetTile(currentSelectedTile, tileLibrary.GetRoadTile(roadTileInfo.ConnectionDirection));
            }
            else {
                var newRoadTile = new RoadTileInfo();
                newRoadTile.TurnOnDirection(newTileDirection);

                roadTiles.Add(pos, newRoadTile);
                roadTilemap.SetTile(currentSelectedTile, tileLibrary.GetRoadTile(newRoadTile.ConnectionDirection));
            }

            previousSelectedTile = pos;
        }

        private bool CanEraseRoad(Vector3Int pos)
        {
            if (roadTiles.TryGetValue(pos, out var tileInfo)) {
                return !tileInfo.WasInitiallyPlaced;
            }

            return false;
        }

        private bool CanPlaceRoad(Vector3Int pos)
        {
            var terrainTile = terrainTilemap.GetTile<TerrainTile>(pos);
            return terrainTile && terrainTile.terrainType != TerrainType.Water;
        }
    }
}
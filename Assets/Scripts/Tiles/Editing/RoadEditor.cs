using System.Collections.Generic;
using Level;
using Tiles.Ground;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiles
{
    public class RoadEditor : ITileEditor
    {
        private readonly Tilemap roadTilemap;
        private readonly Tilemap terrainTilemap;
        private readonly ITileLibrary tileLibrary;

        private readonly Dictionary<Vector3Int, RoadTileInfo> roadTiles;

        private Vector3Int? previousSelectedTile;
        private Vector3Int currentSelectedTile;

        private bool isEditorMode = true;

        public RoadEditor(Tilemap terrainTilemap, Tilemap roadTilemap, ITileLibrary tileLibrary)
        {
            this.terrainTilemap = terrainTilemap;
            this.roadTilemap = roadTilemap;
            this.tileLibrary = tileLibrary;

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

        public void OnTileUp()
        {
            Clear();
        }

        private void Clear()
        {
            previousSelectedTile = null;
        }

        public void Reload()
        {
            roadTiles.Clear();
            AddInitiallyPlacedRoad();
        }

        public void EraseRoad(Vector3Int pos)
        {
            currentSelectedTile = pos;

            if (!CanEraseRoad(pos) && !isEditorMode) {
                return;
            }

            var neighbourTilePositions = GetNeibhourTilePos(pos);
            foreach (var neighbourTilePos in neighbourTilePositions) {
                if (roadTiles.TryGetValue(neighbourTilePos, out var roadTileInfo)) {
                    roadTileInfo.TurnOffDirection(GetPathDirection(neighbourTilePos, pos));
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
                var previousTileDirection = GetPathDirection(previousSelectedTile.Value, currentSelectedTile);
                var previousTile = roadTiles[previousSelectedTile.Value];
                previousTile.TurnOnDirection(previousTileDirection);
                roadTilemap.SetTile(previousSelectedTile.Value,
                    tileLibrary.GetRoadTile(previousTile.ConnectionDirection));
            }

            previousSelectedTile = previousSelectedTile ?? currentSelectedTile;

            var newTileDirection = GetPathDirection(currentSelectedTile, previousSelectedTile.Value);
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

        private void AddInitiallyPlacedRoad()
        {
            foreach (var pos in roadTilemap.cellBounds.allPositionsWithin) {
                var tile = roadTilemap.GetTile<RoadTile>(pos);
                if (!tile) {
                    continue;
                }

                var roadTileInfo = new RoadTileInfo(tile.connectionDirection, true);
                roadTiles.Add(pos, roadTileInfo);
            }   
        }

        private ConnectionDirection GetPathDirection(Vector3Int from, Vector3Int to) =>
            from switch {
                { } when from.y < to.y => ConnectionDirection.Up,
                { } when from.y > to.y => ConnectionDirection.Down,
                { } when from.x < to.x => ConnectionDirection.Right,
                { } when from.x > to.x => ConnectionDirection.Left,
                _ => ConnectionDirection.None
            };

        private IEnumerable<Vector3Int> GetNeibhourTilePos(Vector3Int pos)
        {
            yield return pos + Vector3Int.up;
            yield return pos + Vector3Int.down;
            yield return pos + Vector3Int.left;
            yield return pos + Vector3Int.right;
        }
    }
}
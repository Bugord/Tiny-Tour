using System;
using System.Collections.Generic;
using Core;
using Level;
using Tiles.Ground;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility;
using Object = UnityEngine.Object;

namespace Tiles
{
    public class RoadEditor : ITileEditor
    {
        private readonly Tilemap roadTilemap;
        private readonly Tilemap terrainTilemap;

        private readonly Dictionary<Vector3Int, RoadTileInfo> tiles;
        private readonly Dictionary<ConnectionDirection, RoadTile> roadTileObjects;

        private Vector3Int? previousSelectedTile;
        private Vector3Int currentSelectedTile;

        public RoadEditor(Tilemap terrainTilemap, Tilemap roadTilemap, ITileLibrary tileLibrary)
        {
            this.terrainTilemap = terrainTilemap;
            this.roadTilemap = roadTilemap;

            roadTileObjects = new Dictionary<ConnectionDirection, RoadTile>();
            tiles = new Dictionary<Vector3Int, RoadTileInfo>();

            ConfigureRoadObjects(tileLibrary.GetRoadTile());
            SetInitiallyPlacedRoad();
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

        public void Clear()
        {
            previousSelectedTile = null;
        }

        public void EraseRoad(Vector3Int pos)
        {
            currentSelectedTile = pos;

            if (!CanEraseRoad(pos)) {
                return;
            }

            var neighbourTilePositions = GetNeibhourTilePos(pos);
            foreach (var neighbourTilePos in neighbourTilePositions) {
                if (tiles.TryGetValue(neighbourTilePos, out var roadTileInfo)) {
                    roadTileInfo.TurnOffDirection(GetPathDirection(neighbourTilePos, pos));
                    roadTilemap.SetTile(neighbourTilePos, roadTileObjects[roadTileInfo.ConnectionDirection]);
                }
            }

            tiles.Remove(pos);
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
                var previousTile = tiles[previousSelectedTile.Value];
                previousTile.TurnOnDirection(previousTileDirection);
                roadTilemap.SetTile(previousSelectedTile.Value, roadTileObjects[previousTile.ConnectionDirection]);
            }

            previousSelectedTile = previousSelectedTile ?? currentSelectedTile;

            var newTileDirection = GetPathDirection(currentSelectedTile, previousSelectedTile.Value);
            if (tiles.TryGetValue(pos, out var roadTileInfo)) {
                roadTileInfo.TurnOnDirection(newTileDirection);
                roadTilemap.SetTile(currentSelectedTile, roadTileObjects[roadTileInfo.ConnectionDirection]);
            }
            else {
                var newRoadTile = new RoadTileInfo();
                newRoadTile.TurnOnDirection(newTileDirection);

                tiles.Add(pos, newRoadTile);
                roadTilemap.SetTile(currentSelectedTile, roadTileObjects[newRoadTile.ConnectionDirection]);
            }

            previousSelectedTile = pos;
        }

        private bool CanEraseRoad(Vector3Int pos)
        {
            if (tiles.TryGetValue(pos, out var tileInfo)) {
                return !tileInfo.WasInitiallyPlaced;
            }

            return false;
        }

        private bool CanPlaceRoad(Vector3Int pos)
        {
            var terrainTile = terrainTilemap.GetTile<TerrainTile>(pos);
            return terrainTile && terrainTile.terrainType != TerrainType.Water;
        }

        private void SetInitiallyPlacedRoad()
        {
            var roadTilesPos = new List<Vector3Int>();
            foreach (var pos in roadTilemap.cellBounds.allPositionsWithin) {
                var tile = roadTilemap.GetTile(pos);
                if (tile) {
                    roadTilesPos.Add(pos);
                }
            }

            foreach (var roadTilePos in roadTilesPos) {
                var roadTileInfo = new RoadTileInfo {
                    WasInitiallyPlaced = true
                };

                var neighbourTilePositions = GetNeibhourTilePos(roadTilePos);
                foreach (var neighbourTilePos in neighbourTilePositions) {
                    if (roadTilesPos.Contains(neighbourTilePos)) {
                        roadTileInfo.TurnOnDirection(GetPathDirection(roadTilePos, neighbourTilePos));
                    }
                }
                
                tiles.Add(roadTilePos, roadTileInfo);
                roadTilemap.SetTile(roadTilePos, roadTileObjects[roadTileInfo.ConnectionDirection]);
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

        private void ConfigureRoadObjects(RoadTile roadTile)
        {
            var connectionDirections = EnumExtensions.GetAllEnums<ConnectionDirection>();
            foreach (var connectionDirection in connectionDirections) {
                var roadTileObject = Object.Instantiate(roadTile);
                roadTileObject.connectionDirection = connectionDirection;
                roadTileObjects.Add(connectionDirection, roadTileObject);
            }
        }

        private IEnumerable<Vector3Int> GetNeibhourTilePos(Vector3Int pos)
        {
            yield return pos + Vector3Int.up;
            yield return pos + Vector3Int.down;
            yield return pos + Vector3Int.left;
            yield return pos + Vector3Int.right;
        }
    }
}
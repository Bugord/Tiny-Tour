using System.Collections.Generic;
using System.Linq;
using Common.Tilemaps;
using Core;
using Level;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility;

namespace Game.Common.Editors.Road
{
    public abstract class BaseRoadLevelLevelEditor : IRoadLevelEditor
    {
        private readonly ITileLibrary tileLibrary;
        private readonly Tilemap roadTilemap;
        protected readonly Dictionary<Vector2Int, RoadTileData> RoadTilesData;

        protected RoadTileData[] CachedRoadTilesData;

        public Vector2Int RoadMapSize => (Vector2Int)roadTilemap.size;

        protected BaseRoadLevelLevelEditor(ITilemapsProvider tilemapsProvider, ITileLibrary tileLibrary)
        {
            this.tileLibrary = tileLibrary;
            roadTilemap = tilemapsProvider.RoadTilemap;

            RoadTilesData = new Dictionary<Vector2Int, RoadTileData>();
        }

        public abstract void EraseTile(Vector2Int position);

        public void SetRoadTile(Vector2Int position)
        {
            if (HasTile(position)) {
                return;
            }

            var roadTileData = new RoadTileData {
                position = position,
                connectionDirection = ConnectionDirection.None
            };

            SetTile(roadTileData);
        }

        public void SetTile(RoadTileData tileData)
        {
            var newTileData = new RoadTileData {
                position = tileData.position,
                connectionDirection = tileData.connectionDirection
            };
            
            RoadTilesData[newTileData.position] = newTileData;
            SetTilemapTile(newTileData);
        }

        public bool HasTile(Vector2Int position)
        {
            return RoadTilesData.ContainsKey(position);
        }

        public void Load(RoadTileData[] tilesData)
        {
            CachedRoadTilesData = tilesData;
            
            foreach (var cachedRoadTileData in CachedRoadTilesData) {
                SetTile(cachedRoadTileData);
            }
        }

        public void Reset()
        {
            Clear();
            foreach (var cachedRoadTileData in CachedRoadTilesData) {
                SetTile(cachedRoadTileData);
            }
        }

        public RoadTileData[] GetTilesData()
        {
            return RoadTilesData.Values.ToArray();
        }

        public void ConnectRoads(Vector2Int positionFrom, Vector2Int positionTo)
        {
            if (!HasTile(positionFrom) || !HasTile(positionTo)) {
                return;
            }

            var roadFrom = RoadTilesData[positionFrom];
            var roadTo = RoadTilesData[positionTo];

            roadFrom.TurnOnDirection(GridHelpers.GetPathDirection(positionFrom, positionTo));
            roadTo.TurnOnDirection(GridHelpers.GetPathDirection(positionTo, positionFrom));

            SetTilemapTile(roadFrom);
            SetTilemapTile(roadTo);
        }

        public void Clear()
        {
            RoadTilesData.Clear();
            roadTilemap.ClearAllTiles();
        }

        protected void SetTilemapTile(RoadTileData roadTileData)
        {
            var tile = tileLibrary.GetRoadTile(roadTileData.connectionDirection);
            roadTilemap.SetTile((Vector3Int)roadTileData.position, tile);
        }

        protected void EraseTilemapTile(Vector2Int position)
        {
            roadTilemap.SetTile((Vector3Int)position, null);
        }
    }
}
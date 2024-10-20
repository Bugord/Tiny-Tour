using System.Collections.Generic;
using System.Linq;
using Common.Editors.Obstacles;
using Common.Tilemaps;
using Core;
using Core.Logging;
using Game.Common.Level.Data;
using Game.Common.Obstacles;
using Level;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Common.Editors
{
    public class ObstaclesEditor : IObstaclesEditor
    {
        private readonly ILogger<ObstaclesEditor> logger;
        private readonly ITileLibrary tileLibrary;
        private readonly Tilemap obstaclesTilemap;

        private readonly Dictionary<Vector2Int, ObstacleTileData> obstacleTiles;
        private ObstacleTileData[] cachedObstacleTileData;

        public ObstaclesEditor(ILogger<ObstaclesEditor> logger, ITilemapsProvider tilemapsProvider,
            ITileLibrary tileLibrary)
        {
            obstaclesTilemap = tilemapsProvider.ObstacleTilemap;
            this.logger = logger;
            this.tileLibrary = tileLibrary;

            obstacleTiles = new Dictionary<Vector2Int, ObstacleTileData>();
        }

        public void SetObstacleTile(Vector2Int position, TeamColor teamColor, ObstacleType obstacleType)
        {
            var obstacleTileData = new ObstacleTileData {
                position = position,
                color = teamColor,
                obstacleType = obstacleType
            };

            SetTile(obstacleTileData);
        }

        public void SetTile(ObstacleTileData tileData)
        {
            obstacleTiles[tileData.position] = tileData;
            SetTileVisuals(tileData);
        }

        public void EraseTile(Vector2Int position)
        {
            if (!obstacleTiles.Remove(position)) {
                return;
            }

            RemoveTileVisuals(position);
        }

        public bool HasTile(Vector2Int position)
        {
            return obstacleTiles.ContainsKey(position);
        }

        public void Load(ObstacleTileData[] tilesData)
        {
            cachedObstacleTileData = tilesData;

            foreach (var tileData in tilesData) {
                SetObstacleTile(tileData.position, tileData.color, tileData.obstacleType);
            }
        }

        public ObstacleTileData[] GetTilesData()
        {
            return obstacleTiles.Values.ToArray();
        }

        public void Clear()
        {
            obstacleTiles.Clear();
            obstaclesTilemap.ClearAllTiles();
        }

        public void Reset()
        {
            Clear();
            foreach (var tileData in cachedObstacleTileData) {
                SetObstacleTile(tileData.position, tileData.color, tileData.obstacleType);
            }
        }

        private void RemoveTileVisuals(Vector2Int position)
        {
            obstaclesTilemap.SetTile((Vector3Int)position, null);
        }

        private void SetTileVisuals(ObstacleTileData obstacleTileData)
        {
            var tile = tileLibrary.GetObstacleTile(obstacleTileData.color, obstacleTileData.obstacleType);
            obstaclesTilemap.SetTile((Vector3Int)obstacleTileData.position, tile);
        }
    }
}
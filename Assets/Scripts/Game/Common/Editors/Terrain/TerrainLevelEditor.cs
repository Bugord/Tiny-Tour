using System;
using System.Collections.Generic;
using System.Linq;
using Common.Tilemaps;
using Level;
using Tiles.Ground;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Common.Editors.Terrain
{
    public class TerrainLevelEditor : ITerrainLevelEditor
    {
        private readonly Dictionary<Vector2Int, TerrainTileData> terrainTilesData;
        private readonly Tilemap terrainTilemap;
        private readonly ITileLibrary tileLibrary;

        private TerrainTileData[] cachedTerrainTilesData;
        
        public TerrainLevelEditor(ITilemapsProvider tilemapsProvider, ITileLibrary tileLibrary)
        {
            terrainTilemap = tilemapsProvider.TerrainTilemap;
            this.tileLibrary = tileLibrary;
            terrainTilesData = new Dictionary<Vector2Int, TerrainTileData>();
        }

        public void SetTerrainTile(Vector2Int position, TerrainType terrainType)
        {
            var terrainTileData = new TerrainTileData {
                position = position,
                terrainType = terrainType
            };
            
            SetTile(terrainTileData);
        }

        public void SetTile(TerrainTileData tileData)
        {
            terrainTilesData[tileData.position] = tileData;
            SetTilemapTile(tileData);
        }

        public void EraseTile(Vector2Int position)
        {
            terrainTilesData[position] = null;
            EraseTilemapTile(position);
        }

        public bool HasTile(Vector2Int position)
        {
            return terrainTilesData.ContainsKey(position);
        }

        public void Load(TerrainTileData[] tilesData)
        {
            cachedTerrainTilesData = tilesData;
            foreach (var tileData in tilesData) {
                SetTile(tileData);
            }
        }

        public TerrainTileData[] GetTilesData()
        {
            return terrainTilesData.Values.ToArray();
        }

        public void Clear()
        {
            terrainTilesData.Clear();
            terrainTilemap.ClearAllTiles();
        }

        public void Reset()
        {
            Clear();
            
            foreach (var tileData in cachedTerrainTilesData) {
                SetTile(tileData);
            }
        }

        public bool HasSolidTile(Vector2Int position)
        {
            if (terrainTilesData.TryGetValue(position, out var terrainTileData)) {
                return terrainTileData.terrainType != TerrainType.Water;
            }

            return false;
        }

        private void SetTilemapTile(TerrainTileData terrainTileData)
        {
            var tile = tileLibrary.GetTerrainTileByType(terrainTileData.terrainType);
            terrainTilemap.SetTile((Vector3Int)terrainTileData.position, tile);
        }

        private void EraseTilemapTile(Vector2Int position)
        {
            terrainTilemap.SetTile((Vector3Int)position, null);
        }
    }
}
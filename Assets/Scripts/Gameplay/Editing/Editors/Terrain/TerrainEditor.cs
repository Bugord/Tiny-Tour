using System.Collections.Generic;
using System.Linq;
using Common.Tilemaps;
using Level;
using Tiles.Ground;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Gameplay.Editing.Editors.Terrain
{
    public class TerrainEditor : ITerrainEditor
    {
        private readonly ITileLibrary tileLibrary;
        private readonly Tilemap terrainTilemap;

        private readonly List<TerrainTileData> terrainTilesData;

        public TerrainEditor(ITilemapsProvider tilemapsProvider, ITileLibrary tileLibrary)
        {
            this.tileLibrary = tileLibrary;
            terrainTilemap = tilemapsProvider.TerrainTilemap;

            terrainTilesData = new List<TerrainTileData>();
        }

        public void SetTerrainTile(Vector3Int position, TerrainType terrainType)
        {
            var existingTerrainTile = terrainTilesData.FirstOrDefault(data => data.position == position);
            if (existingTerrainTile != null) {
                terrainTilesData.Remove(existingTerrainTile);
            }
            
            terrainTilesData.Add(new TerrainTileData {
                position = position,
                terrainType = terrainType
            });
            
            var tile = tileLibrary.GetTerrainTileByType(terrainType);
            terrainTilemap.SetTile(position, tile);
        }

        public bool HasSolidTile(Vector3Int position)
        {
            return terrainTilesData.Any(data => data.position == position && data.terrainType != TerrainType.Water);
        }

        public void Clear()
        {
            terrainTilesData.Clear();
            terrainTilemap.ClearAllTiles();
        } 
    }
}
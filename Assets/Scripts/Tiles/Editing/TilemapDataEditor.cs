using System;
using System.Collections.Generic;
using Level;
using Tiles.Ground;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Tiles
{
    public class TilemapDataEditor : MonoBehaviour, ILevelLoader, ILevelSaver
    {
        [SerializeField]
        private Tilemap terrainTilemap;

        [SerializeField]
        private TileLibraryData tileLibraryData;
        
        private ITileLibrary tileLibrary;

        private void Awake()
        {
            tileLibrary = tileLibraryData;
        }

        public LevelData SaveLevel()
        {
            var terrainTileData = GetTerrainTilesData();
            var levelData = new LevelData {
                terrainTileData = terrainTileData,
                levelName = "Test"
            };

            return levelData;
        }

        private List<TerrainTileData> GetTerrainTilesData()
        {
            var terrainTilesData = new List<TerrainTileData>();
            foreach (var pos in terrainTilemap.cellBounds.allPositionsWithin) {
                var terrainTile = terrainTilemap.GetTile<TerrainTile>(pos);
                if (!terrainTile) {
                    continue;
                }

                var terrainTileData = new TerrainTileData {
                    position = pos,
                    terrainType = terrainTile.terrainType
                };

                terrainTilesData.Add(terrainTileData);
            }

            return terrainTilesData;
        }

        private void LoadTerrailTilesData(List<TerrainTileData> terrainTilesData)
        {
            foreach (var terrainTileData in terrainTilesData) {
                terrainTilemap.SetTile(terrainTileData.position, tileLibrary.GetTerrainTileByType(terrainTileData.terrainType));
            }
        }

        public void LoadLevel(LevelData levelData)
        {
            LoadTerrailTilesData(levelData.terrainTileData);
        }
    }
}
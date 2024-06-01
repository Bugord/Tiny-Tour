using System;
using System.Collections.Generic;
using Level;
using Tiles.Ground;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace Tiles
{
    public class TilemapSaveLoader : MonoBehaviour, ILevelLoader, ILevelSaver
    {
        [SerializeField]
        private TilemapEditor tilemapEditor;
        
        [SerializeField]
        private Tilemap terrainTilemap;
        
        [SerializeField]
        private Tilemap roadTilemap;
        
        [SerializeField]
        private TileLibraryData tileLibraryData;
        
        private ITileLibrary tileLibrary;

        private LevelData currentLevelData;

        private void Awake()
        {
            tileLibrary = tileLibraryData;
        }

        public LevelData SaveLevel()
        {
            var terrainTilesData = GetTerrainTilesData();
            var roadTilesData = GetRoadTilesData();
            var levelData = new LevelData {
                terrainTileData = terrainTilesData,
                roadTileData = roadTilesData,
                levelName = currentLevelData.levelName
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

        private List<RoadTileData> GetRoadTilesData()
        {
            var roadTilesData = new List<RoadTileData>();
            foreach (var pos in roadTilemap.cellBounds.allPositionsWithin) {
                var roadTile = roadTilemap.GetTile<RoadTile>(pos);
                if (!roadTile) {
                    continue;
                }

                var terrainTileData = new RoadTileData {
                    position = pos,
                    connectionDirection = roadTile.connectionDirection
                };

                roadTilesData.Add(terrainTileData);
            }

            return roadTilesData;
        }

        private void LoadTerrainTilesData(List<TerrainTileData> terrainTilesData)
        {
            terrainTilemap.ClearAllTiles();
            
            foreach (var terrainTileData in terrainTilesData) {
                terrainTilemap.SetTile(terrainTileData.position, tileLibrary.GetTerrainTileByType(terrainTileData.terrainType));
            }
        }
        
        private void LoadRoadTilesData(List<RoadTileData> roadTilesData)
        {
            roadTilemap.ClearAllTiles();
            
            foreach (var roadTileData in roadTilesData) {
                roadTilemap.SetTile(roadTileData.position, tileLibrary.GetRoadTile(roadTileData.connectionDirection));
            }
        }

        public void LoadLevel(LevelData levelData)
        {
            currentLevelData = levelData;
            LoadTerrainTilesData(levelData.terrainTileData);
            LoadRoadTilesData(levelData.roadTileData);
            tilemapEditor.Reload();
        }
    }
}
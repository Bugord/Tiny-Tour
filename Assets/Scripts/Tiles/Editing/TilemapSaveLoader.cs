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
        private Tilemap uiTilemap;

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
            var levelData = new LevelData {
                terrainTileData = GetTerrainTilesData(),
                roadTileData = GetRoadTilesData(),
                spawnPointsData = GetSpawnPointsData(),
                targetsData = GetTargetsData(),
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

        private List<SpawnPointData> GetSpawnPointsData()
        {
            var spawnPointsData = new List<SpawnPointData>();
            foreach (var pos in uiTilemap.cellBounds.allPositionsWithin) {
                var roadTile = uiTilemap.GetTile<UITile>(pos);
                if (!roadTile || roadTile.type != UITileType.SpawnPoint) {
                    continue;
                }

                var spawnPointData = new SpawnPointData {
                    position = pos,
                    team = roadTile.team
                };

                spawnPointsData.Add(spawnPointData);
            }

            return spawnPointsData;
        }
        
        private List<TargetData> GetTargetsData()
        {
            var targetsData = new List<TargetData>();
            foreach (var pos in uiTilemap.cellBounds.allPositionsWithin) {
                var roadTile = uiTilemap.GetTile<UITile>(pos);
                if (!roadTile || roadTile.type != UITileType.Target) {
                    continue;
                }

                var targetData = new TargetData() {
                    position = pos,
                    team = roadTile.team
                };

                targetsData.Add(targetData);
            }

            return targetsData;
        }

        private void LoadTerrainTilesData(List<TerrainTileData> terrainTilesData)
        {
            foreach (var terrainTileData in terrainTilesData) {
                terrainTilemap.SetTile(terrainTileData.position, tileLibrary.GetTerrainTileByType(terrainTileData.terrainType));
            }
        }
        
        private void LoadRoadTilesData(List<RoadTileData> roadTilesData)
        {
            foreach (var roadTileData in roadTilesData) {
                roadTilemap.SetTile(roadTileData.position, tileLibrary.GetRoadTile(roadTileData.connectionDirection));
            }
        }

        private void LoadSpawnPoints(List<SpawnPointData> spawnPointsData)
        {
            foreach (var spawnPointData in spawnPointsData) {
                uiTilemap.SetTile(spawnPointData.position, tileLibrary.GetUIType(UITileType.SpawnPoint));
            }
        }
        
        private void LoadTargets(List<TargetData> targetsData)
        {
            foreach (var targetData in targetsData) {
                uiTilemap.SetTile(targetData.position, tileLibrary.GetUIType(UITileType.Target));
            }
        }

        public void LoadLevel(LevelData levelData)
        {
            currentLevelData = levelData;
            roadTilemap.ClearAllTiles();
            terrainTilemap.ClearAllTiles();
            uiTilemap.ClearAllTiles();
            
            LoadTerrainTilesData(levelData.terrainTileData);
            LoadRoadTilesData(levelData.roadTileData);
            LoadSpawnPoints(levelData.spawnPointsData);
            LoadTargets(levelData.targetsData);
            tilemapEditor.Reload();
        }
    }
}
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Core;
using Level;
using Tiles.Ground;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility;
using LogisticTileData = Tiles.Editing.LogisticTileData;

namespace Tiles
{
    [CreateAssetMenu]
    public class TileLibraryData : ScriptableObject, ITileLibrary
    {
        [SerializeField]
        private TerrainTile groundTile;

        [SerializeField]
        private TerrainTile waterTile;

        [SerializeField]
        private TerrainTile bridgeBaseTile;

        [SerializeField]
        private RoadTile roadTile;

        [SerializeField]
        private LogisticTile spawnPointTile;
        
        [SerializeField]
        private LogisticTile targetTile;
        
        private Dictionary<Team, LogisticTile> spawnPointTiles;

        private Dictionary<Team, LogisticTile> targetTiles;

        private Dictionary<ConnectionDirection, RoadTile> roadTiles;

        public void Init()
        {
            ConfigureRoadObjects(roadTile);
            ConfigureSpawnPointTiles(spawnPointTile);
            ConfigureTargetTiles(targetTile);
        }

        public RoadTile GetRoadTile(ConnectionDirection connectionDirection)
        {
            return roadTiles[connectionDirection];
        }

        public TerrainTile GetTerrainTileByType(TerrainType terrainType) =>
            terrainType switch {
                TerrainType.Ground => groundTile,
                TerrainType.Water => waterTile,
                TerrainType.BridgeBase => bridgeBaseTile,
                _ => null
            };

        public LogisticTile GetLogisticTile(LogisticTileType logisticTileType, Team team) =>
            logisticTileType switch {
                LogisticTileType.Target => targetTiles[team],
                LogisticTileType.SpawnPoint => spawnPointTiles[team],
                _ => null
            };

        private void ConfigureRoadObjects(RoadTile roadTile)
        {
            roadTiles = new Dictionary<ConnectionDirection, RoadTile>();

            var connectionDirections = EnumExtensions.GetAllEnums<ConnectionDirection>();
            foreach (var connectionDirection in connectionDirections) {
                var roadTileObject = Instantiate(roadTile);
                roadTileObject.connectionDirection = connectionDirection;
                roadTiles.Add(connectionDirection, roadTileObject);
            }
        }
        
        private void ConfigureSpawnPointTiles(LogisticTile logisticTile)
        {
            spawnPointTiles = new Dictionary<Team, LogisticTile>();

            var teams = EnumExtensions.GetAllEnums<Team>();
            foreach (var team in teams) {
                var spawnPointTile = Instantiate(logisticTile);
                spawnPointTile.team = team;
                spawnPointTile.type = LogisticTileType.SpawnPoint;
                spawnPointTiles.Add(team, spawnPointTile);
            }
        }
        
        private void ConfigureTargetTiles(LogisticTile logisticTile)
        {
            targetTiles = new Dictionary<Team, LogisticTile>();

            var teams = EnumExtensions.GetAllEnums<Team>();
            foreach (var team in teams) {
                var targetTile = Instantiate(logisticTile);
                targetTile.team = team;
                targetTile.type = LogisticTileType.Target;
                targetTiles.Add(team, targetTile);
            }
        }
    }
}
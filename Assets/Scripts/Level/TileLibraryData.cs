using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Core;
using Level;
using Tiles.Ground;
using Tiles.Logistic;
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
        private TargetTile targetTile;
        
        private Dictionary<Team, LogisticTile> spawnPointTiles;

        private Dictionary<Team, TargetTile> targetTiles;

        private Dictionary<ConnectionDirection, RoadTile> roadTiles;

        public void Init()
        {
            ConfigureRoadObjects(roadTile);
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

        public TargetTile GetTargetTile(Team team)
        {
            return targetTiles[team];
        }

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
        
        private void ConfigureTargetTiles(TargetTile targetTile)
        {
            targetTiles = new Dictionary<Team, TargetTile>();

            var teams = EnumExtensions.GetAllEnums<Team>();
            foreach (var team in teams) {
                var newTargetTile = Instantiate(targetTile);
                newTargetTile.team = team;
                targetTiles.Add(team, newTargetTile);
            }
        }
    }
}
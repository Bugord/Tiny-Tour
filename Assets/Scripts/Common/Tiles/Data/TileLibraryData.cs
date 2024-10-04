using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Cars;
using Core;
using Level;
using Tiles.Ground;
using Tiles.Logistic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility;

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

        [SerializeField]
        private List<Tile> obstacleTiles;

        [SerializeField]
        private SerializedDictionary<TeamColor, Tile> intermediatePointTiles;

        [SerializeField]
        private SerializedDictionary<TeamColor, Tile> carSpawnPointTile;

        private Dictionary<TeamColor, BaseLogisticTile> spawnPointTiles;

        private Dictionary<TeamColor, TargetTile> targetTiles;

        private Dictionary<ConnectionDirection, RoadTile> roadTiles;
        
        public RoadTile GetRoadTile(ConnectionDirection connectionDirection)
        {
            if (roadTiles == null) {
                ConfigureRoadObjects(roadTile);
            }

            return roadTiles[connectionDirection];
        }

        public TerrainTile GetTerrainTileByType(TerrainType terrainType) =>
            terrainType switch {
                TerrainType.Ground => groundTile,
                TerrainType.Water => waterTile,
                TerrainType.BridgeBase => bridgeBaseTile,
                _ => null
            };

        public TargetTile GetTargetTile(TeamColor teamColor)
        {
            if (targetTiles == null) {
                ConfigureTargetTiles(targetTile);
            }
            
            return targetTiles[teamColor];
        }

        public Tile GetIntermediatePointTile(TeamColor teamColor)
        {
            return intermediatePointTiles[teamColor];
        }

        public Tile GetSpawnPointTile(CarType carType, TeamColor teamColor, Direction direction)
        {
            return carSpawnPointTile[teamColor];
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
            targetTiles = new Dictionary<TeamColor, TargetTile>();

            var teams = EnumExtensions.GetAllEnums<TeamColor>();
            foreach (var team in teams) {
                var newTargetTile = Instantiate(targetTile);
                newTargetTile.teamColor = team;
                targetTiles.Add(team, newTargetTile);
            }
        }

        public Tile GetObstacleTile(int id)
        {
            return obstacleTiles[id];
        }

        public Tile[] GetObstacleTiles()
        {
            return obstacleTiles.ToArray();
        }
    }
}
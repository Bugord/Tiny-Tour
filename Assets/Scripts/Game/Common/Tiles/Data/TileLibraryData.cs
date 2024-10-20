using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Cars;
using Core;
using Game.Common.Tiles.Data;
using Level;
using Tiles.Ground;
using Tiles.Logistic;
using UnityEngine;
using UnityEngine.Serialization;
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
        private List<CarSpawnPointData> carSpawnPointTiles;

        [SerializeField]
        private SerializedDictionary<TeamColor, Tile> goalTiles;

        private Dictionary<TeamColor, BaseLogisticTile> spawnPointTiles;

        private Dictionary<ConnectionDirection, RoadTile> roadTiles;

        public void SetCarSpawnPointsData(List<CarSpawnPointData> carSpawnPointsData)
        {
            carSpawnPointTiles = carSpawnPointsData;
        }

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

        public Tile GetIntermediatePointTile(TeamColor teamColor)
        {
            return intermediatePointTiles[teamColor];
        }

        public Tile GetSpawnPointTile(CarType carType, TeamColor color, Direction direction)
        {
            return carSpawnPointTiles.Find(data => data.CarType == carType && data.Color == color && data.Direction == direction).Tile;
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

        public Tile GetObstacleTile(int id)
        {
            return obstacleTiles[id];
        }

        public TileBase GetGoalTile(TeamColor teamColor)
        {
            return goalTiles[teamColor];
        }

        public Tile[] GetObstacleTiles()
        {
            return obstacleTiles.ToArray();
        }
    }
}
using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Core;
using Game.Common.Cars.Core;
using Game.Common.Obstacles;
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
        private SerializedDictionary<ObstacleType, ObstacleTileVisualData> obstacleTiles;

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
            return carSpawnPointTiles
                .Find(data => data.CarType == carType && data.Color == color && data.Direction == direction).Tile;
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

        public Tile GetObstacleTile(TeamColor color, ObstacleType obstacleType)
        {
            var obstacleTileVisualData = obstacleTiles[obstacleType];
            return obstacleTileVisualData.obstacleTiles.ContainsKey(color)
                ? obstacleTileVisualData.obstacleTiles[color]
                : obstacleTileVisualData.defaultTile;
        }

        public TileBase GetGoalTile(TeamColor teamColor)
        {
            return goalTiles[teamColor];
        }

        [Serializable]
        public struct ObstacleTileVisualData
        {
            public Tile defaultTile;
            public SerializedDictionary<TeamColor, Tile> obstacleTiles;
        }
    }
}
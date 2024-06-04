using System.Collections.Generic;
using Core;
using Level;
using Tiles.Ground;
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
        private LogisticTile spawnPointTile;

        [SerializeField]
        private LogisticTile targetTile;

        private Dictionary<ConnectionDirection, RoadTile> roadTiles;

        public void Init()
        {
            ConfigureRoadObjects(roadTile);
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
                LogisticTileType.Target => targetTile,
                LogisticTileType.SpawnPoint => spawnPointTile,
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
    }
}
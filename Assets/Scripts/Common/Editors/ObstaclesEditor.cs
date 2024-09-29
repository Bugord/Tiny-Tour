using Common.Tilemaps;
using Core.Logging;
using Level;
using Level.Data;
using UnityEngine.Tilemaps;

namespace Common.Editors
{
    public class ObstaclesEditor : IObstaclesEditor
    {
        private readonly ILogger<ObstaclesEditor> logger;
        private readonly ITileLibrary tileLibrary;
        private readonly Tilemap obstaclesTilemap;

        public ObstaclesEditor(ILogger<ObstaclesEditor> logger, ITilemapsProvider tilemapsProvider,
            ITileLibrary tileLibrary)
        {
            obstaclesTilemap = tilemapsProvider.ObstacleTilemap;
            this.logger = logger;
            this.tileLibrary = tileLibrary;
        }

        public void LoadObstacles(ObstacleTileData[] obstacleTilesData)
        {
            obstaclesTilemap.ClearAllTiles();

            if (obstacleTilesData == null) {
                logger.LogError("Obstacle tiles are null");
                return;
            }

            foreach (var obstacleTileData in obstacleTilesData) {
                obstaclesTilemap.SetTile(obstacleTileData.position, tileLibrary.GetObstacleTile(obstacleTileData.id));
            }
        }
    }
}
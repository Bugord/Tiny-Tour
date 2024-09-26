using Common.Tilemaps;
using Core.Logging;
using Level;
using Level.Data;
using UnityEngine.Tilemaps;

namespace Common
{
    public class LogisticEditor : ILogisticEditor
    {
        private readonly ILogger<LogisticEditor> logger;
        private readonly ITileLibrary tileLibrary;
        private readonly Tilemap roadTilemap;

        public LogisticEditor(ILogger<LogisticEditor> logger, ITilemapsProvider tilemapsProvider, ITileLibrary tileLibrary)
        {
            roadTilemap = tilemapsProvider.RoadTilemap;
            this.logger = logger;
            this.tileLibrary = tileLibrary;
        }

        public void LoadLogistic(LogisticData logisticData)
        {
            roadTilemap.ClearAllTiles();

            if (logisticData == null) {
                logger.LogError("Logistic data is null");
                return;
            }
            
            if (logisticData.roadTileData == null) {
                logger.LogError("Road tiles are null");
                return;
            }
            
            foreach (var roadTileData in logisticData.roadTileData) {
                roadTilemap.SetTile(roadTileData.position, tileLibrary.GetRoadTile(roadTileData.connectionDirection));
            }
        }
    }
}
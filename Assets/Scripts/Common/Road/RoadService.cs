using Common.Editors;
using Common.Editors.Road;
using Core.Logging;
using Level;

namespace Common.Road
{
    public class RoadService : IRoadService
    {
        private readonly ILogger<RoadService> logger;
        private readonly IRoadEditor roadEditor;

        public RoadService(ILogger<RoadService> logger, IRoadEditor roadEditor)
        {
            this.logger = logger;
            this.roadEditor = roadEditor;
        }
        
        public void LoadRoad(RoadTileData[] roadTilesData)
        {
            roadEditor.Clear();
            
            if (roadTilesData == null) {
                logger.LogError("Road tiles are null");
                return;
            }
            
            foreach (var roadTileData in roadTilesData) {
                roadEditor.SetInitialRoadTile(roadTileData.position, roadTileData.connectionDirection);
            }
        }

        public RoadTileData[] SaveRoad()
        {
            return roadEditor.GetRoadTiles();
        }

        public void Reset()
        {
            roadEditor.Reset();
        }
    }
}
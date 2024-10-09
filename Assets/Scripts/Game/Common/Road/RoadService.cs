using Common.Editors;
using Core.Logging;
using Game.Common.Editors.Road;
using Level;

namespace Common.Road
{
    public class RoadService : IRoadService
    {
        private readonly ILogger<RoadService> logger;
        private readonly IRoadLevelEditor roadLevelEditor;

        public RoadService(ILogger<RoadService> logger, IRoadLevelEditor roadLevelEditor)
        {
            this.logger = logger;
            this.roadLevelEditor = roadLevelEditor;
        }
        
        public void LoadRoad(RoadTileData[] roadTilesData)
        {
            roadLevelEditor.Clear();
            
            if (roadTilesData == null) {
                logger.LogError("Road tiles are null");
                return;
            }
            
            roadLevelEditor.Load(roadTilesData);
        }

        public RoadTileData[] SaveRoad()
        {
            return roadLevelEditor.GetTilesData();
        }

        public void Reset()
        {
            roadLevelEditor.Reset();
        }
    }
}
using Common.Editors;
using Common.Editors.Road;
using Level;

namespace Common.Road
{
    public class RoadService : IRoadService
    {
        private readonly IRoadEditor roadEditor;

        public RoadService(IRoadEditor roadEditor)
        {
            this.roadEditor = roadEditor;
        }
        
        public void LoadRoad(RoadTileData[] roadTilesData)
        {
            foreach (var roadTileData in roadTilesData) {
                roadEditor.SetInitialRoadTile(roadTileData.position, roadTileData.connectionDirection);
            }
        }

        public void Reset()
        {
            roadEditor.Reset();
        }
    }
}
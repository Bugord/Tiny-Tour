using Core.Logging;
using Gameplay.Editing.Editors;
using Level.Data;

namespace Common.Editors
{
    public class LogisticLoader : ILogisticLoader
    {
        private readonly ILogger<LogisticLoader> logger;
        private readonly IRoadEditor roadEditor;

        public LogisticLoader(ILogger<LogisticLoader> logger, IRoadEditor roadEditor)
        {
            this.logger = logger;
            this.roadEditor = roadEditor;
        }

        public void LoadLogistic(LogisticData logisticData)
        {
            roadEditor.Clear();

            if (logisticData == null) {
                logger.LogError("Logistic data is null");
                return;
            }

            if (logisticData.roadTileData == null) {
                logger.LogError("Road tiles are null");
                return;
            }

            foreach (var roadTileData in logisticData.roadTileData) {
                roadEditor.SetInitialRoadTile(roadTileData.position, roadTileData.connectionDirection);
            }
        }
    }
}
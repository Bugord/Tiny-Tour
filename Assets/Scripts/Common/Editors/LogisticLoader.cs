using Core.Logging;
using Gameplay.Editing.Editors;
using Gameplay.Logistic;
using Level.Data;

namespace Common.Editors
{
    public class LogisticLoader : ILogisticLoader
    {
        private readonly ILogger<LogisticLoader> logger;
        private readonly IRoadEditor roadEditor;
        private readonly ILogisticService logisticService;

        public LogisticLoader(ILogger<LogisticLoader> logger, IRoadEditor roadEditor, ILogisticService logisticService)
        {
            this.logger = logger;
            this.roadEditor = roadEditor;
            this.logisticService = logisticService;
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

            foreach (var targetData in logisticData.goalsData) {
                logisticService.AddGoal(targetData.pos, targetData.teamColor);
            }
        }
    }
}
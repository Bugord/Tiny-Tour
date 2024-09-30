using Common.Editors.Logistic;
using Core.Logging;
using Gameplay.Editing.Editors;
using Level.Data;

namespace Common.Editors
{
    public class LogisticLoader : ILogisticLoader
    {
        private readonly ILogger<LogisticLoader> logger;
        private readonly IRoadEditor roadEditor;
        private readonly IGoalEditor goalEditor;

        public LogisticLoader(ILogger<LogisticLoader> logger, IRoadEditor roadEditor, IGoalEditor goalEditor)
        {
            this.logger = logger;
            this.roadEditor = roadEditor;
            this.goalEditor = goalEditor;
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
                goalEditor.SetGoalTile(targetData.pos, targetData.teamColor);
            }
        }
    }
}
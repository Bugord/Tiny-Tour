using System;
using Level;

namespace Game.Common.Level.Data
{
    [Serializable]
    public class LogisticData
    {
        public RoadTileData[] roadTileData;
        public GoalData[] goalsData;
        public IntermediatePointData[] intermediatePointsData;
    }
}
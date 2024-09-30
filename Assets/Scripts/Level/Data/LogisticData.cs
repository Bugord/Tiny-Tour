using System;
using UnityEngine.Serialization;

namespace Level.Data
{
    [Serializable]
    public class LogisticData
    {
        public RoadTileData[] roadTileData;
        public GoalData[] goalsData;
        public IntermediatePointData[] intermediatePointsData;
    }
}
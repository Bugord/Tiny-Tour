using System;

namespace Level.Data
{
    [Serializable]
    public class LogisticData
    {
        public RoadTileData[] roadTileData;
        public TargetData[] targetsData;
        public IntermediatePointData[] intermediatePointsData;
    }
}
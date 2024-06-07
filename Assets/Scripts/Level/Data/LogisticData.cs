using System;

namespace Level.Data
{
    [Serializable]
    public class LogisticData
    {
        public SpawnPointData[] spawnPointsData;
        public TargetData[] targetsData;
        public IntermediatePointData[] intermediatePointsData;
    }
}
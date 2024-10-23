using System;
using Core;
using Core.LevelEditing;
using Game.Common.Cars.Core;

namespace Game.Common.Level.Data
{
    [Serializable]
    public class CarSpawnData : BaseTileData
    {
        public CarType carType;
        public TeamColor teamColor;
        public Direction direction;
    }
}
using System;
using Cars;
using Core;
using Core.LevelEditing;

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
using System;
using Cars;
using Core;
using UnityEngine;

namespace Level.Data
{
    [Serializable]
    public class CarSpawnData
    {
        public Vector3Int pos;
        public CarType carType;
        public TeamColor teamColor;
        public Direction direction;
    }
}
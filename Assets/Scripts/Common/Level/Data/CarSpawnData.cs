using System;
using Cars;
using Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Level.Data
{
    [Serializable]
    public struct CarSpawnData
    {
        public Vector2Int position;
        public CarType carType;
        public TeamColor teamColor;
        public Direction direction;

        public CarSpawnData(Vector2Int position, CarType carType, TeamColor teamColor, Direction direction)
        {
            this.position = position;
            this.carType = carType;
            this.teamColor = teamColor;
            this.direction = direction;
        }
    }
}
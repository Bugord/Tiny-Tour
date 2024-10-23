using System;
using Core;
using Game.Common.Cars.Core;
using UnityEngine.Tilemaps;

namespace Game.Common.Tiles.Data
{
    [Serializable]
    public struct CarSpawnPointData
    {
        public CarType CarType;
        public TeamColor Color;
        public Direction Direction;
        public Tile Tile;

        public CarSpawnPointData(CarType carType, TeamColor color, Direction direction, Tile tile)
        {
            CarType = carType;
            Color = color;
            Direction = direction;
            Tile = tile;
        }
    }
}
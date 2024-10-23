using Core;
using Game.Common.Cars.Core;
using Game.Gameplay.Cars.Model;
using UnityEngine;

namespace Game.Gameplay.Cars.Core
{
    public interface ICarsFactory
    {
        Car Create(Vector3 position, Direction direction, CarType carType, TeamColor teamColor);
    }
}
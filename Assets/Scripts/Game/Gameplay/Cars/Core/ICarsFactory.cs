using Cars;
using Core;
using Game.Common.Cars.Core;
using UnityEngine;

namespace Gameplay.Cars
{
    public interface ICarsFactory
    {
        Car Create(Vector3 position, Direction direction, CarType carType, TeamColor teamColor);
    }
}
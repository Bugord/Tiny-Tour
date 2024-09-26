using Cars;
using Core;
using UnityEngine;

namespace Gameplay.Cars
{
    public interface ICarsFactory
    {
        Car Create(Vector3 position, Direction direction, CarType carType);
    }
}
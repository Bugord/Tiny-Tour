using Cars;
using Core;
using Level.Data;
using UnityEngine;

namespace Gameplay.Cars
{
    public interface ICarsService
    {
        void SpawnCars(CarSpawnData[] carsSpawnData);
    }
}
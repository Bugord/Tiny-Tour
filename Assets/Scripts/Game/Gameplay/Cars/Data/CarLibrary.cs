using System.Collections.Generic;
using System.Linq;
using Game.Common.Cars.Core;
using UnityEngine;

namespace Game.Gameplay.Cars.Data
{
    [CreateAssetMenu]
    public class CarLibrary : ScriptableObject
    {
        public List<CarData> cars;

        public CarData GetCarData(CarType carType)
        {
            return cars.FirstOrDefault(car => car.carType == carType);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

namespace Cars
{
    [CreateAssetMenu]
    public class CarLibrary : ScriptableObject
    {
        public List<CarData> cars;

        public CarData GetCarData(CarType carType, Team team)
        {
            return cars.FirstOrDefault(car => car.carType == carType && car.team == team);
        }
    }
}
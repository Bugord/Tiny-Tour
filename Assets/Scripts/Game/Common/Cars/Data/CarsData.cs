using AYellowpaper.SerializedCollections;
using Cars;
using UnityEngine;

namespace Game.Common.Cars.Data
{
    [CreateAssetMenu(fileName = "data_cars", menuName = "Car/Data/Library")]
    public class CarsData : ScriptableObject
    {
        public SerializedDictionary<CarType, CarColorData> TypesData;
    }
}
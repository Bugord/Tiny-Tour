using AYellowpaper.SerializedCollections;
using Core;
using UnityEngine;

namespace Game.Common.Cars.Data
{
    [CreateAssetMenu(fileName = "data_car_direction", menuName = "Car/Data/Direction")]
    public class CarDirectionData : ScriptableObject
    {
        public SerializedDictionary<Direction, Sprite> DirectionData;
    }
}
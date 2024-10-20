using AYellowpaper.SerializedCollections;
using Core;
using UnityEngine;

namespace Game.Common.Cars.Data
{
    [CreateAssetMenu(fileName = "data_car_color", menuName = "Car/Data/Color")]
    public class CarColorData : ScriptableObject
    {
        public SerializedDictionary<TeamColor, CarDirectionData> ColorData;
    }
}
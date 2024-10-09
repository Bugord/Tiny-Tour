using AYellowpaper.SerializedCollections;
using Core;
using UnityEngine;

namespace Cars
{
    [CreateAssetMenu]
    public class CarData : ScriptableObject
    {
        public CarType carType;
        public SerializedDictionary<TeamColor, CarVisualData> visualsData;
        public float speed;
    }
}
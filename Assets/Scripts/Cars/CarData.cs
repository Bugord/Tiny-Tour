using AYellowpaper.SerializedCollections;
using Core;
using UnityEngine;

namespace Cars
{
    [CreateAssetMenu]
    public class CarData : ScriptableObject
    {
        public CarType carType;
        public SerializedDictionary<Team, CarVisualData> visualsData;
        public float speed;
    }
}
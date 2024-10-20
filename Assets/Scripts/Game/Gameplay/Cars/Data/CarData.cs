using Game.Common.Cars.Core;
using Game.Common.Cars.Data;
using UnityEngine;

namespace Cars
{
    [CreateAssetMenu]
    public class CarData : ScriptableObject
    {
        public CarType carType;
        public CarColorData colorData;
        public float speed;
    }
}
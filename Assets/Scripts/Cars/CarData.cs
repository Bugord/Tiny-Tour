using AYellowpaper.SerializedCollections;
using Core;
using Tiles;
using UnityEngine;

namespace Cars
{
    [CreateAssetMenu]
    public class CarData : ScriptableObject
    {
        public CarType carType;
        public Team team;
        public SerializedDictionary<Direction, Sprite> directionSprites;
        public float speed;
    }
}
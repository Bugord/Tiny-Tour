using AYellowpaper.SerializedCollections;
using Tiles;
using UnityEngine;

namespace Cars
{
    [CreateAssetMenu]
    public class CarData : ScriptableObject
    {
        public SerializedDictionary<Direction, Sprite> directionSprites;
        public float speed;
    }
}
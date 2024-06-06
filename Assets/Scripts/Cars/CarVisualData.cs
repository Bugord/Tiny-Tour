using AYellowpaper.SerializedCollections;
using Core;
using UnityEngine;

namespace Cars
{
    [CreateAssetMenu]
    public class CarVisualData : ScriptableObject
    {
        public SerializedDictionary<Direction, Sprite> directionSprites;
    }
}
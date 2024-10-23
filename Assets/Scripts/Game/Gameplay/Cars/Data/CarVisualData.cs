using AYellowpaper.SerializedCollections;
using Core;
using UnityEngine;

namespace Game.Gameplay.Cars.Data
{
    [CreateAssetMenu]
    public class CarVisualData : ScriptableObject
    {
        public SerializedDictionary<Direction, Sprite> directionSprites;
    }
}
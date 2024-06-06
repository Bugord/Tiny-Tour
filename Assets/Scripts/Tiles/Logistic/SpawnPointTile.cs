using AYellowpaper.SerializedCollections;
using Cars;
using Core;
using UnityEngine;

namespace Tiles.Logistic
{
    [CreateAssetMenu]
    public class SpawnPointVisualData : ScriptableObject
    {
        public CarType carType;
        public SerializedDictionary<Team, CarVisualData> carVisualsData;

        public Sprite GetSprite(Team team, Direction direction)
        {
            return carVisualsData[team].directionSprites[direction];
        }
    }
}
using AYellowpaper.SerializedCollections;
using Core;
using Game.Common.Cars.Core;
using Game.Gameplay.Cars.Data;
using UnityEngine;

namespace Tiles.Logistic
{
    [CreateAssetMenu]
    public class SpawnPointVisualData : ScriptableObject
    {
        public CarType carType;
        public SerializedDictionary<TeamColor, CarVisualData> carVisualsData;

        public Sprite GetSprite(TeamColor teamColor, Direction direction)
        {
            return carVisualsData[teamColor].directionSprites[direction];
        }
    }
}
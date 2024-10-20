using System;
using AYellowpaper.SerializedCollections;
using Cars;
using Core;
using Game.Common.Cars.Core;
using Tiles.Ground;
using UnityEngine;

namespace Game.Gameplay.Editing.Options.Data
{
    [CreateAssetMenu(fileName = "data_editor_car_spawn_point", menuName = "Data/Editor Option/Car Spawn Point Data")]
    public class CarSpawnPointEditorOptionData : EditorOptionData
    {
        [field: SerializeField]
        public SerializedDictionary<CarType, CarVariants> ColoredCarSpawnData { get; private set; }
        
        [Serializable]
        public class CarVariants
        {
            [field: SerializeField]
            public SerializedDictionary<TeamColor, Sprite> ColoredCarVariants { get; private set; }
        }
    }
}
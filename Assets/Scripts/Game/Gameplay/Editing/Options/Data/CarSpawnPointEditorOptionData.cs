using AYellowpaper.SerializedCollections;
using Core;
using Tiles.Ground;
using UnityEngine;

namespace Game.Gameplay.Editing.Options.Data
{
    [CreateAssetMenu(fileName = "data_editor_car_spawn_point", menuName = "Data/Editor Option/Car Spawn Point Data")]
    public class CarSpawnPointEditorOptionData : EditorOptionData
    {
        [field: SerializeField]
        public SerializedDictionary<TeamColor, Sprite> ColoredCarSpawnData { get; private set; }
    }
}
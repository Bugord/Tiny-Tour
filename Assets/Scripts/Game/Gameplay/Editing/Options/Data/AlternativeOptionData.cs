using AYellowpaper.SerializedCollections;
using Core;
using UnityEngine;

namespace Game.Gameplay.Editing.Options.Data
{
    [CreateAssetMenu(fileName = "data_editor__alternative_option", menuName = "Data/Editor Option/Alternative Option")]
    public class AlternativeOptionData : ScriptableObject
    {
        [field: SerializeField]
        public Sprite DefaultIcon { get; private set; }
    }
}
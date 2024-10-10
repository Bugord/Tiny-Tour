using AYellowpaper.SerializedCollections;
using Core;
using UnityEngine;

namespace Gameplay.Editing.Options.Data
{
    [CreateAssetMenu(fileName = "data_editor__alternative_option", menuName = "Data/Editor Option/Alternative Option")]
    public class AlternativeOption : ScriptableObject
    {
        [field: SerializeField]
        public Sprite Icon { get; private set; }

        [field: SerializeField]
        public SerializedDictionary<TeamColor, Sprite> ColoredIcons { get; private set; }
    }
}
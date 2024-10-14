using AYellowpaper.SerializedCollections;
using Core;
using UnityEngine;

namespace Game.Gameplay.Editing.Options.Data
{
    [CreateAssetMenu(fileName = "data_editor__colored_alternative_option", menuName = "Data/Editor Option/Alternative Option Colored")]
    public class ColoredAlternativeOptionData : AlternativeOptionData
    {
        [field: SerializeField]
        public SerializedDictionary<TeamColor, Sprite> ColoredIcons { get; private set; }
    }
}
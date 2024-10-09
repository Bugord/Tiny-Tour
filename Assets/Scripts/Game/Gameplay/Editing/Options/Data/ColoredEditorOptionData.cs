using AYellowpaper.SerializedCollections;
using Core;
using UnityEngine;

namespace Gameplay.Editing.Options.Data
{
    [CreateAssetMenu(fileName = "data_editor_", menuName = "Data/Editor Option/Data Colored")]
    public class ColoredEditorOptionData : EditorOptionData
    {
        [SerializedDictionary]
        public SerializedDictionary<TeamColor, Sprite> ColoredIcons;
    }
}
using AYellowpaper.SerializedCollections;
using Core;
using UnityEngine;

namespace Gameplay.Editing.Options.Data
{
    [CreateAssetMenu(fileName = "data_editor_", menuName = "Data/Editor Option/Data")]
    public class EditorOptionData : ScriptableObject
    {
        [field: SerializeField]
        public string Id { get; private set; }
        
        [field: SerializeField]
        public Sprite Icon { get; private set; }
        
        [field: SerializeField]
        public Sprite CustomInactiveBackground { get; private set; }
        
        [field: SerializeField]
        public Sprite CustomActiveBackground { get; private set; }
        
        [field: SerializeField]
        public SerializedDictionary<TeamColor, Sprite> ColoredIcons { get; private set; }
        
        [field: SerializeField]
        public AlternativeOption[] AlternativeOptions { get; private set; }
    }
}
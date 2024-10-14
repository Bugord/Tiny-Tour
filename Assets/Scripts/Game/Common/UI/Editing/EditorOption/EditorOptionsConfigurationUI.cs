using Game.Common.UI.Editing.EditorOption.AlternativePicker;
using UnityEngine;

namespace Game.Common.UI.Editing.EditorOption
{
    public class EditorOptionsConfigurationUI : MonoBehaviour
    {
        [field: SerializeField]
        public EditorOptionColorPicker EditorOptionColorPicker { get; private set; }

        [field: SerializeField]
        public EditorOptionAlternativePicker EditorOptionAlternativePicker { get; private set; }
    }
}
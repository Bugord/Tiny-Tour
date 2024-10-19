using Game.Common.UI.Editing.EditorOption.AlternativePicker;
using Game.Common.UI.Editing.EditorOption.ColorPicker;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Common.UI.Editing.EditorOption
{
    public class EditorOptionsConfigurationUI : MonoBehaviour
    {
        [field: SerializeField]
        public EditorOptionColorPicker EditorOptionColorPicker { get; private set; }

        [field: SerializeField]
        public EditorOptionAlternativePicker EditorOptionAlternativePicker { get; private set; }

        public void OnDeselect()
        {
            // gameObject.SetActive(false);
        }

        public void EnableColorPicker()
        {
            EditorOptionColorPicker.gameObject.SetActive(true);
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}
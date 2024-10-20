using System;
using Core;
using UnityEngine;

namespace Game.Common.UI.Editing.EditorOption.ColorPicker
{
    public class EditorOptionSettingsColorPickerUI : MonoBehaviour
    {
        public event Action<TeamColor> ColorSelected;

        [SerializeField]
        private ColorPickerButton[] colorPickerButtons;

        private void OnEnable()
        {
            foreach (var colorPickerButton in colorPickerButtons) {
                colorPickerButton.ColorSelected += OnColorSelected;
            }
        }

        private void OnDisable()
        {
            foreach (var colorPickerButton in colorPickerButtons) {
                colorPickerButton.ColorSelected -= OnColorSelected;
            }
        }

        private void OnColorSelected(TeamColor color)
        {
            ColorSelected?.Invoke(color);
        }
    }
}
using System;
using Core;
using Game.Common.UI.Editing.EditorOption.ColorPicker;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Common.UI.Editing.EditorOption
{
    [RequireComponent(typeof(ToggleGroup))]
    public class EditorOptionColorPicker : MonoBehaviour
    {
        public event Action<TeamColor> ColorSelected;

        [SerializeField]
        private ColorPickerToggle[] colorPickerToggles;

        private void OnEnable()
        {
            foreach (var colorPickerToggle in colorPickerToggles) {
                colorPickerToggle.ColorSelected += ColorSelected;
            }
        }

        private void OnDisable()
        {
            foreach (var colorPickerToggle in colorPickerToggles) {
                colorPickerToggle.ColorSelected -= ColorSelected;
            }
        }
    }
}
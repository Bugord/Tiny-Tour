using System;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Common.UI.Editing.EditorOption.ColorPicker
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
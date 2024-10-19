using System;
using System.Collections.Generic;
using Core;
using Game.Common.UI.Editing.EditorOption.AlternativePicker;
using Game.Common.UI.Editing.EditorOption.ColorPicker;
using UnityEngine;

namespace Game.Common.UI.Editing.EditorOption
{
    public class EditorOptionsConfigurationUI : MonoBehaviour
    {
        public event Action<TeamColor> ColorSelected;
        public event Action<int> AlternativeSelected;

        [SerializeField]
        private EditorOptionSettingsColorPickerUI editorOptionColorPicker;

        [SerializeField]
        private EditorOptionSettingsAlternativePickerUI editorOptionAlternativePicker;

        private void OnEnable()
        {
            editorOptionColorPicker.ColorSelected += OnColorSelected;
            editorOptionAlternativePicker.AlternativeSelected += OnAlternativeSelected;
        }

        private void OnDisable()
        {
            editorOptionColorPicker.ColorSelected -= OnColorSelected;
            editorOptionAlternativePicker.AlternativeSelected -= OnAlternativeSelected;
        }

        public void EnableColorPicker()
        {
            editorOptionColorPicker.gameObject.SetActive(true);
        }

        public void SetAlternatives(Dictionary<int, Sprite> alternatives)
        {
            editorOptionAlternativePicker.SetData(alternatives);
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        private void OnColorSelected(TeamColor color)
        {
            ColorSelected?.Invoke(color);
        }

        private void OnAlternativeSelected(int alternativeIndex)
        {
            AlternativeSelected?.Invoke(alternativeIndex);
        }
    }
}
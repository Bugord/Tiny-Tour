using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

namespace Game.Common.UI.Editing.EditorOption
{
    public class EditorOptionsConfiguration
    {
        public event Action<TeamColor> ColorSelected;
        public event Action<int> AlternativeSelected;

        private readonly EditorOptionsConfigurationUI editorOptionsConfigurationUI;

        public TeamColor SelectedColor { get; private set; }
        public int SelectedAlternativeIndex { get; private set; }

        public bool IsConfigured { get; private set; }

        public EditorOptionsConfiguration(EditorOptionsConfigurationUI editorOptionsConfigurationUI)
        {
            this.editorOptionsConfigurationUI = editorOptionsConfigurationUI;

            this.editorOptionsConfigurationUI.ColorSelected += OnColorSelected;
            this.editorOptionsConfigurationUI.AlternativeSelected += OnAlternativeSelected;
        }

        public void EnableColorPicker()
        {
            IsConfigured = true;
            editorOptionsConfigurationUI.EnableColorPicker();
        }

        public void SetAlternatives<T>(Dictionary<T, Sprite> alternatives) where T : Enum
        {
            IsConfigured = true;
            var parsedAlternatives = alternatives.ToDictionary(
                alternative => (int)(object)alternative.Key,
                alternative => alternative.Value);

            editorOptionsConfigurationUI.SetAlternatives(parsedAlternatives);
        }

        public void Toggle()
        {
            if (editorOptionsConfigurationUI.gameObject.activeSelf) {
                Close();
            }
            else {
                Open();
            }
        }

        public void Open()
        {
            if (!IsConfigured) {
                return;
            }
            
            editorOptionsConfigurationUI.gameObject.SetActive(true);
        }

        public void Close()
        {
            editorOptionsConfigurationUI.gameObject.SetActive(false);
        }

        private void OnColorSelected(TeamColor color)
        {
            SelectedColor = color;
            
            ColorSelected?.Invoke(color);
        }

        private void OnAlternativeSelected(int alternativeIndex)
        {
            SelectedAlternativeIndex = alternativeIndex;
            
            AlternativeSelected?.Invoke(alternativeIndex);
        }
    }
}
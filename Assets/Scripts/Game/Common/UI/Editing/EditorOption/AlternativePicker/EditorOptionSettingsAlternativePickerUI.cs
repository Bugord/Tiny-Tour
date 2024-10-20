using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Tilemaps;
using UnityEngine;

namespace Game.Common.UI.Editing.EditorOption.AlternativePicker
{
    public class EditorOptionSettingsAlternativePickerUI : MonoBehaviour
    {
        public event Action<int> AlternativeSelected;

        [SerializeField]
        private EditorOptionAlternative editorOptionAlternativePrefab;

        private readonly List<EditorOptionAlternative> editorOptionAlternatives = new List<EditorOptionAlternative>();

        public void SetData(Dictionary<int, Sprite> alternativesData)
        {
            foreach (var alternativeData in alternativesData) {
                var existingElement = editorOptionAlternatives.Find(element => element.Id == alternativeData.Key);
                if (!existingElement) {
                    existingElement = Instantiate(editorOptionAlternativePrefab, transform);
                    existingElement.OptionSelected += OnAlternativeSelected;
                    editorOptionAlternatives.Add(existingElement);
                }

                existingElement.SetData(alternativeData.Key, alternativeData.Value);
            }
        }

        private void OnDestroy()
        {
            foreach (var editorOptionAlternative in editorOptionAlternatives) {
                editorOptionAlternative.OptionSelected -= OnAlternativeSelected;
            }
        }

        private void OnAlternativeSelected(int selectedAlternative)
        {
            AlternativeSelected?.Invoke(selectedAlternative);
        }
    }
}
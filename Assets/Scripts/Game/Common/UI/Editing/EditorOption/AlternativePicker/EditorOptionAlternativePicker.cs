﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Common.UI.Editing.EditorOption.AlternativePicker
{
    public class EditorOptionAlternativePicker : MonoBehaviour
    {
        public event Action<int> AlternativeSelected;

        [SerializeField]
        private EditorOptionAlternative editorOptionAlternativePrefab;

        private readonly List<EditorOptionAlternative> editorOptionAlternatives = new List<EditorOptionAlternative>();

        public void SetData(Dictionary<int, Sprite> alternativesData)
        {
            foreach (var alternativeData in alternativesData) {
                var editorOptionAlternative = Instantiate(editorOptionAlternativePrefab, transform);
                editorOptionAlternative.SetData(alternativeData.Key, alternativeData.Value);
                editorOptionAlternative.OptionSelected += AlternativeSelected;

                editorOptionAlternatives.Add(editorOptionAlternative);
            }
        }

        private void OnDestroy()
        {
            foreach (var editorOptionAlternative in editorOptionAlternatives) {
                editorOptionAlternative.OptionSelected -= AlternativeSelected;
            }
        }
    }
}
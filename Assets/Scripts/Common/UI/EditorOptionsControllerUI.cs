﻿using System;
using System.Collections.Generic;
using System.Linq;
using Core.Logging;
using Gameplay.Editing.Options.Data;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Common.UI
{
    public class EditorOptionsControllerUI : MonoBehaviour
    {
        public event Action<string> EditorOptionSelected;

        [SerializeField]
        private TileEditorOptionUI tileEditorOptionUIPrefab;

        [SerializeField]
        private ToggleGroup toggleGroup;

        private List<TileEditorOptionUI> tileEditorOptions;
        private ILogger<EditorOptionsControllerUI> logger;

        [Inject]
        private void Construct(ILogger<EditorOptionsControllerUI> logger)
        {
            this.logger = logger;
        }
        
        public void Init(IEnumerable<EditorOptionData> editorOptionsData)
        {
            tileEditorOptions = new List<TileEditorOptionUI>();
            foreach (var editorOptionData in editorOptionsData) {
                var editorOptionUI = Instantiate(tileEditorOptionUIPrefab, transform);
                editorOptionUI.Setup(toggleGroup, editorOptionData);
                editorOptionUI.ToggledOn += OnEditorOptionToggledOn;
                tileEditorOptions.Add(editorOptionUI);
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);
        }

        public void SelectOption(string id)
        {
            var editorOption = tileEditorOptions.FirstOrDefault(option => option.Id == id);
            if (editorOption == null) {
                logger.LogError($"Couldn't select option {id}, it does not exist");
                return;
            }
            
            editorOption.Toggle();
        }

        private void OnDestroy()
        {
            foreach (var tileEditorOption in tileEditorOptions) {
                tileEditorOption.ToggledOn -= OnEditorOptionToggledOn;
            }
        }

        private void OnEditorOptionToggledOn(string id)
        {
            EditorOptionSelected?.Invoke(id);
        }
    }
}
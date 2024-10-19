using System;
using System.Collections.Generic;
using System.Linq;
using Core.Logging;
using Game.Common.UI.Editing.EditorOption;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using ColorButton = Game.Workshop.UI.ColorButton;

namespace Game.Common.UI
{
    public class EditorOptionsControllerUI : MonoBehaviour
    {
        public event Action<string> EditorOptionSelected;

        [SerializeField]
        private EditorOptionUI tileEditorOptionUIPrefab;

        [SerializeField]
        private ToggleGroup toggleGroup;

        private ILogger<EditorOptionsControllerUI> logger;

        private List<EditorOptionUI> tileEditorOptions;

        [Inject]
        private void Construct(ILogger<EditorOptionsControllerUI> logger)
        {
            this.logger = logger;
            tileEditorOptions = new List<EditorOptionUI>();
        }

        public EditorOptionUI InstantiateEditorOptionUI(string id)
        {
            var editorOptionUI = Instantiate(tileEditorOptionUIPrefab, transform);
            editorOptionUI.Init(id, toggleGroup);
            tileEditorOptions.Add(editorOptionUI);

            editorOptionUI.ToggledOn += OnEditorOptionToggledOn;

            return editorOptionUI;
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
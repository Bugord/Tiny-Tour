using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Logging;
using Core.Navigation;
using Game.Common.Editors.Options.Core;
using Game.Common.UI.Editing.EditorOption;
using Game.Main.UI.Controls.Playing;
using LevelEditing.UI;
using UI.Screens;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using ColorButton = Game.Workshop.UI.ColorButton;

namespace Common.UI
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
        private ColorButton colorButton;

        [Inject]
        private void Construct(ILogger<EditorOptionsControllerUI> logger)
        {
            this.logger = logger;
            tileEditorOptions = new List<EditorOptionUI>();
        }

        public EditorOptionUI InstantiateEditorOptionUI(string id)
        {
            var editorOptionUI = Instantiate(tileEditorOptionUIPrefab, transform);
            editorOptionUI.SetId(id);
            editorOptionUI.SetToggleGroup(toggleGroup);
            tileEditorOptions.Add(editorOptionUI);

            editorOptionUI.ToggledOn += OnEditorOptionToggledOn;

            return editorOptionUI;
        }

        // public void Init(IEnumerable<EditorOptionData> editorOptionsData)
        // {
        //     tileEditorOptions = new List<TileEditorOptionUI>();
        //     foreach (var editorOptionData in editorOptionsData) {
        //         var editorOptionUI = Instantiate(tileEditorOptionUIPrefab, transform);
        //         editorOptionUI.Setup(toggleGroup, editorOptionData);
        //         tileEditorOptions.Add(editorOptionUI);
        //     }
        //
        //     LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);
        // }

        public void SelectOption(string id)
        {
            var editorOption = tileEditorOptions.FirstOrDefault(option => option.Id == id);
            if (editorOption == null) {
                logger.LogError($"Couldn't select option {id}, it does not exist");
                return;
            }

            editorOption.Toggle();
        }

        // public void SetColor(TeamColor color)
        // {
        //     foreach (var tileEditorOption in tileEditorOptions) {
        //         tileEditorOption.UpdateColor(color);
        //     }
        // }

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
using System;
using System.Collections.Generic;
using Core.Logging;
using Game.Common.Editors.Options.Core;
using Game.Common.UI;
using Game.Gameplay.Editing.Options.Model;
using Unity.VisualScripting;

namespace Game.Common.EditorOptions
{
    public class EditorOptionsController : IEditorOptionsController, IDisposable
    {
        private readonly IEditorOptionFactory editorOptionFactory;
        private readonly ILogger<EditorOptionsController> logger;
        private readonly Dictionary<Type, BaseEditorOption> editorOptions;
        private readonly EditorOptionsControllerUI editorOptionsControllerUI;

        private BaseEditorOption cachedEditorOption;

        public BaseEditorOption SelectedOption { get; private set; }

        public EditorOptionsController(IEditorOptionsControllerUIProvider editorOptionsControllerUIProvider,
            IEditorOptionFactory editorOptionFactory, ILogger<EditorOptionsController> logger)
        {
            this.editorOptionFactory = editorOptionFactory;
            this.logger = logger;
            editorOptionsControllerUI = editorOptionsControllerUIProvider.EditorOptionsControllerUI;
            editorOptions = new Dictionary<Type, BaseEditorOption>();
        }
        
        public void AddOption<T>() where T : BaseEditorOption
        {
            var optionUI = editorOptionsControllerUI.CreateEditorOptionUI();
            var option = editorOptionFactory.Create<T>(optionUI);

            option.Selected += OnOptionSelected;

            editorOptions.Add(typeof(T), option);
        }

        public void Dispose()
        {
            foreach (var option in editorOptions.Values) {
                option.Selected -= OnOptionSelected;
            }
        }

        public void SelectOption(BaseEditorOption option)
        {
            SelectedOption?.OnDeselected();
            SelectedOption = option;
            SelectedOption?.OnSelected();
        }

        public void SelectOption<T>() where T : BaseEditorOption
        {
            if (editorOptions.TryGetValue(typeof(T), out var option)) {
                SelectOption(option);
                return;
            }
            
            logger.LogWarning($"Option with type {typeof(T)} was not added");
        }

        private void OnOptionSelected(BaseEditorOption option)
        {
            SelectOption(option);
        }
    }
}
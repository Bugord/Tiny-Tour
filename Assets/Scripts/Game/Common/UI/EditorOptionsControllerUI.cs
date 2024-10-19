using System.Collections.Generic;
using Game.Common.Editors.Options.Core;
using Game.Common.UI.Editing.EditorOption;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Common.UI
{
    public class EditorOptionsControllerUI : MonoBehaviour
    {
        [SerializeField]
        private ToggleGroup toggleGroup;

        private IEditorOptionUIFactory editorOptionUIFactory;
        private List<EditorOptionUI> tileEditorOptions;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            // this.editorOptionUIFactory = editorOptionUIFactory;
            this.editorOptionUIFactory = diContainer.Resolve<IEditorOptionUIFactory>();
            
            tileEditorOptions = new List<EditorOptionUI>();
        }

        public EditorOptionUI CreateEditorOptionUI()
        {
            var editorOptionUI = editorOptionUIFactory.Create(transform, toggleGroup);
            tileEditorOptions.Add(editorOptionUI);
            
            return editorOptionUI;
        }
    }
}
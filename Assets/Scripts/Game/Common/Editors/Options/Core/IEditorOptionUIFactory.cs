using Game.Common.UI.Editing.EditorOption;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Common.Editors.Options.Core
{
    public interface IEditorOptionUIFactory
    {
        EditorOptionUI Create(Transform rootTransform, ToggleGroup toggleGroup, string id);
    }
}
using Game.Common.UI.Editing.EditorOption;
using Game.Gameplay.Editing.Options.Model;

namespace Game.Common.Editors.Options.Core
{
    public interface IEditorOptionFactory
    {
        T Create<T>(EditorOptionUI optionUI) where T : BaseEditorOption;
    }
}
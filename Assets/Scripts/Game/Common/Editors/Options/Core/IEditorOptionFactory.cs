using Gameplay.Editing.Editors;

namespace Common.Editors.Options.Core
{
    public interface IEditorOptionFactory
    {
        T Create<T>() where T : BaseEditorOption;
        T Create<T>(string id) where T : BaseEditorOption;
    }
}
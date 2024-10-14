using Game.Gameplay.Editing.Options.Model;

namespace Game.Common.Editors.Options.Core
{
    public interface IEditorOptionFactory
    {
        T Create<T>() where T : BaseEditorOption;
        T Create<T>(string id) where T : BaseEditorOption;
    }
}
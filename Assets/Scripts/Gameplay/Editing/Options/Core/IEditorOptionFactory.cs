namespace Gameplay.Editing.Editors
{
    public interface IEditorOptionFactory
    {
        T Create<T>() where T : BaseEditorOption;
        T Create<T>(string id) where T : BaseEditorOption;
    }
}
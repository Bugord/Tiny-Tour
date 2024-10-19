using Game.Common.UI;

namespace Game.Common.EditorOptions
{
    public interface IEditorOptionsControllerUIProvider
    {
        EditorOptionsControllerUI EditorOptionsControllerUI { get; }
    }
}
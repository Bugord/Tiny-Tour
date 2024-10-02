using LevelEditor.EditorState.States;

namespace LevelEditor.EditorState.Core
{
    public interface IEditorStateFactory
    {
        T Create<T>(EditorStateMachine editorStateMachine) where T : BaseEditorState;
    }
}
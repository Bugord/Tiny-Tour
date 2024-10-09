using LevelEditing.EditorState.States;

namespace LevelEditing.EditorState.Core
{
    public interface IEditorStateFactory
    {
        T Create<T>(EditorStateMachine editorStateMachine) where T : BaseEditorState;
    }
}
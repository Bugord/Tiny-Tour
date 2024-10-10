using LevelEditing.EditorState.Core;
using LevelEditing.EditorState.States;
using Zenject;

namespace LevelEditor
{
    public class WorkshopEntryPoint : IInitializable
    {
        private readonly EditorStateMachine editorStateMachine;

        public WorkshopEntryPoint(EditorStateMachine editorStateMachine)
        {
            this.editorStateMachine = editorStateMachine;
        }
        
        public void Initialize()
        {
            editorStateMachine.ChangeState<LoadEditorState>();
        }
    }
}
using LevelEditor.EditorState.Core;
using LevelEditor.EditorState.States;
using Zenject;

namespace LevelEditor
{
    public class LevelEditorEntryPoint : IInitializable
    {
        private readonly EditorStateMachine editorStateMachine;

        public LevelEditorEntryPoint(EditorStateMachine editorStateMachine)
        {
            this.editorStateMachine = editorStateMachine;
        }
        
        public void Initialize()
        {
            editorStateMachine.ChangeState<LoadEditorState>();
        }
    }
}
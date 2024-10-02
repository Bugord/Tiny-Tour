using LevelEditor.EditorState.Core;

namespace LevelEditor.EditorState.States
{
    public abstract class BaseEditorState
    {
        protected EditorStateMachine EditorStateMachine;
        
        protected BaseEditorState(EditorStateMachine editorStateMachine)
        {
            EditorStateMachine = editorStateMachine;
        }
        
        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
        }
    }
}
using LevelEditing.EditorState.Core;

namespace LevelEditing.EditorState.States
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
using Game.Workshop.WorkshopState.Core;

namespace LevelEditing.EditorState.States
{
    public abstract class BaseEditorState
    {
        protected WorkshopStateMachine WorkshopStateMachine;
        
        protected BaseEditorState(WorkshopStateMachine workshopStateMachine)
        {
            WorkshopStateMachine = workshopStateMachine;
        }
        
        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
        }
    }
}
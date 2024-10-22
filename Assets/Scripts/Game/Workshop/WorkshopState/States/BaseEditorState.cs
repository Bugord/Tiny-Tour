using Game.Workshop.WorkshopState.Core;

namespace Game.Workshop.WorkshopState.States
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
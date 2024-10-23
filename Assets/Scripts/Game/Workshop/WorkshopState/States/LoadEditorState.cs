using Game.Workshop.WorkshopState.Core;

namespace Game.Workshop.WorkshopState.States
{
    public class LoadEditorState : BaseEditorState
    {
        public LoadEditorState(WorkshopStateMachine workshopStateMachine) : base(workshopStateMachine)
        {
        }

        public override void OnEnter()
        {
            WorkshopStateMachine.ChangeState<EditingLevelEditorState>();
        }
    }
}
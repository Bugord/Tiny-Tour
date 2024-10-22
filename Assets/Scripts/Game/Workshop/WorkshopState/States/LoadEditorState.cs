using Game.Workshop.Core;
using Game.Workshop.WorkshopState.Core;

namespace Game.Workshop.WorkshopState.States
{
    public class LoadEditorState : BaseEditorState
    {
        private readonly IWorkshopService workshopService;

        public LoadEditorState(WorkshopStateMachine workshopStateMachine, IWorkshopService workshopService) : base(workshopStateMachine)
        {
            this.workshopService = workshopService;
        }

        public override void OnEnter()
        {
            workshopService.LoadCurrentLevel();
            WorkshopStateMachine.ChangeState<EditingLevelEditorState>();
        }
    }
}
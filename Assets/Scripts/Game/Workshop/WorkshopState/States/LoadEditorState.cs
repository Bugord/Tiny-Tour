using Common.Level.Core;
using Game.Workshop.WorkshopState.Core;
using Level;
using LevelEditor.Level.Core;

namespace LevelEditing.EditorState.States
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
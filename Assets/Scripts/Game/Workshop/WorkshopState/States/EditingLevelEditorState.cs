using Core.Navigation;
using Game.Workshop.Core;
using Game.Workshop.Editing.Core;
using Game.Workshop.WorkshopState.Core;
using UI.Screens;

namespace Game.Workshop.WorkshopState.States
{
    public class EditingLevelEditorState : BaseEditorState
    {
        private readonly WorkshopLevelEditorController workshopLevelEditorController;
        private readonly IWorkshopService workshopService;
        private readonly EditLevelScreen editLevelScreen;

        public EditingLevelEditorState(WorkshopStateMachine workshopStateMachine, WorkshopLevelEditorController workshopLevelEditorController, INavigationService navigationService, IWorkshopService workshopService) : base(workshopStateMachine)
        {
            this.workshopLevelEditorController = workshopLevelEditorController;
            this.workshopService = workshopService;
            editLevelScreen = navigationService.GetScreen<EditLevelScreen>();
        }

        public override void OnEnter()
        {
            editLevelScreen.ResetPressed += OnResetPressed;
            editLevelScreen.SavePressed += OnSavePressed;
            workshopLevelEditorController.EnableEditing();
        }

        public override void OnExit()
        {
            editLevelScreen.ResetPressed -= OnResetPressed;
            editLevelScreen.SavePressed -= OnSavePressed;
            workshopLevelEditorController.DisableEditing();
        }

        private void OnResetPressed()
        {
            workshopService.ResetLevel();
        }

        private void OnSavePressed()
        {
            workshopService.SaveLevel();
        }
    }
}
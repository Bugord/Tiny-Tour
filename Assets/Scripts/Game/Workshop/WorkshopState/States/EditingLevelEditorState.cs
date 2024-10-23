using Game.Main.UI.Screens;
using Game.Workshop.Core;
using Game.Workshop.Editing.Core;
using Game.Workshop.UI;
using Game.Workshop.WorkshopState.Core;

namespace Game.Workshop.WorkshopState.States
{
    public class EditingLevelEditorState : BaseEditorState
    {
        private readonly WorkshopLevelEditorController workshopLevelEditorController;
        private readonly IWorkshopUIProvider workshopUIProvider;
        private readonly IWorkshopEditorService workshopEditorService;

        public EditingLevelEditorState(WorkshopStateMachine workshopStateMachine,
            WorkshopLevelEditorController workshopLevelEditorController, IWorkshopUIProvider workshopUIProvider,
            IWorkshopEditorService workshopEditorService) : base(workshopStateMachine)
        {
            this.workshopLevelEditorController = workshopLevelEditorController;
            this.workshopUIProvider = workshopUIProvider;
            this.workshopEditorService = workshopEditorService;
        }

        public override void OnEnter()
        {
            workshopUIProvider.EditLevelScreen.ResetPressed += OnResetPressed;
            workshopUIProvider.EditLevelScreen.SavePressed += OnSavePressed;
            workshopLevelEditorController.EnableEditing();
        }

        public override void OnExit()
        {
            workshopUIProvider.EditLevelScreen.ResetPressed -= OnResetPressed;
            workshopUIProvider.EditLevelScreen.SavePressed -= OnSavePressed;
            workshopLevelEditorController.DisableEditing();
        }

        private void OnResetPressed()
        {
            workshopEditorService.ResetLevel();
        }

        private void OnSavePressed()
        {
            workshopEditorService.SaveLevel();
        }
    }
}
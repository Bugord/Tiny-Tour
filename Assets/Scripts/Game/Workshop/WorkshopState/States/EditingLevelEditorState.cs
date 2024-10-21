using Core.Navigation;
using Game.Workshop.Editing.Core;
using Game.Workshop.WorkshopState.Core;
using Gameplay.UI;
using LevelEditing.UI;
using LevelEditor.Level.Core;
using UI.Screens;

namespace LevelEditing.EditorState.States
{
    public class EditingLevelEditorState : BaseEditorState
    {
        private readonly WorkshopLevelEditorController workshopLevelEditorController;
        private readonly IWorkshopService workshopService;
        private readonly EditorControllerUI editorControllerUI;
        private readonly EditLevelScreen editLevelScreen;

        public EditingLevelEditorState(WorkshopStateMachine workshopStateMachine, WorkshopLevelEditorController workshopLevelEditorController, INavigationService navigationService, IWorkshopService workshopService) : base(workshopStateMachine)
        {
            this.workshopLevelEditorController = workshopLevelEditorController;
            this.workshopService = workshopService;
            editLevelScreen = navigationService.GetScreen<EditLevelScreen>();
            editorControllerUI = editLevelScreen.EditorControllerUI;
        }

        public override void OnEnter()
        {
            editorControllerUI.ResetPressed += OnResetPressed;
            editLevelScreen.SavePressed += OnSavePressed;
            workshopLevelEditorController.EnableEditing();
        }

        public override void OnExit()
        {
            editorControllerUI.ResetPressed -= OnResetPressed;
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
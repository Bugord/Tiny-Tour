using Core.Navigation;
using Gameplay.UI;
using LevelEditing.Editing.Core;
using LevelEditing.EditorState.Core;
using LevelEditing.UI;
using LevelEditor.Level.Core;
using UI.Screens;

namespace LevelEditing.EditorState.States
{
    public class EditingLevelEditorState : BaseEditorState
    {
        private readonly LevelEditorController levelEditorController;
        private readonly IWorkshopService workshopService;
        private readonly EditorControllerUI editorControllerUI;
        private readonly EditLevelScreen editLevelScreen;

        public EditingLevelEditorState(EditorStateMachine editorStateMachine, LevelEditorController levelEditorController, INavigationService navigationService, IWorkshopService workshopService) : base(editorStateMachine)
        {
            this.levelEditorController = levelEditorController;
            this.workshopService = workshopService;
            editLevelScreen = navigationService.GetScreen<EditLevelScreen>();
            editorControllerUI = editLevelScreen.EditorControllerUI;
        }

        public override void OnEnter()
        {
            editorControllerUI.ResetPressed += OnResetPressed;
            editLevelScreen.SavePressed += OnSavePressed;
            levelEditorController.EnableEditing();
        }

        public override void OnExit()
        {
            editorControllerUI.ResetPressed -= OnResetPressed;
            editLevelScreen.SavePressed -= OnSavePressed;
            levelEditorController.DisableEditing();
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
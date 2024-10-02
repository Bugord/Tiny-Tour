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
        private readonly ILevelEditorService levelEditorService;
        private readonly EditorControllerUI editorControllerUI;

        public EditingLevelEditorState(EditorStateMachine editorStateMachine, LevelEditorController levelEditorController, INavigationService navigationService, ILevelEditorService levelEditorService) : base(editorStateMachine)
        {
            this.levelEditorController = levelEditorController;
            this.levelEditorService = levelEditorService;
            editorControllerUI = navigationService.GetScreen<EditLevelScreen>().EditorControllerUI;
        }

        public override void OnEnter()
        {
            editorControllerUI.ResetPressed += OnResetPressed;
            levelEditorController.EnableEditing();
        }

        public override void OnExit()
        {
            editorControllerUI.ResetPressed -= OnResetPressed;
            levelEditorController.DisableEditing();
        }

        private void OnResetPressed()
        {
            levelEditorService.ResetLevel();
        }
    }
}
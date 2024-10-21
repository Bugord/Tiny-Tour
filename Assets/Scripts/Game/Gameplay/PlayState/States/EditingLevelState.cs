using Core.Navigation;
using Game.Gameplay.Editing;
using Game.Gameplay.Level;
using Gameplay.PlayState.Core;
using Gameplay.UI;
using UI.Screens;

namespace Game.Gameplay.PlayState.States
{
    public class EditingLevelState : BasePlayState
    {
        private readonly PlayEditorController playEditorController;
        private readonly ILevelService levelService;
        private readonly PlayControllerUI playControllerUI;

        public EditingLevelState(PlayStateMachine playStateMachine, PlayEditorController playEditorController, INavigationService navigationService, ILevelService levelService) : base(playStateMachine)
        {
            this.playEditorController = playEditorController;
            this.levelService = levelService;
            playControllerUI = navigationService.GetScreen<PlayLevelScreen>().PlayControllerUI;
        }

        public override void OnEnter()
        {
            playEditorController.EnableEditing();
            playControllerUI.PlayToggledOn += OnPlayToggledOn;
            playControllerUI.ResetPressed += OnResetPressed;
        }

        public override void OnExit()
        {
            playEditorController.DisableEditing();
            playControllerUI.PlayToggledOn -= OnPlayToggledOn;
            playControllerUI.ResetPressed -= OnResetPressed;
        }

        private void OnPlayToggledOn()
        {
            PlayStateMachine.ChangeState<PlayLevelState>();
        }

        private void OnResetPressed()
        {
            levelService.ResetLevel();
        }
    }
}
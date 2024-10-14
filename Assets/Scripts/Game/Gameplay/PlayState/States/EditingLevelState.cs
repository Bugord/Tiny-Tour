using Common;
using Common.Level.Core;
using Core.Navigation;
using Game.Gameplay.Editing;
using Gameplay.PlayState.Core;
using Gameplay.UI;
using UI.Screens;

namespace Gameplay.PlayState.States
{
    public class EditingLevelState : BasePlayState
    {
        private readonly InGameEditor inGameEditor;
        private readonly ILevelService levelService;
        private readonly PlayControllerUI playControllerUI;

        public EditingLevelState(PlayStateMachine playStateMachine, InGameEditor inGameEditor, INavigationService navigationService, ILevelService levelService) : base(playStateMachine)
        {
            this.inGameEditor = inGameEditor;
            this.levelService = levelService;
            playControllerUI = navigationService.GetScreen<PlayLevelScreen>().PlayControllerUI;
        }

        public override void OnEnter()
        {
            inGameEditor.EnableEditing();
            playControllerUI.PlayToggledOn += OnPlayToggledOn;
            playControllerUI.ResetPressed += OnResetPressed;
        }

        public override void OnExit()
        {
            inGameEditor.DisableEditing();
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
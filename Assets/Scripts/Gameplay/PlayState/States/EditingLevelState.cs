using Common;
using Core.Navigation;
using Gameplay.PlayState.Core;
using Gameplay.UI;
using UI.Screens;

namespace Gameplay.PlayState.States
{
    public class EditingLevelState : BasePlayState
    {
        private readonly InGameEditor inGameEditor;
        private readonly PlayControllerUI playControllerUI;

        public EditingLevelState(PlayStateMachine playStateMachine, InGameEditor inGameEditor, INavigationService navigationService) : base(playStateMachine)
        {
            this.inGameEditor = inGameEditor;
            playControllerUI = navigationService.GetScreen<PlayLevelScreen>().PlayControllerUI;
        }

        public override void OnEnter()
        {
            inGameEditor.EnableEditing();
            playControllerUI.PlayToggledOn += OnPlayToggledOn;
        }

        public override void OnExit()
        {
            inGameEditor.DisableEditing();
            playControllerUI.PlayToggledOn -= OnPlayToggledOn;
        }

        private void OnPlayToggledOn()
        {
            PlayStateMachine.ChangeState<PlayLevelState>();
        }
    }
}
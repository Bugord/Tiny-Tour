using Core.Navigation;
using Gameplay.Cars;
using Gameplay.Logistic;
using Gameplay.Playing;
using Gameplay.PlayState.Core;
using Gameplay.UI;
using UI.Screens;

namespace Gameplay.PlayState.States
{
    public class PlayLevelState : BasePlayState
    {
        private readonly IPlayingService playingService;
        private readonly PlayControllerUI playControllerUI;

        public PlayLevelState(PlayStateMachine playStateMachine, INavigationService navigationService,
            IPlayingService playingService)
            : base(playStateMachine)
        {
            this.playingService = playingService;
            playControllerUI = navigationService.GetScreen<PlayLevelScreen>().PlayControllerUI;
        }

        public override void OnEnter()
        {
            playControllerUI.PlayToggledOff += OnPlayToggledOff;
            playingService.PlayEnded += OnPlayEnded;
            
            playingService.Play();
        }

        private void OnPlayEnded()
        {
            playingService.ResetPlay();
            playControllerUI.TogglePlaySilently(false);

            PlayStateMachine.ChangeState<EditingLevelState>();
        }

        public override void OnExit()
        {
            playControllerUI.PlayToggledOff -= OnPlayToggledOff;
            playingService.PlayEnded -= OnPlayEnded;
            
            if (playingService.IsPlaying) {
                playingService.CancelPlay();
            }
        }

        private void OnPlayToggledOff()
        {
            PlayStateMachine.ChangeState<EditingLevelState>();
        }
    }
}
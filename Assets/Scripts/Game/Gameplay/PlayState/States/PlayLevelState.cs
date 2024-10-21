using Common.Level.Core;
using Core.Navigation;
using Game.Gameplay.Level;
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
        private readonly ILevelService levelService;
        private readonly PlayControllerUI playControllerUI;

        public PlayLevelState(PlayStateMachine playStateMachine, INavigationService navigationService,
            IPlayingService playingService, ILevelService levelService)
            : base(playStateMachine)
        {
            this.playingService = playingService;
            this.levelService = levelService;
            playControllerUI = navigationService.GetScreen<PlayLevelScreen>().PlayControllerUI;
        }

        public override void OnEnter()
        {
            playControllerUI.PlayToggledOff += OnPlayToggledOff;
            playControllerUI.ResetPressed += OnResetPressed;
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
            playControllerUI.ResetPressed -= OnResetPressed;
            playingService.PlayEnded -= OnPlayEnded;

            CancelPlay();
        }

        private void CancelPlay()
        {
            playControllerUI.TogglePlaySilently(false);
            if (playingService.IsPlaying) {
                playingService.CancelPlay();
            }
        }

        private void OnPlayToggledOff()
        {
            PlayStateMachine.ChangeState<EditingLevelState>();
        }

        private void OnResetPressed()
        {
            CancelPlay();
            levelService.ResetLevel();
            PlayStateMachine.ChangeState<EditingLevelState>();
        }
    }
}
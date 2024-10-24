using Core.Navigation;
using Cysharp.Threading.Tasks;
using Game.Gameplay.Level;
using Game.Gameplay.Playing;
using Game.Gameplay.PlayState.Core;
using Game.Gameplay.UI;

namespace Game.Gameplay.PlayState.States
{
    public class PlayLevelState : BasePlayState
    {
        private readonly IPlayRunningService playRunningService;
        private readonly ILevelService levelService;
        private readonly PlayControllerUI playControllerUI;

        public PlayLevelState(PlayStateMachine playStateMachine, IPlayUIProvider playUIProvider,
            IPlayRunningService playRunningService, ILevelService levelService) : base(playStateMachine)
        {
            this.playRunningService = playRunningService;
            this.levelService = levelService;
            playControllerUI = playUIProvider.PlayLevelScreen.PlayControllerUI;
        }

        public override void OnEnter()
        {
            playControllerUI.PlayToggledOff += OnPlayToggledOff;
            playControllerUI.ResetPressed += OnResetPressed;
            playRunningService.LevelFailed += OnLevelFailed;
            playRunningService.LevelPassed += OnLevelPassed;

            playRunningService.Play();
        }

        public override void OnExit()
        {
            playControllerUI.PlayToggledOff -= OnPlayToggledOff;
            playControllerUI.ResetPressed -= OnResetPressed;
            playRunningService.LevelFailed -= OnLevelFailed;
            playRunningService.LevelPassed -= OnLevelPassed;
        }

        private void CancelPlay()
        {
            playControllerUI.TogglePlaySilently(false);
            if (playRunningService.IsRunning) {
                playRunningService.CancelPlay();
            }
        }

        private void OnPlayToggledOff()
        {
            CancelPlay();
            PlayStateMachine.ChangeState<EditingLevelState>();
        }

        private void OnResetPressed()
        {
            CancelPlay();
            levelService.ResetLevel();
            PlayStateMachine.ChangeState<EditingLevelState>();
        }

        private void OnLevelFailed()
        {
            PlayStateMachine.ChangeState<LevelFailedState>();
        }

        private void OnLevelPassed()
        {
            PlayStateMachine.ChangeState<LevelPassedState>();
        }
    }
}
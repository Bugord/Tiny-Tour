using Core.Navigation;
using Cysharp.Threading.Tasks;
using Game.Common.Level.Core;
using Game.Gameplay.Core;
using Game.Gameplay.Level;
using Game.Gameplay.Playing;
using Game.Gameplay.PlayState.Core;
using Game.Gameplay.UI;
using Game.Main.Session.Core;
using Game.Main.UI.Popups;
using Game.Main.UI.Screens;
using Level;

namespace Game.Gameplay.PlayState.States
{
    public class PlayLevelState : BasePlayState
    {
        private readonly INavigationService navigationService;
        private readonly IPlayRunningService playRunningService;
        private readonly ILevelService levelService;
        private readonly LevelManager levelManager;
        private readonly ISessionManger sessionManger;
        private readonly IPlayService playService;
        private readonly PlayControllerUI playControllerUI;

        public PlayLevelState(PlayStateMachine playStateMachine, IPlayUIProvider playUIProvider,
            INavigationService navigationService, IPlayRunningService playRunningService, ILevelService levelService,
            LevelManager levelManager, ISessionManger sessionManger, IPlayService playService)
            : base(playStateMachine)
        {
            this.navigationService = navigationService;
            this.playRunningService = playRunningService;
            this.levelService = levelService;
            this.levelManager = levelManager;
            this.sessionManger = sessionManger;
            this.playService = playService;
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

            CancelPlay();
        }

        private void CancelPlay()
        {
            playControllerUI.TogglePlaySilently(false);
            if (playRunningService.IsPlaying) {
                playRunningService.CancelPlay();
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

        private void OnLevelFailed()
        {
            ResetLevelWithDelay().Forget();
        }

        private void OnLevelPassed()
        {
            ShowLevelPassedPopup().Forget();
        }

        private async UniTask ResetLevelWithDelay()
        {
            await UniTask.Delay(1000);

            playRunningService.ResetPlay();
            playControllerUI.TogglePlaySilently(false);

            PlayStateMachine.ChangeState<EditingLevelState>();
        }

        private async UniTask ShowLevelPassedPopup()
        {
            var levelPassedPopup = navigationService.PushPopup<LevelPassedPopup>();
            await levelPassedPopup.Task;

            navigationService.ClosePopup(levelPassedPopup);

            var finishedLevelData = sessionManger.CurrentSession.LevelData;
            sessionManger.EndSession();

            var nextLevelData = levelManager.GetNextLevel(finishedLevelData);
            if (nextLevelData == null) {
                playService.EndPlaying();
                return;
            }

            sessionManger.StartSession(nextLevelData);

            levelService.ClearLevel();
            levelService.LoadLevel(nextLevelData);
            PlayStateMachine.ChangeState<EditingLevelState>();
        }
    }
}
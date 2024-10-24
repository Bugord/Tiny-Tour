using Cysharp.Threading.Tasks;
using Game.Gameplay.Playing;
using Game.Gameplay.PlayState.Core;
using Game.Gameplay.UI;

namespace Game.Gameplay.PlayState.States
{
    public class LevelFailedState : BasePlayState
    {
        private readonly IPlayRunningService playRunningService;
        private readonly PlayControllerUI playControllerUI;

        public LevelFailedState(PlayStateMachine playStateMachine, IPlayRunningService playRunningService,
            IPlayUIProvider playUIProvider) : base(playStateMachine)
        {
            this.playRunningService = playRunningService;
            playControllerUI = playUIProvider.PlayLevelScreen.PlayControllerUI;
        }

        public override void OnEnter()
        {
            RestartLevelWithDelay().Forget();
        }

        private async UniTask RestartLevelWithDelay()
        {
            await UniTask.Delay(1000);

            playRunningService.ResetPlay();
            playControllerUI.TogglePlaySilently(false);

            PlayStateMachine.ChangeState<EditingLevelState>();
        }
    }
}
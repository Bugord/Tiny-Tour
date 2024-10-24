using Game.Gameplay.Core;
using Game.Gameplay.PlayState.Core;

namespace Game.Gameplay.PlayState.States
{
    public class LevelPassedState : BasePlayState
    {
        private readonly IPlayService playService;

        public LevelPassedState(PlayStateMachine playStateMachine, IPlayService playService) : base(playStateMachine)
        {
            this.playService = playService;
        }

        public override void OnEnter()
        {
            playService.OnLevelPassed();
        }
    }
}
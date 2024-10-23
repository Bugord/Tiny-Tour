using Game.Gameplay.PlayState.Core;

namespace Game.Gameplay.PlayState.States
{
    public class LoadLevelState : BasePlayState
    {
        public LoadLevelState(PlayStateMachine playStateMachine) : base(playStateMachine)
        {
        }

        public override void OnEnter()
        {
            PlayStateMachine.ChangeState<EditingLevelState>();
        }
    }
}
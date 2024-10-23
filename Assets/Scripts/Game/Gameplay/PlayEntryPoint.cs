using Game.Gameplay.PlayState.Core;
using Game.Gameplay.PlayState.States;
using Zenject;

namespace Game.Gameplay
{
    public class PlayEntryPoint : IInitializable 
    {
        private readonly PlayStateMachine playStateMachine;

        public PlayEntryPoint(PlayStateMachine playStateMachine)
        {
            this.playStateMachine = playStateMachine;
        }
        
        public void Initialize()
        {
            playStateMachine.ChangeState<LoadLevelState>();
        }
    }
}
using Gameplay.PlayState.Core;
using Gameplay.PlayState.States;
using Zenject;

namespace Gameplay
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
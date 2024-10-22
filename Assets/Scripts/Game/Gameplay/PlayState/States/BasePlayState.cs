using Game.Gameplay.PlayState.Core;

namespace Game.Gameplay.PlayState.States
{
    public abstract class BasePlayState
    {
        protected PlayStateMachine PlayStateMachine;
        
        protected BasePlayState(PlayStateMachine playStateMachine)
        {
            PlayStateMachine = playStateMachine;
        }
        
        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
        }
    }
}
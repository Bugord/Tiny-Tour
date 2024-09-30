using Core.GameState;

namespace States
{
    public abstract class BaseGameState
    {
        protected GameStateMachine GameStateMachine;
        
        protected BaseGameState(GameStateMachine gameStateMachine)
        {
            GameStateMachine = gameStateMachine;
        }
        
        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
        }
    }
}
namespace States
{
    public abstract class BaseGameState : IState
    {
        public abstract void OnEnter();
        public abstract void OnExit();
    }
}
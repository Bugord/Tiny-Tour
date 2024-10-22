using Application.GameState.Systems;
using Game.Main.GameState.States;

namespace Game.Project.GameState.Systems
{
    public class GameStateMachine
    {
        private readonly IGameStateFactory gameStateFactory;

        private BaseGameState currentState;

        public GameStateMachine(IGameStateFactory gameStateFactory)
        {
            this.gameStateFactory = gameStateFactory;
        }

        public void ChangeState<T>() where T : BaseGameState
        {
            currentState?.OnExit();

            var newState = gameStateFactory.Create<T>(this);
            currentState = newState;
            currentState?.OnEnter();
        }
    }
}
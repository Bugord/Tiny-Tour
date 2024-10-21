using States;

namespace Application.GameState.Systems
{
    public interface IGameStateFactory
    {
        T Create<T>(GameStateMachine gameStateMachine) where T : BaseGameState;
    }
}
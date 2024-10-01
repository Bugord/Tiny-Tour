using States;

namespace Core.GameState
{
    public interface IGameStateFactory
    {
        T Create<T>(GameStateMachine gameStateMachine) where T : BaseGameState;
    }
}
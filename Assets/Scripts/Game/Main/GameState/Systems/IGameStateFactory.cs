using Game.Main.GameState.States;
using Game.Project.GameState.Systems;

namespace Application.GameState.Systems
{
    public interface IGameStateFactory
    {
        T Create<T>(GameStateMachine gameStateMachine) where T : BaseGameState;
    }
}
using Game.Gameplay.PlayState.States;

namespace Game.Gameplay.PlayState.Core
{
    public interface IPlayStateFactory
    {
        T Create<T>(PlayStateMachine playStateMachine) where T : BasePlayState;
    }
}
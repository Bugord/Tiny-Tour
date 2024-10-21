using Game.Gameplay.PlayState.States;
using Gameplay.PlayState.Core;

namespace Gameplay.PlayState
{
    public interface IPlayStateFactory
    {
        T Create<T>(PlayStateMachine playStateMachine) where T : BasePlayState;
    }
}
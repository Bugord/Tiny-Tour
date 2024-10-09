using Gameplay.PlayState.Core;
using Gameplay.PlayState.States;

namespace Gameplay.PlayState
{
    public interface IPlayStateFactory
    {
        T Create<T>(PlayStateMachine playStateMachine) where T : BasePlayState;
    }
}
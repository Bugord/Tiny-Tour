using Core.GameState;
using Core.GameState.States;
using UnityEngine;
using Zenject;

namespace Core
{
    public class ApplicationEntryPoint : IInitializable
    {
        private readonly GameStateMachine gameStateMachine;

        public ApplicationEntryPoint(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
        }

        public void Initialize()
        {
            Application.targetFrameRate = 60;
            gameStateMachine.ChangeState<MainMenuState>();
        }
    }
}
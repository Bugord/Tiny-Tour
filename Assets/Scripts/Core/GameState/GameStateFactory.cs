﻿using States;
using Zenject;

namespace Core.GameState
{
    public class GameStateFactory : IGameStateFactory
    {
        private readonly DiContainer diContainer;

        public GameStateFactory(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        public T Create<T>(GameStateMachine gameStateMachine) where T : BaseGameState
        {
            return diContainer.Instantiate<T>(new[] { gameStateMachine });
        }
    }
}
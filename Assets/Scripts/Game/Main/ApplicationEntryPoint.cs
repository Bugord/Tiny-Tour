﻿using Application.GameState.Systems;
using Core.GameState.States;
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
            UnityEngine.Application.targetFrameRate = 60;
            gameStateMachine.ChangeState<MainMenuState>();
        }
    }
}
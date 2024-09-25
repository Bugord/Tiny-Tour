using System;
using System.Collections.Generic;
using Core.GameState.States;
using States;
using UnityEngine;

namespace Core.GameState
{
    public class GameStateMachine
    {
        private readonly IGameStateFactory gameStateFactory;
        private readonly Dictionary<Type, BaseGameState> states;

        private BaseGameState currentState;

        public GameStateMachine(IGameStateFactory gameStateFactory)
        {
            this.gameStateFactory = gameStateFactory;

            states = new Dictionary<Type, BaseGameState>();
            
            RegisterState<MainMenuState>();
            RegisterState<SelectLevelToPlayState>();
            RegisterState<SelectLevelToEditState>();
            RegisterState<PlayLevelState>();
            RegisterState<TestPlayLevelState>();
            RegisterState<EditLevelState>();
        }

        private void RegisterState<T>() where T : BaseGameState
        {
            if (states.ContainsKey(typeof(T))) {
                Debug.LogWarning($"Game state {typeof(T)} was already registered");
                return;
            }

            var state = gameStateFactory.Create<T>(this);
            states.Add(typeof(T), state);
        }

        public void ChangeState<T>() where T : BaseGameState
        {
            if (!states.ContainsKey(typeof(T))) {
                Debug.LogError($"State {typeof(T)} is not registered");
                return;
            }
            
            currentState?.OnExit();
            currentState = states[typeof(T)];
            currentState?.OnEnter();
        }
    }
}
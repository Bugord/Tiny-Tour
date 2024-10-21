using System;
using System.Collections.Generic;
using Game.Gameplay.PlayState.States;
using UnityEngine;

namespace Gameplay.PlayState.Core
{
    public class PlayStateMachine
    {
        private readonly IPlayStateFactory playStateFactory;
        private readonly Dictionary<Type, BasePlayState> states;

        private BasePlayState currentState;

        public PlayStateMachine(IPlayStateFactory playStateFactory)
        {
            this.playStateFactory = playStateFactory;
            states = new Dictionary<Type, BasePlayState>();
            
            RegisterState<LoadLevelState>();
            RegisterState<EditingLevelState>();
            RegisterState<PlayLevelState>();
        }

        private void RegisterState<T>() where T : BasePlayState
        {
            if (states.ContainsKey(typeof(T))) {
                Debug.LogWarning($"Play state {typeof(T)} was already registered");
                return;
            }

            var state = playStateFactory.Create<T>(this);
            states.Add(typeof(T), state);
        }

        public void ChangeState<T>() where T : BasePlayState
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
using System;
using System.Collections.Generic;
using Game.Gameplay.PlayState.States;
using UnityEngine;

namespace Game.Gameplay.PlayState.Core
{
    public class PlayStateMachine
    {
        private readonly IPlayStateFactory playStateFactory;

        private BasePlayState currentState;

        public PlayStateMachine(IPlayStateFactory playStateFactory)
        {
            this.playStateFactory = playStateFactory;
        }

        public void ChangeState<T>() where T : BasePlayState
        {
            currentState?.OnExit();

            var newState = playStateFactory.Create<T>(this);
            currentState = newState;
            currentState?.OnEnter();
        }
    }
}
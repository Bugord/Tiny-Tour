﻿using Gameplay.PlayState.Core;
using Gameplay.PlayState.States;
using Zenject;

namespace Gameplay.PlayState
{
    public class PlayStateFactory : IPlayStateFactory
    {
        private readonly DiContainer diContainer;

        public PlayStateFactory(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        public T Create<T>(PlayStateMachine playStateMachine) where T : BasePlayState
        {
            return diContainer.Instantiate<T>(new[] { playStateMachine });
        }
    }
}
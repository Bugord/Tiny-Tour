using System;
using System.Collections.Generic;
using Core.Logging;
using Core.Navigation.Core;
using UI.Screens;
using UnityEngine;
using Zenject;

namespace Core.Navigation
{
    public class ScreenFactory : IScreenFactory
    {
        private readonly DiContainer diContainer;
        private readonly ILogger<ScreenFactory> logger;
        private readonly NavigationConfig navigationConfig;

        private Dictionary<Type, BaseScreen> cachedScreenPrefabsDictionary;

        public ScreenFactory(DiContainer diContainer, ILogger<ScreenFactory> logger, NavigationConfig navigationConfig)
        {
            this.diContainer = diContainer;
            this.logger = logger;
            this.navigationConfig = navigationConfig;

            ConfigurePrefabs();
        }

        private void ConfigurePrefabs()
        {
            cachedScreenPrefabsDictionary = new Dictionary<Type, BaseScreen>();

            foreach (var screenPrefab in navigationConfig.screenPrefabs) {
                var screenType = screenPrefab.GetType();
                
                if (cachedScreenPrefabsDictionary.ContainsKey(screenType)) {
                    logger.LogWarning($"Screen with type {screenType} was already added");
                    continue;
                }
                
                cachedScreenPrefabsDictionary.Add(screenType, screenPrefab);
            }
        }

        public T Create<T>(Transform parentTransform) where T : BaseScreen
        {
            var screenType = typeof(T);

            if (!cachedScreenPrefabsDictionary.ContainsKey(screenType)) {
                logger.LogError($"Screen with type {screenType} is not registered");
                return null;
            }

            var screenPrefab = cachedScreenPrefabsDictionary[screenType];
            var screen = diContainer.InstantiatePrefabForComponent<T>(screenPrefab, parentTransform);

            return screen;
        }
    }
}
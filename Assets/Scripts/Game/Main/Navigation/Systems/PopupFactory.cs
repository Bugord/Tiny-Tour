using System;
using System.Collections.Generic;
using Core.Logging;
using Core.Navigation;
using UnityEngine;
using Zenject;

namespace Game.Navigation.Systems
{
    public class PopupFactory : IPopupFactory
    {
        private readonly DiContainer diContainer;
        private readonly ILogger<PopupFactory> logger;
        private readonly NavigationConfig navigationConfig;

        private Dictionary<Type, BasePopup> cachedPopupsPrefabsDictionary;

        public PopupFactory(DiContainer diContainer, ILogger<PopupFactory> logger, NavigationConfig navigationConfig)
        {
            this.diContainer = diContainer;
            this.logger = logger;
            this.navigationConfig = navigationConfig;

            ConfigurePrefabs();
        }

        private void ConfigurePrefabs()
        {
            cachedPopupsPrefabsDictionary = new Dictionary<Type, BasePopup>();

            foreach (var popupPrefab in navigationConfig.popupPrefabs) {
                var popupType = popupPrefab.GetType();
                
                if (cachedPopupsPrefabsDictionary.ContainsKey(popupType)) {
                    logger.LogWarning($"Popup with type {popupType} was already added");
                    continue;
                }
                
                cachedPopupsPrefabsDictionary.Add(popupType, popupPrefab);
            }
        }

        public T Create<T>(Transform parentTransform) where T : BasePopup
        {
            var popupType = typeof(T);

            if (!cachedPopupsPrefabsDictionary.ContainsKey(popupType)) {
                logger.LogError($"Popup with type {popupType} is not registered");
                return null;
            }

            var popupPrefab = cachedPopupsPrefabsDictionary[popupType];
            var popup = diContainer.InstantiatePrefabForComponent<T>(popupPrefab, parentTransform);

            return popup;
        }
    }
}
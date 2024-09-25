using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.UI
{
    public class UIProvider
    {
        public readonly Canvas Canvas;
        public readonly EventSystem EventSystem;
        
        public UIProvider(UIProviderConfig uiProviderConfig)
        {
            Canvas = Object.Instantiate(uiProviderConfig.CanvasPrefab);
            EventSystem = Object.Instantiate(uiProviderConfig.EventSystemPrefab);
            
            Object.DontDestroyOnLoad(Canvas);
            Object.DontDestroyOnLoad(EventSystem);
        }
    }
}
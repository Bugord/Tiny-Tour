using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{
    [CreateAssetMenu(fileName = "config_ui_provider", menuName = "Config/UI Provider")]
    public class UIProviderConfig : ScriptableObject
    {
        [field: SerializeField]
        public Canvas CanvasPrefab { get; private set; }      
        
        [field: SerializeField]
        public EventSystem EventSystemPrefab { get; private set; }
    }
}
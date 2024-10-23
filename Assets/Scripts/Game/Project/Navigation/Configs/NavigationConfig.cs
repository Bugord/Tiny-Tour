using System.Collections.Generic;
using UnityEngine;

namespace Core.Navigation
{
    [CreateAssetMenu(fileName = "config_navigation", menuName = "Config/Navigation")]
    public class NavigationConfig : ScriptableObject
    {
        public List<BaseScreen> screenPrefabs;
        public List<BasePopup> popupPrefabs;
    }
}
using System.Collections.Generic;
using Core.Navigation.Core;
using UI.Screens;
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
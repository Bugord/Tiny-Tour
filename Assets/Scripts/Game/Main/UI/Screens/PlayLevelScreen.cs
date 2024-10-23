using System;
using Core.Navigation;
using Game.Common.UI;
using Game.Gameplay.UI;
using UnityEngine;

namespace Game.Main.UI.Screens
{
    public class PlayLevelScreen : BaseScreen
    {
        public event Action BackPressed;
        
        [field: SerializeField]
        public EditorOptionsControllerUI EditorOptionsControllerUI { get; private set; }

        [field: SerializeField]
        public PlayControllerUI PlayControllerUI { get; private set; }

        public void OnBackButtonPressed()
        {
            BackPressed?.Invoke();
        }
    }
}
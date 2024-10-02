using System;
using Gameplay.UI;
using UnityEngine;

namespace UI.Screens
{
    public class PlayLevelScreen : BaseScreen
    {
        public event Action BackPressed;
        
        [field: SerializeField]
        public InGameEditorUI InGameEditorUI { get; private set; }

        [field: SerializeField]
        public PlayControllerUI PlayControllerUI { get; private set; }

        public void OnBackButtonPressed()
        {
            BackPressed?.Invoke();
        }
    }
}
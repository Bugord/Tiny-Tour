using System;
using Gameplay.UI;
using UnityEngine;

namespace UI.Screens
{
    public class PlayLevelScreen : BaseScreen
    {
        [field: SerializeField]
        public InGameEditorUI InGameEditorUI { get; private set; }
        
        public event Action BackPressed;
       
        public void OnBackButtonPressed()
        {
            BackPressed?.Invoke();
        }
    }
}
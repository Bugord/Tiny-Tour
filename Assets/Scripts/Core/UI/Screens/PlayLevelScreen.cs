using System;
using Common.UI;
using Core.Navigation.Core;
using Gameplay.UI;
using UnityEngine;

namespace UI.Screens
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
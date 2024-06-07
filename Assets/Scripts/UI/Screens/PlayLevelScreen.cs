using System;
using Tiles.Editing;
using UnityEngine;

namespace UI.Screens
{
    public class PlayLevelScreen : BaseScreen
    {
        public event Action BackPressed;
        public event Action PlayPressed;
        public event Action ResetPressed;
        public event Action PreviousLevelPressed;
        public event Action NextLevelPressed;

        [SerializeField]
        private TilemapEditorUI tilemapEditorUI;

        public TilemapEditorUI TilemapEditorUI => tilemapEditorUI;
        
        public void OnBackButtonPressed()
        {
            BackPressed?.Invoke();
        }      
        
        public void OnPlayButtonPressed()
        {
            PlayPressed?.Invoke();
        }      
        
        public void OnResetButtonPressed()
        {
            ResetPressed?.Invoke();
        }
        
        public void OnPreviousLevelButtonPressed()
        {
            PreviousLevelPressed?.Invoke();
        }
        
        public void OnNextLevelButtonPressed()
        {
            NextLevelPressed?.Invoke();
        }
    }
}
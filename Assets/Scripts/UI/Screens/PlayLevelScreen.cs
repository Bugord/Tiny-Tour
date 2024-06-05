using System;
using Tiles.Editing;
using UnityEngine;

namespace UI.Screens
{
    public class PlayLevelScreen : BaseScreen
    {
        public event Action BackPressed;
        public event Action PlayPressed;

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
    }
}
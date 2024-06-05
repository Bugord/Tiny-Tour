using System;
using Tiles.Editing;
using UnityEngine;

namespace UI.Screens
{
    public class EditLevelScreen : BaseScreen
    {
        public event Action BackPressed;
        public event Action SavePressed;
        public event Action PlayPressed;

        [SerializeField]
        private TilemapEditorUI tilemapEditorUI;

        public TilemapEditorUI TilemapEditorUI => tilemapEditorUI;
        
        public void OnBackButtonPressed()
        {
            BackPressed?.Invoke();
        }

        public void OnSaveButtonPressed()
        {
            SavePressed?.Invoke();
        }   
        
        public void OnPlayButtonPressed()
        {
            PlayPressed?.Invoke();
        }
    }
}
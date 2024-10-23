using System;
using Core.Navigation;
using Game.Common.Level.Data;
using UI;
using UnityEngine;

namespace Game.Main.UI.Screens
{
    public class EditLevelSelectScreen : BaseScreen
    {
        public event Action<int> LevelSelected;
        public event Action BackPressed; 
        public event Action CreateNewPressed; 

        [SerializeField]
        private LevelsGridView levelsGridView;

        private void Awake()
        {
            levelsGridView.LevelButtonPressed += OnLevelButtonPressed;
        }

        private void OnDestroy()
        {
            levelsGridView.LevelButtonPressed -= OnLevelButtonPressed;
        }

        public void SetLevels(LevelData[] levelsData)
        {
            levelsGridView.SetLevels(levelsData);
        }

        private void OnLevelButtonPressed(int levelIndex)
        {
            LevelSelected?.Invoke(levelIndex);
        }
        
        public void OnBackButtonPressed()
        {
            BackPressed?.Invoke();
        }
        
        public void OnCreateNewButtonPressed()
        {
            CreateNewPressed?.Invoke();
        }
    }
}
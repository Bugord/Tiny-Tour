using System;
using Level.Data;
using UnityEngine;

namespace UI.Screens
{
    public class PlayLevelScreen : BaseScreen
    {
        public event Action<int> LevelSelected;
        public event Action BackPressed; 

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
    }
}
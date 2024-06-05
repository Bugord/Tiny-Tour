using System;
using Level.Data;
using UnityEngine;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private LevelLibrary levelLibrary;

        private ILevelProvider levelProvider;

        private int selectedLevelIndex;

        private void Awake()
        {
            levelProvider = levelLibrary;
        }

        public void LoadLevels()
        {
            levelProvider.LoadAllLevels();
        }
        
        public LevelData[] GetLevels()
        {
            return levelProvider.GetCachedLevels();
        }

        public void SelectLevel(int levelIndex)
        {
            selectedLevelIndex = levelIndex;
        }

        public LevelData GetSelectedLevel()
        {
            return levelProvider.GetLevelByIndex(selectedLevelIndex);
        }

        public void SaveLevel(LevelData levelData)
        {
            levelProvider.SaveLevel(levelData);
        }
    }
}
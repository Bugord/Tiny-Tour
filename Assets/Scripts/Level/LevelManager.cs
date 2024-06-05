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

        private LevelData selectedLevelData;

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
            selectedLevelData = levelProvider.GetCachedLevels()[levelIndex];
        }
        
        public void SelectLevel(LevelData levelData)
        {
            selectedLevelData = levelData;
        }

        public LevelData GetSelectedLevel()
        {
            return selectedLevelData;
        }

        public void SaveLevel(LevelData levelData)
        {
            levelProvider.SaveLevel(levelData);
        }
    }
}
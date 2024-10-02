using System;
using Cysharp.Threading.Tasks;
using Level.Data;

namespace Level
{
    public class LevelManager
    {
        private readonly ILevelProvider levelProvider;
        private LevelData selectedLevelData;

        public LevelManager(ILevelProvider levelProvider)
        {
            this.levelProvider = levelProvider;
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

        public LevelData CreateNewLevel(string levelName)
        {
            return levelProvider.CreateNewLevel(levelName);
        }

        public LevelData GetNextLevel()
        {
            var levels = levelProvider.GetCachedLevels();
            var currentIndex = Array.IndexOf(levels, selectedLevelData);
            if (currentIndex == -1) {
                return null;
            }

            if (currentIndex == levels.Length - 1) {
                currentIndex = -1;
            }

            return levelProvider.GetLevelByIndex(currentIndex + 1);
        }

        public LevelData GetPreviousLevel()
        {
            var levels = levelProvider.GetCachedLevels();
            var currentIndex = Array.IndexOf(levels, selectedLevelData);
            if (currentIndex == -1) {
                return null;
            }

            if (currentIndex == 0) {
                currentIndex = levels.Length;
            }

            return levelProvider.GetLevelByIndex(currentIndex - 1);
        }
    }
}
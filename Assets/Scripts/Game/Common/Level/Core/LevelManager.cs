﻿using System;
using Cysharp.Threading.Tasks;
using Game.Common.Level.Data;
using Zenject;

namespace Level
{
    public class LevelManager : IInitializable
    {
        private readonly ILevelProvider levelProvider;

        public void Initialize()
        {
            levelProvider.LoadLevels();
        }

        public LevelManager(ILevelProvider levelProvider)
        {
            this.levelProvider = levelProvider;
        }

        public LevelData[] GetLevels()
        {
            return levelProvider.GetLevels();
        }

        public LevelData GetLevelByIndex(int levelIndex)
        {
            return levelProvider.GetLevels()[levelIndex];
        }

        public void SaveLevel(LevelData levelData)
        {
            levelProvider.SaveLevel(levelData);
            levelProvider.LoadLevels();
        }

        public LevelData CreateNewLevel(string levelName)
        {
            return levelProvider.CreateNewLevel(levelName);
        }

        // public LevelData GetNextLevel()
        // {
        //     var levels = levelProvider.GetLevels();
        //     var currentIndex = Array.IndexOf(levels, selectedLevelData);
        //     if (currentIndex == -1) {
        //         return null;
        //     }
        //
        //     if (currentIndex == levels.Length - 1) {
        //         currentIndex = -1;
        //     }
        //
        //     return levelProvider.GetLevelByIndex(currentIndex + 1);
        // }
        //
        // public LevelData GetPreviousLevel()
        // {
        //     var levels = levelProvider.GetLevels();
        //     var currentIndex = Array.IndexOf(levels, selectedLevelData);
        //     if (currentIndex == -1) {
        //         return null;
        //     }
        //
        //     if (currentIndex == 0) {
        //         currentIndex = levels.Length;
        //     }
        //
        //     return levelProvider.GetLevelByIndex(currentIndex - 1);
        // }
    }
}
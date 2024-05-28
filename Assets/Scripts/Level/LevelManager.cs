using System;
using System.Linq;
using Tiles;
using UnityEngine;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private TilemapDataEditor tilemapDataEditor;

        [SerializeField]
        private LevelLibrary levelLibrary;
        
        private ILevelLoader levelLoader;
        private ILevelSaver levelSaver;
        private ILevelProvider levelProvider;

        private void Awake()
        {
            levelLoader = tilemapDataEditor;
            levelSaver = tilemapDataEditor;
            levelProvider = levelLibrary;
        }

        private void Start()
        {
            LoadLevels();

            var levels = levelProvider.GetAllLevels();
            if (levels.Any()) {
                var levelToLoad = levels[0];
                LoadLevel(levelToLoad);
            }
        }

        [ContextMenu("Save")]
        public void SaveLevel()
        {
            var levelData = levelSaver.SaveLevel();
            levelProvider.SaveLevel(levelData);
        }

        private void LoadLevels()
        {
            levelProvider.LoadAllLevels();
        }

        private void LoadLevel(LevelData levelData)
        {
            levelLoader.LoadLevel(levelData);
        }
    }
}
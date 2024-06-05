using System.Linq;
using Core;
using Level.Data;
using Tiles;
using Tiles.Editing;
using Tiles.Editing.Workshop;
using UnityEngine;
using UnityEngine.Serialization;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField]
        private WorkshopTilemapEditor workshopTilemapEditor;

        [SerializeField]
        private GameSession gameSession;

        [SerializeField]
        private LevelLibrary levelLibrary;

        private ILevelProvider levelProvider;
        
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

        private void OnLoadInGamePressed(int levelId)
        {
            gameSession.gameObject.SetActive(true);
            gameSession.LoadLevel(levelProvider.GetLevelByIndex(levelId));
        }

        private void OnLoadWorkshopPressed(int levelId)
        {
            workshopTilemapEditor.gameObject.SetActive(true);
            workshopTilemapEditor.Setup();
            workshopTilemapEditor.LoadLevel(levelProvider.GetLevelByIndex(levelId));
        }

        [ContextMenu("Save")]
        public void SaveLevel()
        {
            var levelData = workshopTilemapEditor.SaveLevel();
            levelProvider.SaveLevel(levelData);
        }

        private void OnSavePressed()
        {
            var levelData = workshopTilemapEditor.SaveLevel();
            levelProvider.SaveLevel(levelData);

            levelProvider.LoadAllLevels();
            var levelsData = levelProvider.GetCachedLevels();

            var savedLevel = levelProvider.GetLevelByName(levelData.levelName);
        }

        private void OnSaveAsPressed(string levelName)
        {
            var levelData = workshopTilemapEditor.SaveLevel();
            levelData.levelName = levelName;
            levelProvider.SaveLevel(levelData);

            levelProvider.LoadAllLevels();
            var levelsData = levelProvider.GetCachedLevels();

            var savedLevel = levelProvider.GetLevelByName(levelName);
        }
    }
}
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
        private LevelManagerUI levelManagerUI;

        [SerializeField]
        private LevelLibrary levelLibrary;

        private ILevelProvider levelProvider;

        private GameState currentState;

        private void Awake()
        {
            currentState = GameState.None;
            levelProvider = levelLibrary;

            levelManagerUI.SavePressed += OnSavePressed;
            levelManagerUI.SaveAsPressed += OnSaveAsPressed;
            levelManagerUI.LoadWorkshopPressed += OnLoadWorkshopPressed;
            levelManagerUI.LoadInGamePressed += OnLoadInGamePressed;
        }

        private void OnDestroy()
        {
            levelManagerUI.SavePressed -= OnSavePressed;
            levelManagerUI.SaveAsPressed -= OnSaveAsPressed;
            levelManagerUI.LoadWorkshopPressed -= OnLoadWorkshopPressed;
            levelManagerUI.LoadInGamePressed -= OnLoadInGamePressed;
        }

        private void Start()
        {
            LoadLevels();
            // workshopTilemapEditor.Setup();

            var levelsData = levelProvider.GetCachedLevels();
            if (levelsData.Any()) {
                levelManagerUI.SetData(levelsData);
            }
        }

        private void OnLoadInGamePressed(int levelId)
        {
            gameSession.gameObject.SetActive(true);
            gameSession.LoadLevel(levelProvider.GetLevelByIndex(levelId));
            currentState = GameState.InGame;
        }

        private void OnLoadWorkshopPressed(int levelId)
        {
            workshopTilemapEditor.gameObject.SetActive(true);
            workshopTilemapEditor.Setup();
            workshopTilemapEditor.LoadLevel(levelProvider.GetLevelByIndex(levelId));
            currentState = GameState.Workshop;
        }

        [ContextMenu("Save")]
        public void SaveLevel()
        {
            var levelData = workshopTilemapEditor.SaveLevel();
            levelProvider.SaveLevel(levelData);
        }

        private void LoadLevels()
        {
            levelProvider.LoadAllLevels();
        }

        private void OnSavePressed()
        {
            var levelData = workshopTilemapEditor.SaveLevel();
            levelProvider.SaveLevel(levelData);

            levelProvider.LoadAllLevels();
            var levelsData = levelProvider.GetCachedLevels();
            levelManagerUI.SetData(levelsData);

            var savedLevel = levelProvider.GetLevelByName(levelData.levelName);
            levelManagerUI.SetSelectedLevel(levelsData.IndexOf(savedLevel));
        }

        private void OnSaveAsPressed(string levelName)
        {
            var levelData = workshopTilemapEditor.SaveLevel();
            levelData.levelName = levelName;
            levelProvider.SaveLevel(levelData);

            levelProvider.LoadAllLevels();
            var levelsData = levelProvider.GetCachedLevels();
            levelManagerUI.SetData(levelsData);

            var savedLevel = levelProvider.GetLevelByName(levelName);
            levelManagerUI.SetSelectedLevel(levelsData.IndexOf(savedLevel));
        }
    }
}
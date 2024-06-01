using System.Linq;
using Tiles;
using UnityEngine;
using UnityEngine.Serialization;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        [FormerlySerializedAs("tilemapDataEditor")]
        [SerializeField]
        private TilemapSaveLoader tilemapSaveLoader;

        [SerializeField]
        private LevelManagerUI levelManagerUI;

        [SerializeField]
        private LevelLibrary levelLibrary;
        
        private ILevelLoader levelLoader;
        private ILevelSaver levelSaver;
        private ILevelProvider levelProvider;
        
        private void Awake()
        {
            levelLoader = tilemapSaveLoader;
            levelSaver = tilemapSaveLoader;
            levelProvider = levelLibrary;
            
            levelManagerUI.LevelSelected += OnLevelSelected;
            levelManagerUI.SavePressed += OnSavePressed;
            levelManagerUI.SaveAsPressed += OnSaveAsPressed;
        }
        
        private void OnDestroy()
        {
            levelManagerUI.LevelSelected -= OnLevelSelected;
            levelManagerUI.SavePressed -= OnSavePressed;
            levelManagerUI.SaveAsPressed -= OnSaveAsPressed;
        }

        private void Start()
        {
            LoadLevels();

            var levelsData = levelProvider.GetCachedLevels();
            if (levelsData.Any()) {
                levelManagerUI.SetData(levelsData);
                OnLevelSelected(0);
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

        private void OnLevelSelected(int levelId)
        {
            var levelToLoad = levelProvider.GetLevelByIndex(levelId);
            LoadLevel(levelToLoad);
        }

        private void LoadLevel(LevelData levelData)
        {
            levelLoader.LoadLevel(levelData);
        }

        private void OnSavePressed()
        {
            var levelData = levelSaver.SaveLevel();
            levelProvider.SaveLevel(levelData);
            
            levelProvider.LoadAllLevels();
            var levelsData = levelProvider.GetCachedLevels();
            levelManagerUI.SetData(levelsData);
            
            var savedLevel = levelProvider.GetLevelByName(levelData.levelName);
            levelManagerUI.SetSelectedLevel(levelsData.IndexOf(savedLevel));
        }

        private void OnSaveAsPressed(string levelName)
        {
            var levelData = levelSaver.SaveLevel();
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
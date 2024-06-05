using System.Collections;
using Level;
using Tiles.Editing.Workshop;
using UI;
using UI.Screens;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace States
{
    public class EditLevelState : BaseGameState
    {
        private readonly NavigationSystem navigationSystem;
        private readonly GameStateSystem gameStateSystem;
        private readonly LevelManager levelManager;

        private WorkshopTilemapEditor workshopTilemapEditor;
        private EditLevelScreen editLevelScreen;
        
        private const string WorkshoptilemapeditorTag = "WorkshopTilemapEditor";
        private const string EditLevelScene = "EditLevelScene";

        public EditLevelState(GameStateSystem gameStateSystem, NavigationSystem navigationSystem,
            LevelManager levelManager)
        {
            this.gameStateSystem = gameStateSystem;
            this.navigationSystem = navigationSystem;
            this.levelManager = levelManager;
        }

        public override void OnEnter()
        {
            editLevelScreen = navigationSystem.Push<EditLevelScreen>();
            editLevelScreen.BackPressed += OnBackPressed;
            editLevelScreen.SavePressed += OnSavePressed;
            editLevelScreen.PlayPressed += OnPlayPressed;

            gameStateSystem.StartCoroutine(LoadEditor());
        }

        private IEnumerator LoadEditor()
        {
            SceneManager.LoadScene(EditLevelScene, LoadSceneMode.Additive);
            yield return null;
            var selectedLevel = levelManager.GetSelectedLevel();
            workshopTilemapEditor = GameObject.FindGameObjectWithTag(WorkshoptilemapeditorTag)
                ?.GetComponent<WorkshopTilemapEditor>();

            if (workshopTilemapEditor == null) {
                SceneManager.UnloadSceneAsync(EditLevelScene);
                Debug.LogError($"Could not find {nameof(WorkshopTilemapEditor)}");
                gameStateSystem.ChangeState(gameStateSystem.SelectLevelToEditState);
                yield break;
            }
            
            workshopTilemapEditor.Setup(editLevelScreen.TilemapEditorUI);
            workshopTilemapEditor.LoadLevel(selectedLevel);
        }

        public override void OnExit()
        {
            SceneManager.UnloadSceneAsync(EditLevelScene);

            editLevelScreen.BackPressed -= OnBackPressed;
            editLevelScreen.SavePressed -= OnSavePressed;
            editLevelScreen.PlayPressed -= OnPlayPressed;
            editLevelScreen.Close();
        }

        private void OnSavePressed()
        {
            var levelData = workshopTilemapEditor.SaveLevel();
            levelManager.SaveLevel(levelData);
        }

        private void OnBackPressed()
        {
            gameStateSystem.ChangeState(gameStateSystem.SelectLevelToEditState);
        }

        private void OnPlayPressed()
        {
            levelManager.SelectLevel(workshopTilemapEditor.SaveLevel());
            gameStateSystem.ChangeState(gameStateSystem.TestPlayLevelState);
        }
    }
}
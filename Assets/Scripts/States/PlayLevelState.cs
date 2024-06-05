using System.Collections;
using Core;
using Level;
using UI;
using UI.Screens;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace States
{
    public class PlayLevelState : BaseGameState
    {
        private readonly NavigationSystem navigationSystem;
        private readonly GameStateSystem gameStateSystem;
        private readonly LevelManager levelManager;

        private GameSession gameSession;
        private PlayLevelScreen playLevelScreen;
        
        private const string GameSessionTag = "GameSession";
        private const string PlayLevelScene = "PlayLevelScene";

        public PlayLevelState(GameStateSystem gameStateSystem, NavigationSystem navigationSystem,
            LevelManager levelManager)
        {
            this.gameStateSystem = gameStateSystem;
            this.navigationSystem = navigationSystem;
            this.levelManager = levelManager;
        }

        public override void OnEnter()
        {
            playLevelScreen = navigationSystem.Push<PlayLevelScreen>();
            playLevelScreen.BackPressed += OnBackPressed;
            playLevelScreen.PlayPressed += OnPlayPressed;

            gameStateSystem.StartCoroutine(LoadEditor());
        }

        private IEnumerator LoadEditor()
        {
            SceneManager.LoadScene(PlayLevelScene, LoadSceneMode.Additive);
            yield return null;
            gameSession = GameObject.FindGameObjectWithTag(GameSessionTag)
                ?.GetComponent<GameSession>();

            if (gameSession == null) {
                SceneManager.UnloadSceneAsync(PlayLevelScene);
                Debug.LogError($"Could not find {nameof(GameSession)}");
                gameStateSystem.ChangeState(gameStateSystem.SelectLevelToPlayState);
                yield break;
            }
            
            var selectedLevel = levelManager.GetSelectedLevel();
            gameSession.SetupEditor(playLevelScreen.TilemapEditorUI);
            gameSession.LoadLevel(selectedLevel);
        }

        public override void OnExit()
        {
            SceneManager.UnloadSceneAsync(PlayLevelScene);

            playLevelScreen.BackPressed -= OnBackPressed;
            playLevelScreen.PlayPressed -= OnPlayPressed;
            playLevelScreen.Close();
        }

        private void OnPlayPressed()
        {
            gameSession.Play();
        }

        private void OnBackPressed()
        {
            gameStateSystem.ChangeState(gameStateSystem.SelectLevelToEditState);
        }
    }
}
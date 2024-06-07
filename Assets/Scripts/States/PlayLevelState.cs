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
            playLevelScreen.ResetPressed += OnResetPressed;
            playLevelScreen.PreviousLevelPressed += OnPreviousLevelPressed;
            playLevelScreen.NextLevelPressed += OnNextLevelPressed;

            gameStateSystem.StartCoroutine(LoadEditor());
        }

        public override void OnExit()
        {
            SceneManager.UnloadSceneAsync(PlayLevelScene);

            playLevelScreen.BackPressed -= OnBackPressed;
            playLevelScreen.PlayPressed -= OnPlayPressed;
            playLevelScreen.ResetPressed -= OnResetPressed;
            playLevelScreen.PreviousLevelPressed -= OnPreviousLevelPressed;
            playLevelScreen.NextLevelPressed -= OnNextLevelPressed;
            playLevelScreen.Close();
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

        private void OnPlayPressed()
        {
            gameSession.Play();
        }

        private void OnBackPressed()
        {
            gameStateSystem.ChangeState(gameStateSystem.SelectLevelToPlayState);
        }

        private void OnResetPressed()
        {
            gameSession.ResetCars();
        }

        private void OnPreviousLevelPressed()
        {
            var previousLevel = levelManager.GetPreviousLevel();
            if (previousLevel == null) {
                return;
            }
            levelManager.SelectLevel(previousLevel);
            gameSession.CloseLevel();
            gameSession.LoadLevel(previousLevel);
        }

        private void OnNextLevelPressed()
        {
            var nextLevel = levelManager.GetNextLevel();
            if (nextLevel == null) {
                return;
            }
            levelManager.SelectLevel(nextLevel);
            gameSession.CloseLevel();
            gameSession.LoadLevel(nextLevel);
        }
    }
}
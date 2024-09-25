using Core.Navigation;
using Cysharp.Threading.Tasks;
using Level;
using States;
using Tiles.Editing.Workshop;
using UI;
using UI.Screens;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.GameState.States
{
    public class EditLevelState : BaseGameState
    {
        private readonly INavigationService navigationService;
        private readonly GameStateMachine gameStateSystem;
        private readonly LevelManager levelManager;

        private WorkshopTilemapEditor workshopTilemapEditor;
        private EditLevelScreen editLevelScreen;
        
        private const string WorkshoptilemapeditorTag = "WorkshopTilemapEditor";
        private const string EditLevelScene = "EditLevelScene";

        public EditLevelState(GameStateMachine gameStateSystem, INavigationService navigationService,
            LevelManager levelManager)
        {
            this.gameStateSystem = gameStateSystem;
            this.navigationService = navigationService;
            this.levelManager = levelManager;
        }

        public override void OnEnter()
        {
            editLevelScreen = navigationService.Push<EditLevelScreen>();
            editLevelScreen.BackPressed += OnBackPressed;
            editLevelScreen.SavePressed += OnSavePressed;
            editLevelScreen.PlayPressed += OnPlayPressed;
            editLevelScreen.CameraScaleChanged += OnCameraScaleChanged;

            LoadEditor().Forget();
        }

        public override void OnExit()
        {
            SceneManager.UnloadSceneAsync(EditLevelScene);

            editLevelScreen.BackPressed -= OnBackPressed;
            editLevelScreen.SavePressed -= OnSavePressed;
            editLevelScreen.PlayPressed -= OnPlayPressed;
            editLevelScreen.CameraScaleChanged -= OnCameraScaleChanged;
            navigationService.PopScreen(editLevelScreen);
        }

        private async UniTask LoadEditor()
        {
            await SceneManager.LoadSceneAsync(EditLevelScene, LoadSceneMode.Additive);
            var selectedLevel = levelManager.GetSelectedLevel();
            workshopTilemapEditor = GameObject.FindGameObjectWithTag(WorkshoptilemapeditorTag)
                ?.GetComponent<WorkshopTilemapEditor>();

            if (workshopTilemapEditor == null) {
                SceneManager.UnloadSceneAsync(EditLevelScene);
                Debug.LogError($"Could not find {nameof(WorkshopTilemapEditor)}");
                gameStateSystem.ChangeState<SelectLevelToEditState>();
            }
            
            workshopTilemapEditor.Setup(editLevelScreen.TilemapEditorUI);
            workshopTilemapEditor.LoadLevel(selectedLevel);
        }

        private void OnSavePressed()
        {
            var levelData = workshopTilemapEditor.SaveLevel();
            levelManager.SaveLevel(levelData);
        }

        private void OnBackPressed()
        {
            gameStateSystem.ChangeState<SelectLevelToEditState>();
        }

        private void OnPlayPressed()
        {
            levelManager.SelectLevel(workshopTilemapEditor.SaveLevel());
            gameStateSystem.ChangeState<TestPlayLevelState>();
        }

        private void OnCameraScaleChanged(float cameraScale)
        {
            workshopTilemapEditor.ChangeCameraScale(cameraScale);
        }
    }
}
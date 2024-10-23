using System;
using Core;
using Core.Navigation;
using Cysharp.Threading.Tasks;
using Game.Common.Level.Data;
using Game.Gameplay.Core;
using Game.Main.UI.Screens;
using Game.Workshop.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Main.Workshop
{
    public class WorkshopService : IWorkshopService
    {
        public event Action TestLevelStarted;
        public event Action TestLevelEnded;
        public event Action LevelEditingEnded;

        private readonly SceneContextRegistry sceneContextRegistry;

        private readonly INavigationService navigationService;

        private IWorkshopEditorService workshopEditorService;

        private IPlayService playService;
        
        private PlayLevelScreen playLevelScreen;

        public LevelData EditedLevelData { get; private set; }

        public WorkshopService(SceneContextRegistry sceneContextRegistry, INavigationService navigationService)
        {
            this.sceneContextRegistry = sceneContextRegistry;
            this.navigationService = navigationService;
        }

        public void SetLevelData(LevelData levelData)
        {
            EditedLevelData = levelData;
        }

        public async UniTask LoadWorkshop()
        {
            await SceneManager.LoadSceneAsync(SceneNames.EditorSceneName, LoadSceneMode.Additive);
            await UniTask.WaitForEndOfFrame();

            var editSceneContext = sceneContextRegistry.GetSceneContextForScene(SceneNames.EditorSceneName);
            workshopEditorService = editSceneContext.Container.Resolve<IWorkshopEditorService>();
            workshopEditorService.EditLevel(EditedLevelData);

            workshopEditorService.TestLeveStarted += OnTestLeveStarted;
            workshopEditorService.LevelEditingEnded += OnLevelEditingEnded;
        }

        public void UnloadWorkshop()
        {
            workshopEditorService.TestLeveStarted -= OnTestLeveStarted;
            SceneManager.UnloadSceneAsync(SceneNames.EditorSceneName);
        }

        private void OnTestLeveStarted(LevelData levelData)
        {
            EditedLevelData = levelData;
            TestLevelStarted?.Invoke();
        }

        private void OnLevelEditingEnded()
        {
            LevelEditingEnded?.Invoke();
        }

        public async UniTask LoadLevelTest()
        {
            playLevelScreen = navigationService.PushScreen<PlayLevelScreen>();
            
            await SceneManager.LoadSceneAsync(SceneNames.PlaySceneName, LoadSceneMode.Additive);
            await UniTask.WaitForEndOfFrame();
            
            var playSceneContext = sceneContextRegistry.GetSceneContextForScene(SceneNames.PlaySceneName);
            playService = playSceneContext.Container.Resolve<IPlayService>();
            playService.PlayLevel(EditedLevelData);
            
            playService.PlayingEnded += OnPlayingEnded;
        }

        public void UnloadLevelTest()
        {
            playService.PlayingEnded -= OnPlayingEnded;
            SceneManager.UnloadSceneAsync(SceneNames.PlaySceneName);
            navigationService.PopScreen(playLevelScreen);
        }

        public void Clear()
        {
            EditedLevelData = null;
        }

        private void OnPlayingEnded()
        {
            TestLevelEnded?.Invoke();
        }
    }
}
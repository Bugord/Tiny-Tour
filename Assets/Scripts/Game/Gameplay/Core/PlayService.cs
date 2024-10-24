using System;
using Game.Common.Level.Data;
using Game.Gameplay.Level;
using Game.Gameplay.Playing;
using Game.Gameplay.PlayState.Core;
using Game.Gameplay.PlayState.States;
using Game.Gameplay.UI;
using Game.Main.UI.Screens;

namespace Game.Gameplay.Core
{
    public class PlayService : IPlayService, IDisposable
    {
        public event Action PlayingExitCalled;
        public event Action LevelPassed;
        public event Action LevelFailed;

        private readonly PlayStateMachine playStateMachine;
        private readonly ILevelService levelService;
        private readonly IPlayRunningService playRunningService;
        private readonly PlayLevelScreen playLevelScreen;

        public PlayService(PlayStateMachine playStateMachine, ILevelService levelService,
            IPlayUIProvider playUIProvider, IPlayRunningService playRunningService)
        {
            this.playStateMachine = playStateMachine;
            this.levelService = levelService;
            this.playRunningService = playRunningService;

            playLevelScreen = playUIProvider.PlayLevelScreen;
            playLevelScreen.BackPressed += OnPlayExited;
        }

        public void Dispose()
        {
            playLevelScreen.BackPressed -= OnPlayExited;
        }

        public void PlayLevel(LevelData levelData)
        {
            levelService.LoadLevel(levelData);
            playStateMachine.ChangeState<EditingLevelState>();
        }

        public void RestartLevel()
        {
            playRunningService.ResetPlay();
            playStateMachine.ChangeState<EditingLevelState>();
        }

        public void ClearLevel()
        {
            levelService.ClearLevel();
        }

        public void OnLevelFailed()
        {
            LevelFailed?.Invoke();
        }

        public void OnLevelPassed()
        {
            LevelPassed?.Invoke();
        }

        public void OnPlayExited()
        {
            PlayingExitCalled?.Invoke();
        }
    }
}
using System;
using Core.Navigation;
using Game.Common.Level.Data;
using Game.Gameplay.Level;
using Game.Gameplay.UI;
using Game.Main.Session.Models;
using Game.Main.UI.Screens;

namespace Game.Gameplay.Core
{
    public class PlayService : IPlayService, IDisposable
    {
        public event Action PlayingStarted;
        public event Action PlayingEnded;

        private readonly ILevelService levelService;
        private readonly PlayLevelScreen playLevelScreen;

        public PlayService(ILevelService levelService, IPlayUIProvider playUIProvider)
        {
            this.levelService = levelService;

            playLevelScreen = playUIProvider.PlayLevelScreen;
            playLevelScreen.BackPressed += OnBackPressed;
        }

        public void PlayLevel(LevelData levelData)
        {
            levelService.LoadLevel(levelData);
            PlayingStarted?.Invoke();
        }

        public void EndPlaying()
        {
            levelService.ClearLevel();
            PlayingEnded?.Invoke();
        }

        public void Dispose()
        {
            playLevelScreen.BackPressed -= OnBackPressed;
        }

        private void OnBackPressed()
        {
            PlayingEnded?.Invoke();
        }
    }
}
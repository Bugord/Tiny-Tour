using System;
using Core.Navigation;
using Game.Main.UI.Screens;

namespace Game.Gameplay.UI
{
    public class PlayUIProvider : IPlayUIProvider, IDisposable
    {
        private readonly INavigationService navigationService;
        public PlayLevelScreen PlayLevelScreen { get; }

        public PlayUIProvider(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            PlayLevelScreen = navigationService.PushScreen<PlayLevelScreen>();
        }

        public void Dispose()
        {
            navigationService.PopScreen(PlayLevelScreen);
        }
    }
}
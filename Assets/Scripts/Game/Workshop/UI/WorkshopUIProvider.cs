using System;
using Core.Navigation;
using Game.Main.UI.Screens;

namespace Game.Workshop.UI
{
    public class WorkshopUIProvider : IDisposable, IWorkshopUIProvider
    {
        private readonly INavigationService navigationService;
        public EditLevelScreen EditLevelScreen {get; private set;}

        public WorkshopUIProvider(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            EditLevelScreen = navigationService.PushScreen<EditLevelScreen>();
        }

        public void Dispose()
        {
            navigationService.PopScreen(EditLevelScreen);
        }
    }
}
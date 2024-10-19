using Core.Navigation;
using Game.Common.UI;
using UI.Screens;

namespace Game.Common.EditorOptions
{
    public class GameplayEditorOptionsControllerUIProvider : IEditorOptionsControllerUIProvider
    {
        private readonly INavigationService navigationService;

        public EditorOptionsControllerUI EditorOptionsControllerUI =>
            navigationService.GetScreen<PlayLevelScreen>().EditorOptionsControllerUI;

        public GameplayEditorOptionsControllerUIProvider(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
    }
}
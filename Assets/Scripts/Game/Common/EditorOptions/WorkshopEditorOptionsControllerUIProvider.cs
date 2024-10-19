using Core.Navigation;
using Game.Common.UI;
using UI.Screens;

namespace Game.Common.EditorOptions
{
    public class WorkshopEditorOptionsControllerUIProvider : IEditorOptionsControllerUIProvider
    {
        private readonly INavigationService navigationService;

        public EditorOptionsControllerUI EditorOptionsControllerUI =>
            navigationService.GetScreen<EditLevelScreen>().EditorOptionsControllerUI;

        public WorkshopEditorOptionsControllerUIProvider(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
    }
}
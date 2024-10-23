using Game.Common.EditorOptions;
using Game.Common.UI;

namespace Game.Workshop.UI
{
    public class WorkshopEditorOptionsControllerUIProvider : IEditorOptionsControllerUIProvider
    {
        private readonly IWorkshopUIProvider workshopUIProvider;

        public EditorOptionsControllerUI EditorOptionsControllerUI =>
            workshopUIProvider.EditLevelScreen.EditorOptionsControllerUI;

        public WorkshopEditorOptionsControllerUIProvider(IWorkshopUIProvider workshopUIProvider)
        {
            this.workshopUIProvider = workshopUIProvider;
        }
    }
}
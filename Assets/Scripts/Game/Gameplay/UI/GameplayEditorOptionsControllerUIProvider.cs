using Game.Common.EditorOptions;
using Game.Common.UI;

namespace Game.Gameplay.UI
{
    public class GameplayEditorOptionsControllerUIProvider : IEditorOptionsControllerUIProvider
    {
        private readonly IPlayUIProvider playUIProvider;

        public EditorOptionsControllerUI EditorOptionsControllerUI => playUIProvider.PlayLevelScreen.EditorOptionsControllerUI;

        public GameplayEditorOptionsControllerUIProvider(IPlayUIProvider playUIProvider)
        {
            this.playUIProvider = playUIProvider;
        }
    }
}
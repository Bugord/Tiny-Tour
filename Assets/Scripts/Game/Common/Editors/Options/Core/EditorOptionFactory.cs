using Game.Common.UI.Editing.EditorOption;
using Game.Gameplay.Editing.Options.Model;
using Zenject;

namespace Game.Common.Editors.Options.Core
{
    public class EditorOptionFactory : IEditorOptionFactory
    {
        private readonly DiContainer diContainer;

        public EditorOptionFactory(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        public T Create<T>(EditorOptionUI optionUI) where T : BaseEditorOption
        {
            return diContainer.Instantiate<T>(new[] { optionUI });
        }
    }
}
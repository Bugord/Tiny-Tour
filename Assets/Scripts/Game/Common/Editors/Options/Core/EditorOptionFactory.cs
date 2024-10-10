using Game.Gameplay.Editing.Options.Model;
using Zenject;

namespace Common.Editors.Options.Core
{
    public class EditorOptionFactory : IEditorOptionFactory
    {
        private readonly DiContainer diContainer;

        public EditorOptionFactory(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        public T Create<T>() where T : BaseEditorOption
        {
            return diContainer.Instantiate<T>();
        }

        public T Create<T>(string id) where T : BaseEditorOption
        {
            throw new System.NotImplementedException();
        }
    }
}
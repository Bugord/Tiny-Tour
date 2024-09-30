using Zenject;

namespace Gameplay.Editing.Editors
{
    public class EditorOptionOptionFactory : IEditorOptionFactory
    {
        private readonly DiContainer diContainer;

        public EditorOptionOptionFactory(DiContainer diContainer)
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
using LevelEditor.EditorState.States;
using Zenject;

namespace LevelEditor.EditorState.Core
{
    public class EditorStateFactory : IEditorStateFactory
    {
        private readonly DiContainer diContainer;

        public EditorStateFactory(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        public T Create<T>(EditorStateMachine editorStateMachine) where T : BaseEditorState
        {
            return diContainer.Instantiate<T>(new[] { editorStateMachine });
        }
    }
}
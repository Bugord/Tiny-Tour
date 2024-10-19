using Game.Gameplay.Editing.Options.Model;

namespace Game.Common.EditorOptions
{
    public interface IEditorOptionsController
    {
        BaseEditorOption SelectedOption { get; }
        void AddOption<T>() where T : BaseEditorOption;
        void SelectOption<T>() where T : BaseEditorOption;
        void SelectOption(BaseEditorOption option);
    }
}
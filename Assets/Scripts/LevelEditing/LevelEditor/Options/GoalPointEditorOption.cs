using Gameplay.Editing.Editors;
using Gameplay.Editing.Options.Data;

namespace LevelEditing.LevelEditor.Options
{
    public class GoalSpawnPointEditorOption : BaseEditorOption
    {
        public GoalSpawnPointEditorOption(EditorOptionDataLibrary editorOptionDataLibrary)
        {
            EditorOptionData = editorOptionDataLibrary.GoalPointEditorOptionData;
        }
    }
}
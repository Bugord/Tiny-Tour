using Cars;
using Common.Editors.Logistic;
using Common.Editors.Road;
using Gameplay.Editing.Editors;
using Gameplay.Editing.Options.Data;
using LevelEditing.UI;
using LevelEditor.ColorVariants;
using UnityEngine;

namespace LevelEditing.LevelEditor.Options
{
    public class GoalSpawnPointEditorOption : BaseEditorOption
    {
        private readonly IGoalEditor goalEditor;
        private readonly IRoadEditor roadEditor;
        private readonly ColorButton colorButton;

        public GoalSpawnPointEditorOption(EditorOptionDataLibrary editorOptionDataLibrary,
            IGoalEditor goalEditor, IRoadEditor roadEditor, IColorButtonProvider colorButtonProvider)
        {
            this.goalEditor = goalEditor;
            this.roadEditor = roadEditor;
            colorButton = colorButtonProvider.ColorButton;
            EditorOptionData = editorOptionDataLibrary.GoalPointEditorOptionData;
        }

        public override void OnTileDown(Vector2Int position)
        {
            if (roadEditor.HasTile(position)) {
                goalEditor.SetTile(position, colorButton.Color);
            }
        }
    }
}
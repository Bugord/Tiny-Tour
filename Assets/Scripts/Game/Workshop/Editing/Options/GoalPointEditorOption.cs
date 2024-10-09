using Cars;
using Game.Common.Editors.Goals;
using Game.Common.Editors.Road;
using Gameplay.Editing.Editors;
using Gameplay.Editing.Options.Data;
using LevelEditing.UI;
using LevelEditor.ColorVariants;
using UnityEngine;

namespace LevelEditing.LevelEditor.Options
{
    public class GoalSpawnPointEditorOption : BaseEditorOption
    {
        private readonly IGoalLevelEditor goalLevelEditor;
        private readonly IRoadLevelEditor roadLevelEditor;
        private readonly ColorButton colorButton;

        public GoalSpawnPointEditorOption(EditorOptionDataLibrary editorOptionDataLibrary,
            IGoalLevelEditor goalLevelEditor, IRoadLevelEditor roadLevelEditor, IColorButtonProvider colorButtonProvider)
        {
            this.goalLevelEditor = goalLevelEditor;
            this.roadLevelEditor = roadLevelEditor;
            colorButton = colorButtonProvider.ColorButton;
            EditorOptionData = editorOptionDataLibrary.GoalPointEditorOptionData;
        }

        public override void OnTileDown(Vector2Int position)
        {
            if (roadLevelEditor.HasTile(position)) {
                goalLevelEditor.SetTile(position, colorButton.Color);
            }
        }
    }
}
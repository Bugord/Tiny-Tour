using Cars;
using Core;
using Game.Common.Editors.Goals;
using Game.Common.Editors.Road;
using Game.Gameplay.Editing.Options.Data;
using Game.Gameplay.Editing.Options.Model;
using Game.Workshop.Editing.Core;
using Game.Workshop.UI;
using LevelEditing.UI;
using LevelEditor.ColorVariants;
using UnityEngine;

namespace LevelEditing.LevelEditor.Options
{
    public class GoalSpawnPointEditorOption : BaseEditorOption
    {
        private readonly IGoalLevelEditor goalLevelEditor;
        private readonly IRoadLevelEditor roadLevelEditor;

        private TeamColor selectedColor;

        public GoalSpawnPointEditorOption(EditorOptionDataLibrary editorOptionDataLibrary,
            IGoalLevelEditor goalLevelEditor, IRoadLevelEditor roadLevelEditor,
            ILevelEditorController levelEditorController)
        {
            this.goalLevelEditor = goalLevelEditor;
            this.roadLevelEditor = roadLevelEditor;
            EditorOptionData = editorOptionDataLibrary.GoalPointEditorOptionData;

            SetupUI(levelEditorController);
            EditorOptionUI.EnableColorPicker();
        }

        protected override void OnColorSelected(TeamColor color)
        {
            selectedColor = color;
        }

        public override void OnTileDown(Vector2Int position)
        {
            if (roadLevelEditor.HasTile(position)) {
                goalLevelEditor.SetTile(position, selectedColor);
            }
        }
    }
}
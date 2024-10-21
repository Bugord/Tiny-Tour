using Common.Editors.Obstacles;
using Core;
using Game.Common.Editors.Goals;
using Game.Common.Editors.Road;
using Game.Common.UI.Editing.EditorOption;
using Game.Gameplay.Editing.Options.Data;
using Game.Gameplay.Editing.Options.Model;
using Game.Workshop.Editing.Core;
using UnityEngine;

namespace Game.Workshop.Editing.Options
{
    public class GoalSpawnPointEditorOption : BaseEditorOption
    {
        private readonly IGoalLevelEditor goalLevelEditor;
        private readonly IRoadEditor roadEditor;
        private readonly IObstaclesEditor obstaclesEditor;
        private readonly GoalPointEditorOptionData goalPointEditorOptionData;
        
        public GoalSpawnPointEditorOption(EditorOptionUI editorOptionUI, EditorOptionDataLibrary editorOptionDataLibrary,
            IGoalLevelEditor goalLevelEditor, IRoadEditor roadEditor, IObstaclesEditor obstaclesEditor)
            : base(editorOptionUI, editorOptionDataLibrary.GoalPointEditorOptionData)
        {
            this.goalLevelEditor = goalLevelEditor;
            this.roadEditor = roadEditor;
            this.obstaclesEditor = obstaclesEditor;
            goalPointEditorOptionData = editorOptionDataLibrary.GoalPointEditorOptionData;

            EditorOptionsConfiguration.EnableColorPicker();
        }

        protected override void OnColorSelected(TeamColor color)
        {
            var sprite = goalPointEditorOptionData.ColoredGoalPointsIcons[color];
            EditorOptionUI.SetIcon(sprite);
        }

        public override void OnTileDown(Vector2Int position)
        {
            if (CanBePlaced(position)) {
                goalLevelEditor.SetTile(position, EditorOptionsConfiguration.SelectedColor);
            }
        }

        private bool CanBePlaced(Vector2Int position)
        {
            var canBePlaced = roadEditor.HasTile(position);
            canBePlaced = canBePlaced && !obstaclesEditor.HasTile(position);

            return canBePlaced;
        }
    }
}
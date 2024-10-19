using Cars;
using Core;
using Game.Common.Editors.Road;
using Game.Gameplay.Editing.Options.Data;
using Game.Gameplay.Editing.Options.Model;
using Game.Workshop.Editing.Core;
using Game.Workshop.Editing.Editors;
using Game.Workshop.LevelEditor.Editors;
using Game.Workshop.UI;
using LevelEditing.UI;
using LevelEditor.ColorVariants;
using UnityEngine;

namespace LevelEditing.LevelEditor.Options
{
    public class CarSpawnPointEditorOption : BaseEditorOption
    {
        private readonly ISpawnPointLevelEditor spawnPointLevelEditor;
        private readonly IRoadLevelEditor roadLevelEditor;

        private TeamColor selectedColor;

        public CarSpawnPointEditorOption(EditorOptionDataLibrary editorOptionDataLibrary,
            ISpawnPointLevelEditor spawnPointLevelEditor, IRoadLevelEditor roadLevelEditor,
            ILevelEditorController levelEditorController)
        {
            this.spawnPointLevelEditor = spawnPointLevelEditor;
            this.roadLevelEditor = roadLevelEditor;
            EditorOptionData = editorOptionDataLibrary.CarSpawnPointEditorOptionData;

            SetupUI(levelEditorController);
            EditorOptionUI.EnableColorPicker();
        }

        protected override void OnColorSelected(TeamColor color)
        {
            selectedColor = color;
        }

        public override void OnTileDown(Vector2Int position)
        {
            if (spawnPointLevelEditor.HasSpawnPointWithColor(position, selectedColor)) {
                spawnPointLevelEditor.RotateSpawnPoint(position);
            }
            else if (roadLevelEditor.HasTile(position)) {
                spawnPointLevelEditor.SetCarSpawnPoint(position, CarType.Regular, selectedColor, Direction.Right);
            }
        }
    }
}
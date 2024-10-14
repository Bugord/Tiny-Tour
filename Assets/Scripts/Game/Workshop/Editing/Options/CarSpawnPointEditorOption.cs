using Cars;
using Core;
using Game.Common.Editors.Road;
using Game.Gameplay.Editing.Options.Data;
using Game.Gameplay.Editing.Options.Model;
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
        private readonly ColorButton colorButton;

        public CarSpawnPointEditorOption(EditorOptionDataLibrary editorOptionDataLibrary,
            ISpawnPointLevelEditor spawnPointLevelEditor, IRoadLevelEditor roadLevelEditor, IColorButtonProvider colorButtonProvider)
        {
            this.spawnPointLevelEditor = spawnPointLevelEditor;
            this.roadLevelEditor = roadLevelEditor;
            colorButton = colorButtonProvider.ColorButton;
            EditorOptionData = editorOptionDataLibrary.CarSpawnPointEditorOptionData;
        }

        public override void OnTileDown(Vector2Int position)
        {
            if (spawnPointLevelEditor.HasSpawnPointWithColor(position, colorButton.Color)) {
                spawnPointLevelEditor.RotateSpawnPoint(position);
            }
            else if (roadLevelEditor.HasTile(position)) {
                spawnPointLevelEditor.SetCarSpawnPoint(position, CarType.Regular, colorButton.Color, Direction.Right);
            }
        }
    }
}
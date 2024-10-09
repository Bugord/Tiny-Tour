using Cars;
using Common.Editors.Road;
using Core;
using Gameplay.Editing.Editors;
using Gameplay.Editing.Options.Data;
using LevelEditing.Editing.Editors;
using LevelEditing.UI;
using LevelEditor.ColorVariants;
using UnityEngine;

namespace LevelEditing.LevelEditor.Options
{
    public class CarSpawnPointEditorOption : BaseEditorOption
    {
        private readonly ISpawnPointEditor spawnPointEditor;
        private readonly IRoadEditor roadEditor;
        private readonly ColorButton colorButton;

        public CarSpawnPointEditorOption(EditorOptionDataLibrary editorOptionDataLibrary,
            ISpawnPointEditor spawnPointEditor, IRoadEditor roadEditor, IColorButtonProvider colorButtonProvider)
        {
            this.spawnPointEditor = spawnPointEditor;
            this.roadEditor = roadEditor;
            colorButton = colorButtonProvider.ColorButton;
            EditorOptionData = editorOptionDataLibrary.CarSpawnPointEditorOptionData;
        }

        public override void OnTileDown(Vector2Int position)
        {
            if (spawnPointEditor.HasSpawnPointWithColor(position, colorButton.Color)) {
                spawnPointEditor.RotateSpawnPoint(position);
            }
            else if (roadEditor.HasTile(position)) {
                spawnPointEditor.SetCarSpawnPoint(position, CarType.Regular, colorButton.Color, Direction.Right);
            }
        }
    }
}
using Cars;
using Core;
using Game.Common.Editors.Road;
using Game.Common.UI.Editing.EditorOption;
using Game.Gameplay.Editing.Options.Data;
using Game.Gameplay.Editing.Options.Model;
using Game.Workshop.Editing.Editors;
using UnityEngine;

namespace Game.Workshop.Editing.Options
{
    public class CarSpawnPointEditorOption : BaseEditorOption
    {
        private readonly ISpawnPointLevelEditor spawnPointLevelEditor;
        private readonly IRoadLevelEditor roadLevelEditor;
        
        public CarSpawnPointEditorOption(EditorOptionDataLibrary editorOptionDataLibrary,
            ISpawnPointLevelEditor spawnPointLevelEditor, IRoadLevelEditor roadLevelEditor,
            EditorOptionUI editorOptionUI)
            : base(editorOptionUI, editorOptionDataLibrary.CarSpawnPointEditorOptionData)
        {
            this.spawnPointLevelEditor = spawnPointLevelEditor;
            this.roadLevelEditor = roadLevelEditor;

            EditorOptionsConfiguration.EnableColorPicker();
        }

        public override void OnTileDown(Vector2Int position)
        {
            if (spawnPointLevelEditor.HasSpawnPointWithColor(position, EditorOptionsConfiguration.SelectedColor)) {
                spawnPointLevelEditor.RotateSpawnPoint(position);
            }
            else if (roadLevelEditor.HasTile(position)) {
                spawnPointLevelEditor.SetCarSpawnPoint(position, CarType.Regular, EditorOptionsConfiguration.SelectedColor, Direction.Right);
            }
        }
    }
}
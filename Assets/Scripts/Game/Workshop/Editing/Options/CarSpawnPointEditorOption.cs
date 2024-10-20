using System.Linq;
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
        private readonly CarSpawnPointEditorOptionData carSpawnPointEditorOptionData;

        public CarSpawnPointEditorOption(EditorOptionDataLibrary editorOptionDataLibrary,
            ISpawnPointLevelEditor spawnPointLevelEditor, IRoadLevelEditor roadLevelEditor,
            EditorOptionUI editorOptionUI)
            : base(editorOptionUI, editorOptionDataLibrary.CarSpawnPointEditorOptionData)
        {
            this.spawnPointLevelEditor = spawnPointLevelEditor;
            this.roadLevelEditor = roadLevelEditor;
            carSpawnPointEditorOptionData = editorOptionDataLibrary.CarSpawnPointEditorOptionData;

            EditorOptionsConfiguration.EnableColorPicker();
            SetCarAlternatives();
        }

        protected override void OnColorSelected(TeamColor color)
        {
            SetCarAlternatives();
            UpdateSprite();
        }

        protected override void OnAlternativeSelected(int alternativeId)
        {
            UpdateSprite();
        }

        private void SetCarAlternatives()
        {
            var selectedColor = EditorOptionsConfiguration.SelectedColor;
            var mappedAlternatives = carSpawnPointEditorOptionData.ColoredCarSpawnData
                .ToDictionary(data => data.Key, data => data.Value.ColoredCarVariants[selectedColor]);

            EditorOptionsConfiguration.SetAlternatives(mappedAlternatives);
        }

        private void UpdateSprite()
        {
            var color = EditorOptionsConfiguration.SelectedColor;
            var carType = (CarType)EditorOptionsConfiguration.SelectedAlternativeIndex;

            var icon = carSpawnPointEditorOptionData.ColoredCarSpawnData[carType].ColoredCarVariants[color];
            EditorOptionUI.SetIcon(icon);
        }

        public override void OnTileDown(Vector2Int position)
        {
            var color = EditorOptionsConfiguration.SelectedColor;
            var carType = (CarType)EditorOptionsConfiguration.SelectedAlternativeIndex;

            if (spawnPointLevelEditor.HasSpawnPoint(position, carType, color)) {
                spawnPointLevelEditor.RotateSpawnPoint(position);
            }
            else if (roadLevelEditor.HasTile(position)) {
                spawnPointLevelEditor.SetCarSpawnPoint(position, carType, color, Direction.Right);
            }
        }
    }
}
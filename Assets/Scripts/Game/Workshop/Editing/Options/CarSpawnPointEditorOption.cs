﻿using System.Linq;
using Common.Editors.Obstacles;
using Core;
using Game.Common.Cars.Core;
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
        private readonly ISpawnPointEditor spawnPointEditor;
        private readonly IRoadEditor roadEditor;
        private readonly IObstaclesEditor obstaclesEditor;
        private readonly CarSpawnPointEditorOptionData carSpawnPointEditorOptionData;

        public CarSpawnPointEditorOption(EditorOptionDataLibrary editorOptionDataLibrary,
            ISpawnPointEditor spawnPointEditor, IRoadEditor roadEditor,
            EditorOptionUI editorOptionUI, IObstaclesEditor obstaclesEditor)
            : base(editorOptionUI, editorOptionDataLibrary.CarSpawnPointEditorOptionData)
        {
            this.spawnPointEditor = spawnPointEditor;
            this.roadEditor = roadEditor;
            this.obstaclesEditor = obstaclesEditor;
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

            if (spawnPointEditor.HasSpawnPoint(position, carType, color)) {
                spawnPointEditor.RotateSpawnPoint(position);
                return;
            }
            
            if (CanBePlaced(position)) {
                spawnPointEditor.SetCarSpawnPoint(position, carType, color, Direction.Right);
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
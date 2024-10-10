using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Editors.Options.Core;
using Common.UI;
using Core;
using Core.Navigation;
using Game.Workshop.UI;
using Gameplay.Editing.Editors;
using Gameplay.Editing.Options;
using LevelEditing.LevelEditor.Options;
using LevelEditing.UI;
using UI.Screens;
using UnityEngine;
using Zenject;

namespace LevelEditing.Editing.Core
{
    public class LevelEditorController : IInitializable, IDisposable
    {
        private readonly ITilemapInput tilemapInput;
        private readonly IEditorOptionFactory editorOptionFactory;
        private readonly Dictionary<string, BaseEditorOption> editorOptions;
        private readonly EditorOptionsControllerUI editorOptionsControllerUI;
        private readonly ColorButton colorButton;

        private BaseEditorOption selectedEditorOption;
        private BaseEditorOption cachedEditorOption;

        private ErasingWorkshopEditorOption option;

        public LevelEditorController(ITilemapInput tilemapInput, INavigationService navigationService,
            IEditorOptionFactory editorOptionFactory)
        {
            this.tilemapInput = tilemapInput;
            this.editorOptionFactory = editorOptionFactory;
            var editLevelScreen = navigationService.GetScreen<EditLevelScreen>();
            editorOptionsControllerUI = editLevelScreen.EditorOptionsControllerUI;
            colorButton = editLevelScreen.ColorButton;

            editorOptions = new Dictionary<string, BaseEditorOption>();
        }

        public void Initialize()
        {
            AddEditorOption<GroundTerrainEditorOption>();
            AddEditorOption<BridgeTerrainEditorOption>();
            AddEditorOption<CarSpawnPointEditorOption>();
            AddEditorOption<GoalSpawnPointEditorOption>();
            AddEditorOption<RoadEditorOption>();

            option = editorOptionFactory.Create<ErasingWorkshopEditorOption>();
            editorOptions.Add(option.EditorOptionData.Id, option);

            selectedEditorOption = editorOptions.First().Value;

            editorOptionsControllerUI.Init(editorOptions.Values.Select(option => option.EditorOptionData));
            editorOptionsControllerUI.EditorOptionSelected += OnOptionSelected;
            
            colorButton.ColorChanged += OnColorChanged;

            var defaultOptionId = editorOptions.First().Key;
            editorOptionsControllerUI.SelectOption(defaultOptionId);
        }

        public void Dispose()
        {
            editorOptionsControllerUI.EditorOptionSelected -= OnOptionSelected;
            colorButton.ColorChanged -= OnColorChanged;
        }

        private void OnColorChanged(TeamColor color)
        {
            editorOptionsControllerUI.SetColor(color);
        }

        private void AddEditorOption<T>() where T : BaseEditorOption
        {
            var editorOption = editorOptionFactory.Create<T>();
            var id = editorOption.EditorOptionData.Id;

            editorOptions.Add(id, editorOption);
        }

        private void SelectOption(string id)
        {
            editorOptionsControllerUI.SelectOption(id);
            selectedEditorOption = editorOptions[id];
        }

        private void OnOptionSelected(string id)
        {
            selectedEditorOption = editorOptions[id];
        }

        public void EnableEditing()
        {
            tilemapInput.TileDragged += OnTileDragged;
            tilemapInput.TilePressDown += OnTileDown;
            tilemapInput.TilePressUp += OnTileUp;

            tilemapInput.TileAltDragged += OnTileAltDragged;
            tilemapInput.TileAltPressDown += OnTileAltDown;
            tilemapInput.TileAltPressUp += OnTileAltUp;
        }

        public void DisableEditing()
        {
            tilemapInput.TileDragged -= OnTileDragged;
            tilemapInput.TilePressDown -= OnTileDown;
            tilemapInput.TilePressUp -= OnTileUp;

            tilemapInput.TileAltDragged -= OnTileAltDragged;
            tilemapInput.TileAltPressDown -= OnTileAltDown;
            tilemapInput.TileAltPressUp -= OnTileAltUp;
        }

        private void OnTileAltDown(Vector2Int tilePos)
        {
            cachedEditorOption = selectedEditorOption;
            SelectOption(option.EditorOptionData.Id);

            selectedEditorOption.OnAltTileDown(tilePos);
        }

        private void OnTileAltDragged(Vector2Int tilePos)
        {
            selectedEditorOption.OnAltTileDrag(tilePos);
        }

        private void OnTileAltUp(Vector2Int tilePos)
        {
            SelectOption(cachedEditorOption.EditorOptionData.Id);
            selectedEditorOption.OnAltTileUp(tilePos);
        }

        private void OnTileUp(Vector2Int tilePos)
        {
            selectedEditorOption.OnTileUp(tilePos);
        }

        private void OnTileDown(Vector2Int tilePos)
        {
            selectedEditorOption.OnTileDown(tilePos);
        }

        private void OnTileDragged(Vector2Int tilePos)
        {
            selectedEditorOption.OnTileDrag(tilePos);
        }
    }
}
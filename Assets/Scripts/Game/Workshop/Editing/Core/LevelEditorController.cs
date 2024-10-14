using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.UI;
using Core.Navigation;
using Game.Common.Editors.Options.Core;
using Game.Common.UI.Editing.EditorOption;
using Game.Gameplay.Editing.Options.Model;
using Game.Main.UI.Controls.Playing;
using LevelEditing.LevelEditor.Options;
using LevelEditor.LevelEditor.Options;
using UI.Screens;
using UnityEngine;
using Zenject;

namespace Game.Workshop.Editing.Core
{
    public class LevelEditorController : IInitializable, IDisposable, ILevelEditorController
    {
        private readonly ITilemapInput tilemapInput;
        private readonly IEditorOptionFactory editorOptionFactory;
        private readonly IEditorOptionUIFactory editorOptionUIFactory;
        private readonly EditorOptionsControllerUI editorOptionsControllerUI;

        private readonly Dictionary<string, BaseEditorOption> editorOptions;
        
        private BaseEditorOption selectedEditorOption;
        private BaseEditorOption cachedEditorOption;

        private ErasingWorkshopEditorOption erasingOption;

        public LevelEditorController(ITilemapInput tilemapInput, INavigationService navigationService,
            IEditorOptionFactory editorOptionFactory, IEditorOptionUIFactory editorOptionUIFactory)
        {
            this.tilemapInput = tilemapInput;
            this.editorOptionFactory = editorOptionFactory;
            this.editorOptionUIFactory = editorOptionUIFactory;
            var editLevelScreen = navigationService.GetScreen<EditLevelScreen>();
            editorOptionsControllerUI = editLevelScreen.EditorOptionsControllerUI;

            editorOptions = new Dictionary<string, BaseEditorOption>();
        }

        public void Initialize()
        {
            AddEditorOption<TerrainWorkshopEditorOption>();
            // AddEditorOption<CarSpawnPointEditorOption>();
            // AddEditorOption<GoalSpawnPointEditorOption>();
            // AddEditorOption<RoadEditorOption>();
            erasingOption = AddEditorOption<ErasingWorkshopEditorOption>();

            selectedEditorOption = editorOptions.First().Value;
            editorOptionsControllerUI.EditorOptionSelected += OnOptionSelected;

            var defaultOptionId = editorOptions.First().Key;
            editorOptionsControllerUI.SelectOption(defaultOptionId);
        }

        public void Dispose()
        {
            editorOptionsControllerUI.EditorOptionSelected -= OnOptionSelected;
        }

        public T AddEditorOption<T>() where T : BaseEditorOption
        {
            var editorOption = editorOptionFactory.Create<T>();
            editorOptions.Add(editorOption.Id, editorOption);
            return editorOption;
        }

        public EditorOptionUI AddEditorOptionUI(string id)
        {
            return editorOptionsControllerUI.InstantiateEditorOptionUI(id);
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
            SelectOption(erasingOption.Id);

            selectedEditorOption.OnAltTileDown(tilePos);
        }

        private void OnTileAltDragged(Vector2Int tilePos)
        {
            selectedEditorOption.OnAltTileDrag(tilePos);
        }

        private void OnTileAltUp(Vector2Int tilePos)
        {
            SelectOption(cachedEditorOption.Id);
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
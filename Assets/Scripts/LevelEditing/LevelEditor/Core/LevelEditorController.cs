﻿using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Editors.Options.Core;
using Common.UI;
using Core.Navigation;
using Gameplay.Editing.Editors;
using Gameplay.Editing.Options;
using LevelEditing.LevelEditor.Options;
using UI.Screens;
using UnityEngine;
using Zenject;

namespace LevelEditing.Editing.Core
{
    public class LevelEditorController : IInitializable
    {
        private readonly ITilemapInput tilemapInput;
        private readonly IEditorOptionFactory editorOptionFactory;
        private readonly Dictionary<string, BaseEditorOption> editorOptions;
        private readonly EditorOptionsControllerUI editorOptionsControllerUI;

        private BaseEditorOption selectedEditorOption;
        private BaseEditorOption cachedEditorOption;

        private EditorErasingEditorOption erasingEditorOption;

        public LevelEditorController(ITilemapInput tilemapInput, INavigationService navigationService, IEditorOptionFactory editorOptionFactory)
        {
            this.tilemapInput = tilemapInput;
            this.editorOptionFactory = editorOptionFactory;
            editorOptionsControllerUI = navigationService.GetScreen<EditLevelScreen>().EditorOptionsControllerUI;

            editorOptions = new Dictionary<string, BaseEditorOption>();
        }

        public void Initialize()
        {
            AddEditorOption<GroundTerrainEditorOption>();
            AddEditorOption<WaterTerrainEditorOption>();
            AddEditorOption<BridgeTerrainEditorOption>();
            AddEditorOption<RoadEditorOption>();
            
            erasingEditorOption = editorOptionFactory.Create<EditorErasingEditorOption>();
            editorOptions.Add(erasingEditorOption.EditorOptionData.Id, erasingEditorOption);

            selectedEditorOption = editorOptions.First().Value;

            editorOptionsControllerUI.Init(editorOptions.Values.Select(option => option.EditorOptionData));
            editorOptionsControllerUI.EditorOptionSelected += OnOptionSelected;

            var defaultOptionId = editorOptions.First().Key;
            editorOptionsControllerUI.SelectOption(defaultOptionId);
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

        private void OnTileAltDown(Vector3Int tilePos)
        {
            cachedEditorOption = selectedEditorOption;
            SelectOption(erasingEditorOption.EditorOptionData.Id);
            
            selectedEditorOption.OnAltTileDown(tilePos);
        }

        private void OnTileAltDragged(Vector3Int tilePos)
        {
            selectedEditorOption.OnAltTileDrag(tilePos);
        }

        private void OnTileAltUp(Vector3Int tilePos)
        {
            SelectOption(cachedEditorOption.EditorOptionData.Id);
            selectedEditorOption.OnAltTileUp(tilePos);
        }

        private void OnTileUp(Vector3Int tilePos)
        {
            selectedEditorOption.OnTileUp(tilePos);
        }

        private void OnTileDown(Vector3Int tilePos)
        {
            selectedEditorOption.OnTileDown(tilePos);
        }

        private void OnTileDragged(Vector3Int tilePos)
        {
            selectedEditorOption.OnTileDrag(tilePos);
        }
    }
}
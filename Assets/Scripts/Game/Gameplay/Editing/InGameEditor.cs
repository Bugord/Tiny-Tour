using System;
using System.Collections.Generic;
using System.Linq;
using Common.Editors.Options.Core;
using Common.UI;
using Core.Navigation;
using Game.Gameplay.Editing.Options.Model;
using Gameplay.Editing.Options;
using Gameplay.UI;
using UI;
using UI.Screens;
using UnityEngine;
using Zenject;

namespace Common
{
    public class InGameEditor : IInitializable
    {
        private readonly ITilemapInput tilemapInput;
        private readonly IEditorOptionFactory editorOptionFactory;
        private readonly Dictionary<string, BaseEditorOption> editorOptions;
        private readonly EditorOptionsControllerUI editorOptionsControllerUI;

        private BaseEditorOption selectedEditorOption;

        public InGameEditor(ITilemapInput tilemapInput, IEditorOptionFactory editorOptionFactory, INavigationService navigationService)
        {
            this.tilemapInput = tilemapInput;
            this.editorOptionFactory = editorOptionFactory;
            editorOptionsControllerUI = navigationService.GetScreen<PlayLevelScreen>().EditorOptionsControllerUI;

            editorOptions = new Dictionary<string, BaseEditorOption>();
        }

        public void Initialize()
        {
            AddEditorOption<RoadEditorOption>();
            AddEditorOption<ErasingEditorOption>();

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

        private void OnTileAltUp(Vector2Int tilePos)
        {
            selectedEditorOption.OnAltTileUp(tilePos);
        }

        private void OnTileAltDown(Vector2Int tilePos)
        {
            selectedEditorOption.OnAltTileDown(tilePos);
        }

        private void OnTileAltDragged(Vector2Int tilePos)
        {
            selectedEditorOption.OnAltTileDrag(tilePos);
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
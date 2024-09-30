using System;
using System.Collections.Generic;
using System.Linq;
using Core.Navigation;
using Gameplay.Editing.Editors;
using Gameplay.Editing.Options;
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
        private readonly InGameEditorUI inGameEditorUI;

        private BaseEditorOption selectedEditorOption;

        public InGameEditor(ITilemapInput tilemapInput, IEditorOptionFactory editorOptionFactory, INavigationService navigationService)
        {
            this.tilemapInput = tilemapInput;
            this.editorOptionFactory = editorOptionFactory;
            inGameEditorUI = navigationService.GetScreen<PlayLevelScreen>().InGameEditorUI;

            editorOptions = new Dictionary<string, BaseEditorOption>();
        }

        public void Initialize()
        {
            AddEditorOption<RoadEditorOption>();
            AddEditorOption<ErasingEditorOption>();

            selectedEditorOption = editorOptions.First().Value;
            
            inGameEditorUI.Init(editorOptions.Values.Select(option => option.EditorOptionData));
            inGameEditorUI.EditorOptionSelected += OnOptionSelected;
            
            var defaultOptionId = editorOptions.First().Key;
            inGameEditorUI.SelectOption(defaultOptionId);
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

        public void Enable()
        {
            tilemapInput.TileDragged += OnTileDragged;
            tilemapInput.TilePressDown += OnTileDown;
            tilemapInput.TilePressUp += OnTileUp;

            tilemapInput.TileAltDragged += OnTileAltDragged;
            tilemapInput.TileAltPressDown += OnTileAltDown;
            tilemapInput.TileAltPressUp += OnTileAltUp;
        }

        public void Disable()
        {
            tilemapInput.TileDragged += OnTileDragged;
            tilemapInput.TilePressDown += OnTileDown;
            tilemapInput.TilePressUp += OnTileUp;

            tilemapInput.TileAltDragged -= OnTileAltDragged;
            tilemapInput.TileAltPressDown -= OnTileAltDown;
            tilemapInput.TileAltPressUp -= OnTileAltUp;
        }

        private void OnTileAltUp(Vector3Int tilePos)
        {
            selectedEditorOption.OnAltTileUp(tilePos);
        }

        private void OnTileAltDown(Vector3Int tilePos)
        {
            selectedEditorOption.OnAltTileDown(tilePos);
        }

        private void OnTileAltDragged(Vector3Int tilePos)
        {
            selectedEditorOption.OnAltTileDrag(tilePos);
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
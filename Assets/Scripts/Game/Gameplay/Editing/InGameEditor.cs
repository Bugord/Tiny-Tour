using Common;
using Game.Common.EditorOptions;
using Game.Gameplay.Editing.Options.Model;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Editing
{
    public class InGameEditor : IInitializable
    {
        private readonly ITilemapInput tilemapInput;
        private readonly IEditorOptionsController editorOptionsController;

        private BaseEditorOption cachedEditorOption;

        public InGameEditor(ITilemapInput tilemapInput, IEditorOptionsController editorOptionsController)
        {
            this.tilemapInput = tilemapInput;
            this.editorOptionsController = editorOptionsController;
        }

        public void Initialize()
        {
            editorOptionsController.AddOption<RoadEditorOption>();
            editorOptionsController.AddOption<ErasingEditorOption>();

            editorOptionsController.SelectOption<RoadEditorOption>();
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
            cachedEditorOption = editorOptionsController.SelectedOption;
            editorOptionsController.SelectOption<ErasingEditorOption>();

            editorOptionsController.SelectedOption.OnAltTileDown(tilePos);
        }

        private void OnTileAltUp(Vector2Int tilePos)
        {
            editorOptionsController.SelectedOption.OnAltTileUp(tilePos);

            editorOptionsController.SelectOption(cachedEditorOption);
            cachedEditorOption = null;
        }

        private void OnTileAltDragged(Vector2Int tilePos)
        {
            editorOptionsController.SelectedOption.OnAltTileDrag(tilePos);
        }

        private void OnTileUp(Vector2Int tilePos)
        {
            editorOptionsController.SelectedOption.OnTileUp(tilePos);
        }

        private void OnTileDown(Vector2Int tilePos)
        {
            editorOptionsController.SelectedOption.OnTileDown(tilePos);
        }

        private void OnTileDragged(Vector2Int tilePos)
        {
            editorOptionsController.SelectedOption.OnTileDrag(tilePos);
        }
    }
}
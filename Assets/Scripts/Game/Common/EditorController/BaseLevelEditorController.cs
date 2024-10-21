using Common;
using Game.Common.EditorOptions;
using Game.Gameplay.Editing.Options.Model;
using Game.Workshop.Editing.Options;
using UnityEngine;
using Zenject;

namespace Game.Common.EditorController
{
    public class BaseLevelEditorController : IInitializable
    {
        private readonly ITilemapInput tilemapInput;
        protected readonly IEditorOptionsController EditorOptionsController;

        private BaseEditorOption cachedEditorOption;

        protected BaseLevelEditorController(ITilemapInput tilemapInput, IEditorOptionsController editorOptionsController)
        {
            EditorOptionsController = editorOptionsController;
            this.tilemapInput = tilemapInput;
        }

        public void Initialize()
        {
            AddOptions();
        }

        protected virtual void AddOptions()
        {
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
            cachedEditorOption = EditorOptionsController.SelectedOption;
            EditorOptionsController.SelectOption<ErasingWorkshopEditorOption>();

            EditorOptionsController.SelectedOption.OnAltTileDown(tilePos);
        }

        private void OnTileAltDragged(Vector2Int tilePos)
        {
            EditorOptionsController.SelectedOption.OnAltTileDrag(tilePos);
        }

        private void OnTileAltUp(Vector2Int tilePos)
        {
            EditorOptionsController.SelectedOption.OnAltTileUp(tilePos);

            EditorOptionsController.SelectOption(cachedEditorOption);
            cachedEditorOption = null;
        }

        private void OnTileUp(Vector2Int tilePos)
        {
            EditorOptionsController.SelectedOption.OnTileUp(tilePos);
        }

        private void OnTileDown(Vector2Int tilePos)
        {
            EditorOptionsController.SelectedOption.OnTileDown(tilePos);
        }

        private void OnTileDragged(Vector2Int tilePos)
        {
            EditorOptionsController.SelectedOption.OnTileDrag(tilePos);
        }
    }
}
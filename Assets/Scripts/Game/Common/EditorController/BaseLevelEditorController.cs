using Common;
using Game.Common.EditorOptions;
using UnityEngine;
using Zenject;

namespace Game.Common.EditorController
{
    public class BaseLevelEditorController : IInitializable
    {
        private readonly ITilemapInput tilemapInput;
        protected readonly IEditorOptionsController EditorOptionsController;
        
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

        protected virtual void OnTileAltDown(Vector2Int tilePos)
        {
            EditorOptionsController.SelectedOption.OnAltTileDown(tilePos);
        }

        protected virtual void OnTileAltDragged(Vector2Int tilePos)
        {
            EditorOptionsController.SelectedOption.OnAltTileDrag(tilePos);
        }

        protected virtual void OnTileAltUp(Vector2Int tilePos)
        {
            EditorOptionsController.SelectedOption.OnAltTileUp(tilePos);
        }

        protected virtual void OnTileUp(Vector2Int tilePos)
        {
            EditorOptionsController.SelectedOption.OnTileUp(tilePos);
        }

        protected virtual void OnTileDown(Vector2Int tilePos)
        {
            EditorOptionsController.SelectedOption.OnTileDown(tilePos);
        }

        protected virtual void OnTileDragged(Vector2Int tilePos)
        {
            EditorOptionsController.SelectedOption.OnTileDrag(tilePos);
        }
    }
}
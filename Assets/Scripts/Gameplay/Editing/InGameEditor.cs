using Gameplay.Editing.Editors;
using Gameplay.Editing.Options;
using UnityEngine;
using Zenject;

namespace Common
{
    public class InGameEditor : IInitializable
    {
        private readonly ITilemapInput tilemapInput;
        private readonly IEditorOptionFactory editorOptionFactory;

        private BaseEditorOption selectedEditorOption;

        public InGameEditor(ITilemapInput tilemapInput, IEditorOptionFactory editorOptionFactory)
        {
            this.tilemapInput = tilemapInput;
            this.editorOptionFactory = editorOptionFactory;
        }

        public void Initialize()
        {
            selectedEditorOption = editorOptionFactory.Create<RoadEditorOption>();
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
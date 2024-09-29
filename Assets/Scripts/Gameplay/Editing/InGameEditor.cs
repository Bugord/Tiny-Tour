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
        }

        public void Disable()
        {
            tilemapInput.TileDragged += OnTileDragged;
            tilemapInput.TilePressDown += OnTileDown;
            tilemapInput.TilePressUp += OnTileUp;
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
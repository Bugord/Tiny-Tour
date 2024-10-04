using Common.Editors.Terrain;
using Gameplay.Editing.Editors;
using Tiles.Ground;
using UnityEngine;

namespace LevelEditor.LevelEditor.Options
{
    public abstract class BaseTerrainEditorOption : BaseEditorOption
    {
        private readonly ITerrainEditor terrainEditor;
        protected virtual TerrainType TerrainType { get; }

        protected BaseTerrainEditorOption(ITerrainEditor terrainEditor)
        {
            this.terrainEditor = terrainEditor;
        }

        public override void OnTileDown(Vector2Int position)
        {
            terrainEditor.SetTerrainTile(position, TerrainType);
        }

        public override void OnTileDrag(Vector2Int position)
        {
            terrainEditor.SetTerrainTile(position, TerrainType);
        }

        public override void OnAltTileDown(Vector2Int position)
        {
            terrainEditor.EraseTile(position);
        }

        public override void OnAltTileDrag(Vector2Int position)
        {
            terrainEditor.EraseTile(position);
        }
    }
}
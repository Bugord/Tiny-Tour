using Common.Editors.Terrain;
using Game.Common.UI.Editing.EditorOption;
using Game.Gameplay.Editing.Options.Data;
using Game.Gameplay.Editing.Options.Model;
using Game.Workshop.Editing.Core;
using Tiles.Ground;
using UnityEngine;

namespace Game.Workshop.Editing.Options
{
    public class TerrainWorkshopEditorOption : BaseEditorOption
    {
        private readonly ITerrainLevelEditor terrainEditor;

        private EditorOptionUI editorOptionUI;
        private TerrainType selectedTerrainType;

        public TerrainWorkshopEditorOption(EditorOptionUI editorOptionUI, ITerrainLevelEditor terrainEditor,
            EditorOptionDataLibrary editorOptionDataLibrary)
            : base(editorOptionUI, editorOptionDataLibrary.TerrainEditorOptionData)
        {
            this.terrainEditor = terrainEditor;

            EditorOptionsConfiguration.SetAlternatives(editorOptionDataLibrary.TerrainEditorOptionData
                .AlternativeTerrains);
        }

        protected override void OnAlternativeSelected(int alternativeId)
        {
            selectedTerrainType = (TerrainType)alternativeId;
        }

        public override void OnTileDown(Vector2Int position)
        {
            terrainEditor.SetTerrainTile(position, selectedTerrainType);
        }

        public override void OnTileDrag(Vector2Int position)
        {
            terrainEditor.SetTerrainTile(position, selectedTerrainType);
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
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
        private readonly ITerrainEditor terrainEditor;
        private readonly TerrainEditorOptionData terrainEditorOptionData;

        public TerrainWorkshopEditorOption(EditorOptionUI editorOptionUI, ITerrainEditor terrainEditor,
            EditorOptionDataLibrary editorOptionDataLibrary)
            : base(editorOptionUI, editorOptionDataLibrary.TerrainEditorOptionData)
        {
            this.terrainEditor = terrainEditor;
            terrainEditorOptionData = editorOptionDataLibrary.TerrainEditorOptionData;

            EditorOptionsConfiguration.SetAlternatives(editorOptionDataLibrary.TerrainEditorOptionData.AlternativeTerrains);
        }

        protected override void OnAlternativeSelected(int alternativeId)
        {
            var sprite = terrainEditorOptionData.AlternativeTerrains[(TerrainType)alternativeId];
            EditorOptionUI.SetIcon(sprite);
        }

        public override void OnTileDown(Vector2Int position)
        {
            terrainEditor.SetTerrainTile(position, (TerrainType)EditorOptionsConfiguration.SelectedAlternativeIndex);
        }

        public override void OnTileDrag(Vector2Int position)
        {
            terrainEditor.SetTerrainTile(position, (TerrainType)EditorOptionsConfiguration.SelectedAlternativeIndex);
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
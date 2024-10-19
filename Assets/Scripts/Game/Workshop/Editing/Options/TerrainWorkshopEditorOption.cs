using System.Linq;
using Common.Editors.Terrain;
using Core.Navigation;
using Game.Common.UI.Editing.EditorOption;
using Game.Gameplay.Editing.Options.Data;
using Game.Gameplay.Editing.Options.Model;
using Game.Main.UI.Controls.Playing;
using Game.Workshop.Editing.Core;
using Tiles.Ground;
using UI.Screens;
using UnityEngine;

namespace LevelEditor.LevelEditor.Options
{
    public class TerrainWorkshopEditorOption : BaseEditorOption
    {
        private readonly ITerrainLevelEditor terrainEditor;

        private EditorOptionUI editorOptionUI;
        private TerrainType selectedTerrainType;

        private TerrainEditorOptionData TerrainEditorOptionData => (TerrainEditorOptionData)EditorOptionData;

        protected TerrainWorkshopEditorOption(ILevelEditorController levelEditorController,
            EditorOptionDataLibrary editorOptionDataLibrary, ITerrainLevelEditor terrainEditor)
        {
            this.terrainEditor = terrainEditor;
            EditorOptionData = editorOptionDataLibrary.TerrainEditorOptionData;

            SetupUI(levelEditorController);
            SetAlternatives(TerrainEditorOptionData.AlternativeTerrains);
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
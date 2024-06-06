using System.Collections.Generic;
using System.Linq;
using Level;
using Level.Data;
using Tiles.Editing.Options;
using UnityEngine;
using Utility;

namespace Tiles.Editing
{
    public class InGameTilemapEditor : BaseTilemapEditor
    {
        [SerializeField]
        private TileLibraryData tileLibraryData;

        private RoadEditor roadEditor;
        private TerrainEditor terrainEditor;
        private InGameLogisticEditor inGameLogisticEditor;

        private ITileLibrary tileLibrary;
        private TilemapEditorUI tilemapEditorUI;

        public void Setup(TilemapEditorUI tilemapEditorUI)
        {
            tileLibraryData.Init();
            
            tileLibrary = tileLibraryData;
            roadEditor = new RoadEditor(terrainTilemap, roadTilemap, tileLibrary, false);
            terrainEditor = new TerrainEditor(terrainTilemap, tileLibrary);
            inGameLogisticEditor = new InGameLogisticEditor(logisticTilemap, tileLibrary);

            TileEditors = new List<ITileEditor> {
                roadEditor
            };
            SelectedEditor = TileEditors.First();

            this.tilemapEditorUI = tilemapEditorUI;
            this.tilemapEditorUI.SetData(TileEditors.SelectMany(editor => editor.GetOptions()).ToList());
            this.tilemapEditorUI.SelectedValueChanged += OnSelectedValueChanged;
        }

        private void OnDestroy()
        {
            tilemapEditorUI.SelectedValueChanged -= OnSelectedValueChanged;
        }

        private void OnSelectedValueChanged(BaseEditorOption option)
        {
            SelectedEditor = option.TileEditor;
        }

        public override void LoadLevel(LevelData levelData)
        {
            terrainEditor.Load(levelData.terrainTilesData);
            roadEditor.Load(levelData.roadTileData);
            inGameLogisticEditor.Load(levelData.logisticData);
        }

        public Vector3 CellToWorldPos(Vector3Int tilePos) => terrainTilemap.CellToWorldCenter(tilePos);
        public Vector3 CellToWorldPos(Vector2Int tilePos) => terrainTilemap.CellToWorldCenter(tilePos);

        protected override Vector3Int MouseToTilePosition(Vector3 mousePos)
        {
            return terrainTilemap.WorldToCell(mousePos);
        }
    }
}
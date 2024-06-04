using System.Collections.Generic;
using System.Linq;
using Level;
using Level.Data;
using Tiles.Editing.Options;
using Tiles.Editing.Workshop;
using UnityEngine;
using Utility;

namespace Tiles.Editing
{
    public class InGameTilemapEditor : BaseTilemapEditor
    {
        [SerializeField]
        private TilemapEditorUI tilemapEditorUI;
        
        [SerializeField]
        private TileLibraryData tileLibraryData;

        private RoadEditor roadEditor;
        private TerrainEditor terrainEditor;
        private LogisticEditor logisticEditor;
        
        private ITileLibrary tileLibrary;

        public override void Setup()
        {
            tileLibraryData.Init();
            
            tileLibrary = tileLibraryData;
            roadEditor = new RoadEditor(terrainTilemap, roadTilemap, tileLibrary, false);
            terrainEditor = new TerrainEditor(terrainTilemap, tileLibrary);
            logisticEditor = new LogisticEditor(logicTilemap, tileLibrary);

            TileEditors = new List<ITileEditor> {
                roadEditor
            };
            SelectedEditor = TileEditors.First();
            
            tilemapEditorUI.SetData(TileEditors.SelectMany(editor => editor.GetOptions()).ToList());
            tilemapEditorUI.SelectedValueChanged += OnSelectedValueChanged;
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
            logisticEditor.Load(levelData.pathsData);
        }

        public Vector3 CellToWorldPos(Vector3Int tilePos) => terrainTilemap.CellToWorldCenter(tilePos);

        protected override Vector3Int MouseToTilePosition(Vector3 mousePos)
        {
            return terrainTilemap.WorldToCell(mousePos);
        }
    }
}
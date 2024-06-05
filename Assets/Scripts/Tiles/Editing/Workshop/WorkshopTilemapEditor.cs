using System.Collections.Generic;
using System.Linq;
using Level;
using Level.Data;
using Tiles.Editing.Options;
using UnityEditor.Rendering.Universal;
using UnityEngine;

namespace Tiles.Editing.Workshop
{
    public class WorkshopTilemapEditor : BaseTilemapEditor
    {
        [SerializeField]
        private TilemapEditorUI tilemapEditorUI;
        
        [SerializeField]
        private TileLibraryData tileLibraryData;

        private ITileLibrary tileLibrary;
        
        private RoadEditor roadEditor;
        private TerrainEditor terrainEditor;
        private LogisticEditor logisticEditor;

        private string levelName;

        public void Setup(TilemapEditorUI tilemapEditorUI)
        {
            tileLibraryData.Init();
            tileLibrary = tileLibraryData;
            
            roadEditor = new RoadEditor(terrainTilemap, roadTilemap, tileLibrary, true);
            terrainEditor = new TerrainEditor(terrainTilemap, tileLibrary);
            logisticEditor = new LogisticEditor(logicTilemap, tileLibrary);

            TileEditors = new List<ITileEditor> {
                terrainEditor,
                roadEditor,
                logisticEditor
            };

            this.tilemapEditorUI = tilemapEditorUI;
            this.tilemapEditorUI.SetData(TileEditors.SelectMany(editor => editor.GetOptions()).ToList());
            this.tilemapEditorUI.SelectedValueChanged += OnSelectedTileEditorChanged;

            SelectedEditor = terrainEditor;
        }

        private void OnDestroy()
        {
            tilemapEditorUI.SelectedValueChanged -= OnSelectedTileEditorChanged;
        }

        public LevelData SaveLevel()
        {
            return new LevelData {
                levelName = levelName,
                roadTileData = roadEditor.Save(),
                terrainTilesData = terrainEditor.Save(),
                pathsData = logisticEditor.Save()
            };
        }

        public override void LoadLevel(LevelData levelData)
        {
            levelName = levelData.levelName;
            roadEditor.Load(levelData.roadTileData);
            terrainEditor.Load(levelData.terrainTilesData);
            logisticEditor.Load(levelData.pathsData);
        }
        
        private void OnSelectedTileEditorChanged(BaseEditorOption editorOption)
        {
            SelectedEditor = editorOption.TileEditor;
            SelectedEditor.SetOption(editorOption);
        }

        protected override Vector3Int MouseToTilePosition(Vector3 mousePos)
        {
            return logicTilemap.WorldToCell(mousePos);
        }
    }
}
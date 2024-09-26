using System.Collections.Generic;
using System.Linq;
using Cars;
using Level;
using Level.Data;
using Tiles.Editing.Options;
using UnityEngine;

namespace Tiles.Editing.Workshop
{
    public class WorkshopTilemapEditor : BaseTilemapEditor
    {
        [SerializeField]
        private TilemapEditorUI tilemapEditorUI;
        
        [SerializeField]
        private TileLibraryData tileLibraryData;

        [SerializeField]
        private EditorSpawnPointView spawnPointViewPrefab;

        [SerializeField]
        private CarLibrary carLibrary;

        private Camera mainCamera;

        private ITileLibrary tileLibrary;
        
        private RoadEditor roadEditor;
        private TerrainEditor terrainEditor;
        private WorkshopLogisticEditor workshopLogisticEditor;
        private ObstacleEditor obstacleEditor;

        private string levelName;

        public void Setup(TilemapEditorUI tilemapEditorUI)
        {
            tileLibraryData.Init();
            tileLibrary = tileLibraryData;
            
            mainCamera = Camera.main;

            roadEditor = new RoadEditor(roadTilemap, terrainTilemap, obstacleTilemap, tileLibrary, true);
            terrainEditor = new TerrainEditor(terrainTilemap, tileLibrary);
            workshopLogisticEditor = new WorkshopLogisticEditor(logisticTilemap, tileLibrary, spawnPointViewPrefab, carLibrary, new RectTransform());
            obstacleEditor = new ObstacleEditor(obstacleTilemap, tileLibrary);
            
            TileEditors = new List<ITileEditor> {
                terrainEditor,
                roadEditor,
                workshopLogisticEditor,
                obstacleEditor
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
                //roadTileData = roadEditor.Save(),
                terrainTilesData = terrainEditor.Save(),
                logisticData = workshopLogisticEditor.Save(),
                obstaclesData = obstacleEditor.Save()
            };
        }

        public override void LoadLevel(LevelData levelData)
        {
            levelName = levelData.levelName;
            //roadEditor.Load(levelData.roadTileData);
            terrainEditor.Load(levelData.terrainTilesData);
            workshopLogisticEditor.Load(levelData.logisticData);
            obstacleEditor.Load(levelData.obstaclesData);
        }

        public void ChangeCameraScale(float scale)
        {
            mainCamera.orthographicSize = scale;
        }
        
        private void OnSelectedTileEditorChanged(BaseEditorOption editorOption)
        {
            SelectedEditor = editorOption.TileEditor;
            SelectedEditor.SetOption(editorOption);
        }

        protected override Vector3Int MouseToTilePosition(Vector3 mousePos)
        {
            return logisticTilemap.WorldToCell(mousePos);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Level;
using Tiles;
using Tiles.Ground;
using Tiles.Options;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class TilemapEditor : MonoBehaviour, ILevelSaver, ILevelLoader
{
    [SerializeField]
    private TilemapEditorUI tilemapEditorUI;

    [SerializeField]
    private Tilemap terrainTilemap;
    
    [SerializeField]
    private Tilemap roadTilemap;
    
    [SerializeField]
    private Tilemap uiTilemap;
    
    [SerializeField]
    private TileLibraryData tileLibraryData;
        
    private ITileLibrary tileLibrary;

    private Camera mainCamera;
    
    private RoadEditor roadEditor;
    private TerrainEditor terrainEditor;
    private UIEditor uiEditor;

    private List<ITileEditor> tileEditors;
    private ITileEditor selectedEditor;
    
    private void Awake()
    {
        tileLibraryData.Init();
        tileLibrary = tileLibraryData;
        mainCamera = Camera.main;
        roadEditor = new RoadEditor(terrainTilemap, roadTilemap, tileLibrary);
        terrainEditor = new TerrainEditor(terrainTilemap, tileLibrary);
        uiEditor = new UIEditor(uiTilemap, tileLibrary);

        tileEditors = new List<ITileEditor> {
            terrainEditor,
            roadEditor,
            uiEditor
        };
        
        tilemapEditorUI.SetData(tileEditors.SelectMany(editor => editor.GetOptions()).ToList());
        tilemapEditorUI.SelectedValueChanged += OnSelectedTileEditorChanged;
        
        selectedEditor = terrainEditor;
    }

    private void OnDestroy()
    {
        tilemapEditorUI.SelectedValueChanged -= OnSelectedTileEditorChanged;
    }

    private void Update()
    {
        var pointerPosition = Input.mousePosition;
        var tilePosition = roadTilemap.WorldToCell(mainCamera.ScreenToWorldPoint(pointerPosition));

        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            selectedEditor.OnTileDown(tilePosition);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            selectedEditor.OnTileUp();
        }

        if (Input.GetKey(KeyCode.Mouse0)) {
            selectedEditor.OnTileMove(tilePosition);
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            selectedEditor.OnTileEraseDown(tilePosition);
        }

        if (Input.GetKey(KeyCode.Mouse1)) {
            selectedEditor.OnTileEraseMove(tilePosition);
        }
    }

    private void OnSelectedTileEditorChanged(BaseEditorOption editorOption)
    {
        selectedEditor = editorOption.TileEditor;
        selectedEditor.SetOption(editorOption);
    }

    public LevelData SaveLevel()
    {
        return new LevelData {
            roadTileData = roadEditor.Save(),
            terrainTilesData = terrainEditor.Save(),
            uiTilesData = uiEditor.Save()
        };
    }

    public void LoadLevel(LevelData levelData)
    {
        roadEditor.Load(levelData.roadTileData);
        terrainEditor.Load(levelData.terrainTilesData);
        uiEditor.Load(levelData.uiTilesData);
    }
}
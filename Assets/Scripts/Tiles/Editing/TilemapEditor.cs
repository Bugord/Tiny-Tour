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

public class TilemapEditor : MonoBehaviour
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
    private ITileEditor currentEditor;

    private bool isSelecting;

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
        
        currentEditor = terrainEditor;
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
            currentEditor.OnTileDown(tilePosition);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0)) {
            currentEditor.OnTileUp();
        }

        if (Input.GetKey(KeyCode.Mouse0)) {
            currentEditor.OnTileMove(tilePosition);
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            currentEditor.OnTileEraseDown(tilePosition);
        }

        if (Input.GetKey(KeyCode.Mouse1)) {
            currentEditor.OnTileEraseMove(tilePosition);
        }
    }

    public void Reload()
    {
        roadEditor.Reload();
    }

    private void OnSelectedTileEditorChanged(BaseEditorOption editorOption)
    {
        currentEditor = editorOption.TileEditor;
        currentEditor.SetOption(editorOption);
    }
}
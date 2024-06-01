using System;
using System.Collections.Generic;
using Level;
using Tiles;
using Tiles.Ground;
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
    private TileLibraryData tileLibraryData;
        
    private ITileLibrary tileLibrary;

    private Camera mainCamera;
    
    private RoadEditor roadEditor;
    private TerrainEditor groundEditor;
    private TerrainEditor bridgeBaseEditor;

    private List<ITileEditor> tileEditors;
    private ITileEditor currentEditor;

    private bool isSelecting;

    private void Awake()
    {
        tileLibraryData.Init();
        tileLibrary = tileLibraryData;
        mainCamera = Camera.main;
        roadEditor = new RoadEditor(terrainTilemap, roadTilemap, tileLibrary);
        groundEditor = new TerrainEditor(terrainTilemap, tileLibrary, TerrainType.Ground);
        bridgeBaseEditor = new TerrainEditor(terrainTilemap, tileLibrary, TerrainType.BridgeBase);

        tileEditors = new List<ITileEditor> {
            groundEditor,
            roadEditor,
            bridgeBaseEditor
        };
        
        tilemapEditorUI.SetData(tileEditors.Count);
        tilemapEditorUI.SelectedValueChanged += OnSelectedTileEditorChanged;
        
        currentEditor = groundEditor;
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

    private void OnSelectedTileEditorChanged(int editorIndex)
    {
        currentEditor = tileEditors[editorIndex];
    }
}
using Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapEditor : MonoBehaviour
{
    [SerializeField]
    private Tilemap roadTilemap;

    [SerializeField]
    private Tilemap terrainTilemap;
    
    [SerializeField]
    private RoadTile roadTile;

    private Camera mainCamera;
    
    private RoadEditor roadEditor;
    
    private ITileEditor currentEditor;

    private bool isSelecting;

    private void Awake()
    {
        mainCamera = Camera.main;
        roadEditor = new RoadEditor(terrainTilemap, roadTilemap, roadTile);

        currentEditor = roadEditor;
    }

    private void Update()
    {
        var pointerPosition = Input.mousePosition;
        var tilePosition = roadTilemap.WorldToCell(mainCamera.ScreenToWorldPoint(pointerPosition));

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
}
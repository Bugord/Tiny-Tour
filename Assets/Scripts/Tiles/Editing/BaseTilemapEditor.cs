using System.Collections.Generic;
using Level;
using Level.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace Tiles.Editing
{
    public abstract class BaseTilemapEditor : MonoBehaviour, ILevelLoader
    {
        [SerializeField]
        protected Tilemap terrainTilemap;

        [SerializeField]
        protected Tilemap roadTilemap;

        [SerializeField]
        protected Tilemap logicTilemap;
        
        private Camera mainCamera;

        protected List<ITileEditor> TileEditors;
        protected ITileEditor SelectedEditor;

        protected virtual void Awake()
        {
            mainCamera = Camera.main;
        }

        protected virtual void Update()
        {
            var pointerPosition = Input.mousePosition;
            var tilePosition = MouseToTilePosition(mainCamera.ScreenToWorldPoint(pointerPosition));

            if (EventSystem.current.IsPointerOverGameObject()) {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                SelectedEditor.OnTileDown(tilePosition);
            }

            if (Input.GetKeyUp(KeyCode.Mouse0)) {
                SelectedEditor.OnTileUp();
            }

            if (Input.GetKey(KeyCode.Mouse0)) {
                SelectedEditor.OnTileMove(tilePosition);
            }

            if (Input.GetKeyDown(KeyCode.Mouse1)) {
                SelectedEditor.OnTileEraseDown(tilePosition);
            }

            if (Input.GetKey(KeyCode.Mouse1)) {
                SelectedEditor.OnTileEraseMove(tilePosition);
            }
        }

        protected abstract Vector3Int MouseToTilePosition(Vector3 mousePos);
        
        public abstract void LoadLevel(LevelData levelData);
    }
}
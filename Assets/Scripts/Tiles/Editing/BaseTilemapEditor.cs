using System.Collections.Generic;
using Level;
using Level.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace Tiles.Editing
{
    public abstract class BaseTilemapEditor
    {
        [SerializeField]
        protected Tilemap terrainTilemap;

        [SerializeField]
        protected Tilemap roadTilemap;

        [SerializeField]
        protected Tilemap logisticTilemap;

        [SerializeField]
        protected Tilemap obstacleTilemap;

        private Camera mainCamera;

        protected List<ITileEditor> TileEditors;
        protected ITileEditor SelectedEditor;

        private bool isPressed;

        protected virtual void Awake()
        {
            mainCamera = Camera.main;
        }

        public void TouchProcess(InputAction.CallbackContext callbackContext)
        {
            var pointerPosition = Pointer.current.position.ReadValue();
            var tilePosition = MouseToTilePosition(mainCamera.ScreenToWorldPoint(pointerPosition));
            if (callbackContext.action.WasPressedThisFrame()) {
                SelectedEditor.OnTileDown(tilePosition);
                isPressed = true;
            }
            else if (callbackContext.action.WasReleasedThisFrame()) {
                SelectedEditor.OnTileUp();
                isPressed = false;
            }
        }

        protected virtual void Update()
        {
            if (isPressed) {
                var pointerPosition = Pointer.current.position.ReadValue();
                var tilePosition = MouseToTilePosition(mainCamera.ScreenToWorldPoint(pointerPosition));
                
                SelectedEditor.OnTileMove(tilePosition);
            }
            // var tilePosition = MouseToTilePosition(mainCamera.ScreenToWorldPoint(pointerPosition));
            //
            // if (EventSystem.current.IsPointerOverGameObject()) {
            //     return;
            // }
            //
            // if (Pointer.current.IsPressed()) {
            //     SelectedEditor.OnTileDown(tilePosition);
            // }
            //
            // if (!Pointer.current.IsPressed()) {
            // }
            //
            // if (Pointer.current.IsPressed()) {
            // }
            //
            // if (Input.GetKeyDown(KeyCode.Mouse1)) {
            //     SelectedEditor.OnTileEraseDown(tilePosition);
            // }
            //
            // if (Input.GetKey(KeyCode.Mouse1)) {
            //     SelectedEditor.OnTileEraseMove(tilePosition);
            // }
        }

        protected abstract Vector3Int MouseToTilePosition(Vector3 mousePos);

        public abstract void LoadLevel(LevelData levelData);
    }
}
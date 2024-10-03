using System;
using Common.Tilemaps;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Zenject;

namespace Common
{
    public class TilemapInput : ITilemapInput, IInitializable, IDisposable, ITickable
    {
        public event Action<Vector3Int> TilePressDown;
        public event Action<Vector3Int> TilePressUp;
        public event Action<Vector3Int> TileDragged;

        public event Action<Vector3Int> TileAltPressDown;
        public event Action<Vector3Int> TileAltPressUp;
        public event Action<Vector3Int> TileAltDragged;

        private readonly ITilemapPositionConverter tilemapPositionConverter;
        private readonly InputActions inputActions;

        private Vector2 lastPos;
        private Vector3Int lastTilePos;

        private bool clickedOnTile;

        public bool IsMainPressed => inputActions.Player.MainInteraction.IsPressed();

        public TilemapInput(ITilemapPositionConverter tilemapPositionConverter)
        {
            this.tilemapPositionConverter = tilemapPositionConverter;

            inputActions = new InputActions();
        }

        public void Initialize()
        {
            var playerActions = inputActions.Player;

            playerActions.MainInteraction.started += OnPressStarted;
            playerActions.MainInteraction.performed += OnPressPerformed;
            playerActions.MainInteraction.canceled += OnPressCanceled;

            playerActions.AlternativeInteraction.started += OnAltPressStarted;
            playerActions.AlternativeInteraction.performed += OnAltPressPerformed;
            playerActions.AlternativeInteraction.canceled += OnAltPressCanceled;

            playerActions.MainInteraction.Enable();
            playerActions.AlternativeInteraction.Enable();
        }

        public void Dispose()
        {
            var playerActions = inputActions.Player;

            playerActions.MainInteraction.Disable();
            playerActions.AlternativeInteraction.Disable();

            playerActions.MainInteraction.started -= OnPressStarted;
            playerActions.MainInteraction.performed -= OnPressPerformed;
            playerActions.MainInteraction.canceled -= OnPressCanceled;

            playerActions.AlternativeInteraction.started -= OnAltPressStarted;
            playerActions.AlternativeInteraction.performed -= OnAltPressPerformed;
            playerActions.AlternativeInteraction.canceled -= OnAltPressCanceled;
        }

        public void Tick()
        {
            DoNotClickUIAndGameAtSameTime();
        }

        private void OnPressStarted(InputAction.CallbackContext ctx)
        {
            clickedOnTile = true;

            var pressPosition = ctx.ReadValue<Vector2>();
            var worldPressPosition = Camera.main.ScreenToWorldPoint(pressPosition);
            var tilePosition = tilemapPositionConverter.WorldToCell(worldPressPosition);

            lastTilePos = tilePosition;

            TilePressDown?.Invoke(tilePosition);
        }

        private void OnPressPerformed(InputAction.CallbackContext ctx)
        {
            if (!clickedOnTile) {
                return;
            }

            var pressPosition = ctx.ReadValue<Vector2>();
            var worldPressPosition = Camera.main.ScreenToWorldPoint(pressPosition);
            var tilePosition = tilemapPositionConverter.WorldToCell(worldPressPosition);

            if (lastTilePos == tilePosition) {
                return;
            }

            lastTilePos = tilePosition;

            TileDragged?.Invoke(tilePosition);
        }

        private void OnPressCanceled(InputAction.CallbackContext ctx)
        {
            if (!clickedOnTile) {
                return;
            }

            TilePressUp?.Invoke(lastTilePos);

            clickedOnTile = false;
        }

        private void OnAltPressStarted(InputAction.CallbackContext ctx)
        {
            clickedOnTile = true;

            var pressPosition = ctx.ReadValue<Vector2>();
            var worldPressPosition = Camera.main.ScreenToWorldPoint(pressPosition);
            var tilePosition = tilemapPositionConverter.WorldToCell(worldPressPosition);

            lastTilePos = tilePosition;

            TileAltPressDown?.Invoke(tilePosition);
        }

        private void OnAltPressPerformed(InputAction.CallbackContext ctx)
        {
            if (!clickedOnTile) {
                return;
            }

            var pressPosition = ctx.ReadValue<Vector2>();
            var worldPressPosition = Camera.main.ScreenToWorldPoint(pressPosition);
            var tilePosition = tilemapPositionConverter.WorldToCell(worldPressPosition);

            if (lastTilePos == tilePosition) {
                return;
            }

            lastTilePos = tilePosition;

            TileAltDragged?.Invoke(tilePosition);
        }

        private void OnAltPressCanceled(InputAction.CallbackContext ctx)
        {
            if (!clickedOnTile) {
                return;
            }

            TileAltPressUp?.Invoke(lastTilePos);

            clickedOnTile = false;
        }

        private void DoNotClickUIAndGameAtSameTime()
        {
            if (EventSystem.current.IsPointerOverGameObject()) {
                inputActions.Player.Disable();
            }
            else {
                inputActions.Player.Enable();
            }
        }
    }
}
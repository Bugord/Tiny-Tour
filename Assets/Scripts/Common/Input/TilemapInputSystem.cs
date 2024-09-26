using System;
using Common.Tilemaps;
using Core.Logging;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Common
{
    public class TilemapInput : ITilemapInput, IInitializable, IDisposable
    {
        public event Action<Vector3Int> TilePressDown;
        public event Action<Vector3Int> TilePressUp;
        public event Action<Vector3Int> TileHovered;

        private readonly ITilemapPositionConverter tilemapPositionConverter;
        private readonly InputActions inputActions;

        private Vector2 lastPos;
        private Vector3Int lastTilePos;

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

            playerActions.Enable();
        }

        public void Dispose()
        {
            var playerActions = inputActions.Player;

            playerActions.Disable();

            playerActions.MainInteraction.started -= OnPressStarted;
            playerActions.MainInteraction.performed -= OnPressPerformed;
            playerActions.MainInteraction.canceled -= OnPressCanceled;
        }

        private void OnPressStarted(InputAction.CallbackContext ctx)
        {
            var pressPosition = ctx.ReadValue<Vector2>();
            var worldPressPosition = Camera.main.ScreenToWorldPoint(pressPosition);
            var tilePosition = tilemapPositionConverter.WorldToCell(worldPressPosition);

            TilePressDown?.Invoke(tilePosition);
        }

        private void OnPressPerformed(InputAction.CallbackContext ctx)
        {
            var pressPosition = ctx.ReadValue<Vector2>();
            var worldPressPosition = Camera.main.ScreenToWorldPoint(pressPosition);
            var tilePosition = tilemapPositionConverter.WorldToCell(worldPressPosition);

            if (lastTilePos == tilePosition) {
                return;
            }
            
            lastTilePos = tilePosition;

            TileHovered?.Invoke(tilePosition);
        }

        private void OnPressCanceled(InputAction.CallbackContext ctx)
        {
            TilePressUp?.Invoke(lastTilePos);
        }
    }
}
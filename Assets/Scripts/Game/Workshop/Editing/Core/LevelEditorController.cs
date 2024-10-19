using Common;
using Game.Common.EditorOptions;
using Game.Gameplay.Editing.Options.Model;
using Game.Workshop.Editing.Options;
using UnityEngine;
using Zenject;

namespace Game.Workshop.Editing.Core
{
    public class LevelEditorController : IInitializable, ILevelEditorController
    {
        private readonly ITilemapInput tilemapInput;
        private readonly IEditorOptionsController editorOptionsController;
        
        private BaseEditorOption cachedEditorOption;
        
        public LevelEditorController(ITilemapInput tilemapInput, IEditorOptionsController editorOptionsController)
        {
            this.tilemapInput = tilemapInput;
            this.editorOptionsController = editorOptionsController;
        }

        public void Initialize()
        {
            editorOptionsController.AddOption<TerrainWorkshopEditorOption>();
            editorOptionsController.AddOption<CarSpawnPointEditorOption>();
            editorOptionsController.AddOption<GoalSpawnPointEditorOption>();
            editorOptionsController.AddOption<RoadEditorOption>();
            editorOptionsController.AddOption<ErasingWorkshopEditorOption>();

            editorOptionsController.SelectOption<TerrainWorkshopEditorOption>();
        }

        public void EnableEditing()
        {
            tilemapInput.TileDragged += OnTileDragged;
            tilemapInput.TilePressDown += OnTileDown;
            tilemapInput.TilePressUp += OnTileUp;

            tilemapInput.TileAltDragged += OnTileAltDragged;
            tilemapInput.TileAltPressDown += OnTileAltDown;
            tilemapInput.TileAltPressUp += OnTileAltUp;
        }

        public void DisableEditing()
        {
            tilemapInput.TileDragged -= OnTileDragged;
            tilemapInput.TilePressDown -= OnTileDown;
            tilemapInput.TilePressUp -= OnTileUp;

            tilemapInput.TileAltDragged -= OnTileAltDragged;
            tilemapInput.TileAltPressDown -= OnTileAltDown;
            tilemapInput.TileAltPressUp -= OnTileAltUp;
        }

        private void OnTileAltDown(Vector2Int tilePos)
        {
            cachedEditorOption = editorOptionsController.SelectedOption;
            editorOptionsController.SelectOption<ErasingWorkshopEditorOption>();
            
            editorOptionsController.SelectedOption.OnAltTileDown(tilePos);
        }

        private void OnTileAltDragged(Vector2Int tilePos)
        {
            editorOptionsController.SelectedOption.OnAltTileDrag(tilePos);
        }

        private void OnTileAltUp(Vector2Int tilePos)
        {
            editorOptionsController.SelectedOption.OnAltTileUp(tilePos);
            
            editorOptionsController.SelectOption(cachedEditorOption);
            cachedEditorOption = null;
        }

        private void OnTileUp(Vector2Int tilePos)
        {
            editorOptionsController.SelectedOption.OnTileUp(tilePos);
        }

        private void OnTileDown(Vector2Int tilePos)
        {
            editorOptionsController.SelectedOption.OnTileDown(tilePos);
        }

        private void OnTileDragged(Vector2Int tilePos)
        {
            editorOptionsController.SelectedOption.OnTileDrag(tilePos);
        }
    }
}
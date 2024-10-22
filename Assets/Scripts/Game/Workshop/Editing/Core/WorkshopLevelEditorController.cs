using Common;
using Game.Common.EditorController;
using Game.Common.EditorOptions;
using Game.Gameplay.Editing.Options.Model;
using Game.Workshop.Editing.Options;
using UnityEngine;

namespace Game.Workshop.Editing.Core
{
    public class WorkshopLevelEditorController : BaseLevelEditorController
    {
        private BaseEditorOption cachedEditorOption;

        public WorkshopLevelEditorController(ITilemapInput tilemapInput,
            IEditorOptionsController editorOptionsController) : base(tilemapInput, editorOptionsController)
        {
        }

        protected override void AddOptions()
        {
            EditorOptionsController.AddOption<TerrainWorkshopEditorOption>();
            EditorOptionsController.AddOption<CarSpawnPointEditorOption>();
            EditorOptionsController.AddOption<GoalSpawnPointEditorOption>();
            EditorOptionsController.AddOption<RoadEditorOption>();
            EditorOptionsController.AddOption<ObstacleEditorOption>();
            EditorOptionsController.AddOption<ErasingWorkshopEditorOption>();

            EditorOptionsController.SelectOption<TerrainWorkshopEditorOption>();
        }

        protected override void OnTileAltDown(Vector2Int tilePos)
        {
            cachedEditorOption = EditorOptionsController.SelectedOption;
            EditorOptionsController.SelectOption<ErasingWorkshopEditorOption>();

            base.OnTileAltDown(tilePos);
        }

        protected override void OnTileAltUp(Vector2Int tilePos)
        {
            base.OnTileAltUp(tilePos);

            EditorOptionsController.SelectOption(cachedEditorOption);
            cachedEditorOption = null;
        }
    }
}
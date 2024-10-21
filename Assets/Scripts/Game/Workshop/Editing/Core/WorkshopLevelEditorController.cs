using Common;
using Game.Common.EditorController;
using Game.Common.EditorOptions;
using Game.Gameplay.Editing.Options.Model;
using Game.Workshop.Editing.Options;

namespace Game.Workshop.Editing.Core
{
    public class WorkshopLevelEditorController : BaseLevelEditorController
    {
        public WorkshopLevelEditorController(ITilemapInput tilemapInput, IEditorOptionsController editorOptionsController) : base(tilemapInput, editorOptionsController)
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
    }
}
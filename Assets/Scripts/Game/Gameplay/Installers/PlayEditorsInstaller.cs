using Common;
using Common.Editors;
using Common.Editors.Obstacles;
using Common.Editors.Terrain;
using Game.Common.EditorOptions;
using Game.Common.Editors.Goals;
using Game.Common.Editors.Road;
using Game.Gameplay.Editing.Editors;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "installer_play_editors", menuName = "Installers/Play Editor")]
    public class PlayEditorsInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ITerrainService>().To<TerrainService>().AsSingle();
            Container.Bind<IRoadLevelEditor>().To<PlayRoadLevelLevelEditor>().AsSingle();
            Container.Bind<ITerrainLevelEditor>().To<TerrainLevelEditor>().AsSingle();
            Container.Bind<IGoalLevelEditor>().To<GoalLevelEditor>().AsSingle();
            Container.Bind<IObstaclesEditor>().To<ObstaclesEditor>().AsSingle();
            Container.BindInterfacesTo<GameplayEditorOptionsControllerUIProvider>().AsCached();
        }
    }
}
using Common;
using Common.Editors;
using Common.Editors.Logistic;
using Common.Editors.Obstacles;
using Common.Editors.Road;
using Common.Editors.Terrain;
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
            Container.Bind<IObstaclesEditor>().To<ObstaclesEditor>().AsSingle();
            
            Container.Bind<IRoadEditor>().To<RoadEditor>().AsSingle();
            Container.Bind<ITerrainEditor>().To<TerrainEditor>().AsSingle();
            Container.Bind<IGoalEditor>().To<GoalEditor>().AsSingle();
        }
    }
}
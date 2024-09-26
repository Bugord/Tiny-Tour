using Common;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "installer_play_editors", menuName = "Installers/Play Editor")]
    public class PlayEditorsInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ITerrainEditor>().To<TerrainEditor>().AsSingle();
            Container.Bind<IObstaclesEditor>().To<ObstaclesEditor>().AsSingle();
            Container.Bind<ILogisticEditor>().To<LogisticEditor>().AsSingle();
        }
    }
}
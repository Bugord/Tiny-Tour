using Common;
using Common.Editors;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "installer_play_editors", menuName = "Installers/Play Editor")]
    public class PlayEditorsInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ITerrainLoader>().To<TerrainLoader>().AsSingle();
            Container.Bind<IObstaclesEditor>().To<ObstaclesEditor>().AsSingle();
        }
    }
}
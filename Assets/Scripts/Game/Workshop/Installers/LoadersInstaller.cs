using Common.Editors;
using Gameplay.Logistic;
using UnityEngine;
using Zenject;

namespace Game.Workshop.Installers
{
    [CreateAssetMenu(fileName = "installer_level_editor_loaders", menuName = "Installers/Level Editor/Loader")]
    public class LoadersInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ITerrainService>().To<TerrainService>().AsSingle();
            Container.Bind<ILogisticLoader>().To<LogisticService>().AsSingle();
        }
    }
}
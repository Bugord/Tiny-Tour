using Common.Editors;
using Gameplay.Logistic;
using UnityEngine;
using Zenject;

namespace LevelEditor.Installers
{
    [CreateAssetMenu(fileName = "installer_level_editor_loaders", menuName = "Installers/Level Editor/Loader")]
    public class LoadersInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ITerrainLoader>().To<TerrainLoader>().AsSingle();
            Container.Bind<ILogisticLoader>().To<LogisticService>().AsSingle();
        }
    }
}
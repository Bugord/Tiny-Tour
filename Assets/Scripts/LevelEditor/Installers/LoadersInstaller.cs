using Common.Editors;
using Common.Level.Core;
using LevelEditor.Level.Core;
using UnityEngine;
using Zenject;

namespace LevelEditor.Installers
{
    [CreateAssetMenu(fileName = "installer_level_editor_loaders", menuName = "Installers/Level Editor/Loader")]
    public class LoadersInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ILevelLoader>().To<LevelEditorService>().AsSingle();
            Container.Bind<ITerrainLoader>().To<TerrainLoader>().AsSingle();
        }
    }
}
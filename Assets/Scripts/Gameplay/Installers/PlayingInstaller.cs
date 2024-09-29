using Common;
using Gameplay.Editing.Editors;
using Gameplay.Editing.Editors.Terrain;
using UnityEngine;
using Zenject;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "installer_playing", menuName = "Installers/Playing")]
    public class PlayingInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            // Container.Bind<InGameTilemapEditor>().AsSingle();
            Container.BindInterfacesTo<PlayLevelService>().AsSingle().NonLazy();
            Container.Bind<ILevelLoader>().To<LevelLoader>().AsSingle();

            Container.Bind<IRoadEditor>().To<RoadEditor>().AsSingle();
            Container.Bind<ITerrainEditor>().To<TerrainEditor>().AsSingle();
            
            Container.Bind<IEditorOptionFactory>().To<EditorOptionOptionFactory>().AsTransient();
            Container.BindInterfacesAndSelfTo<InGameEditor>().AsSingle();
        }
    }
}
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
        }
    }
}
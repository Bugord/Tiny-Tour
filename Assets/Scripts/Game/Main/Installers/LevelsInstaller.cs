using Level;
using UnityEngine;
using Zenject;

namespace Game.Main.Installers
{
    [CreateAssetMenu(fileName = "installer_levels", menuName = "Installers/Main/Levels")]
    public class LevelsInstaller : ScriptableObjectInstaller
    {
        [SerializeField]
        private LevelLibrary levelLibrary;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LevelManager>().AsSingle();
            Container.Bind<ILevelProvider>().To<LevelLibrary>().FromInstance(levelLibrary);
        }
    }
}
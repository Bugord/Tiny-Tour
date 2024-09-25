using Level;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    [CreateAssetMenu(fileName = "installer_levels", menuName = "Installers/Levels")]
    public class LevelsInstaller : ScriptableObjectInstaller
    {
        [SerializeField]
        private LevelLibrary levelLibrary;
        
        public override void InstallBindings()
        {
            Container.Bind<LevelManager>().AsSingle();
            Container.Bind<ILevelProvider>().To<LevelLibrary>().FromInstance(levelLibrary);
        }
    }
}
using Cars;
using Gameplay.Cars;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Installers
{
    public class CarsInstaller : MonoInstaller
    {
        [SerializeField]
        private Transform carsRoot;

        [SerializeField]
        private Car carPrefab;

        [SerializeField]
        private CarLibrary carLibrary;

        public override void InstallBindings()
        {
            Container.Bind<ICarsService>().To<CarsService>().AsSingle();
            Container.Bind<ICarsFactory>().To<CarsFactory>().AsSingle().WithArguments(carsRoot, carPrefab);
            Container.Bind<CarLibrary>().FromInstance(carLibrary);
        }
    }
}
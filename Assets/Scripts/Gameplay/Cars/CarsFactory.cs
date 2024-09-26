using Cars;
using Core;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Gameplay.Cars
{
    public class CarsFactory : ICarsFactory
    {
        private readonly Transform carsRoot;
        private readonly Car carPrefab;
        private readonly CarLibrary carLibrary;
        private readonly DiContainer diContainer;

        public CarsFactory(DiContainer diContainer, Transform carsRoot, Car carPrefab, CarLibrary carLibrary)
        {
            this.carsRoot = carsRoot;
            this.carPrefab = carPrefab;
            this.carLibrary = carLibrary;
            this.diContainer = diContainer;
        }

        public Car Create(Vector3 position, Direction direction, CarType carType)
        {
            var carData = carLibrary.GetCarData(carType);
            var car = diContainer.InstantiatePrefabForComponent<Car>(carPrefab, position, quaternion.identity, carsRoot);
            car.SetData(carData);
            car.SetDirection(direction);

            return car;
        }
    }
}
using System.Collections.Generic;
using Cars;
using Game.Common.Level.Data;

namespace Gameplay.Cars
{
    public interface ICarsService
    {
        IReadOnlyCollection<Car> Cars { get; }
        void SpawnCars(CarSpawnData[] carsSpawnData);
        void ResetCars();
    }
}
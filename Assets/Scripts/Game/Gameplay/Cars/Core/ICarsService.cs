using System.Collections.Generic;
using Game.Common.Level.Data;
using Game.Gameplay.Cars.Model;

namespace Game.Gameplay.Cars.Core
{
    public interface ICarsService
    {
        IReadOnlyCollection<Car> Cars { get; }
        void SpawnCars(CarSpawnData[] carsSpawnData);
        void ResetCars();
        void Clear();
    }
}
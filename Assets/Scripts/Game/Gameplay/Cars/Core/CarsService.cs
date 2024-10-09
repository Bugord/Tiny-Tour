using System.Collections.Generic;
using Cars;
using Common.Tilemaps;
using Core;
using Core.Logging;
using Game.Common.Level.Data;
using Level.Data;

namespace Gameplay.Cars
{
    public class CarsService : ICarsService
    {
        private readonly ILogger<CarsService> logger;
        private readonly ITilemapPositionConverter tilemapPositionConverter;
        private readonly CarLibrary carLibrary;
        private readonly ICarsFactory carsFactory;

        private readonly List<Car> cars;
        private readonly Dictionary<Car, CarSpawnData> carsSpawnData;

        public IReadOnlyCollection<Car> Cars => cars;

        public CarsService(ILogger<CarsService> logger, ITilemapPositionConverter tilemapPositionConverter, ICarsFactory carsFactory)
        {
            this.logger = logger;
            this.tilemapPositionConverter = tilemapPositionConverter;
            this.carsFactory = carsFactory;

            cars = new List<Car>();
            carsSpawnData = new Dictionary<Car, CarSpawnData>();
        }

        public void SpawnCars(CarSpawnData[] carsSpawnData)
        {
            if (carsSpawnData == null) {
                logger.LogError("Cars spawn data is null");
                return;
            }
            
            foreach (var spawnPointData in carsSpawnData) {
                var carSpawnPosition = tilemapPositionConverter.CellToWorld(spawnPointData.position);
                var car = carsFactory.Create(carSpawnPosition, spawnPointData.direction, spawnPointData.carType, spawnPointData.teamColor);

                cars.Add(car);
                this.carsSpawnData.Add(car, spawnPointData);
            }
        }

        public void ResetCars()
        {
            foreach (var car in cars) {
                var carSpawnData = carsSpawnData[car];
                car.Reset();
                car.SetPosition(tilemapPositionConverter.CellToWorld(carSpawnData.position));
                car.SetDirection(carSpawnData.direction);
            }
        }
    }
}
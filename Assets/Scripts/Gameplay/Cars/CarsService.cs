using System.Collections.Generic;
using Cars;
using Common.Tilemaps;
using Core.Logging;
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

        public CarsService(ILogger<CarsService> logger, ITilemapPositionConverter tilemapPositionConverter, ICarsFactory carsFactory)
        {
            this.logger = logger;
            this.tilemapPositionConverter = tilemapPositionConverter;
            this.carsFactory = carsFactory;

            cars = new List<Car>();
        }

        public void SpawnCars(CarSpawnData[] carsSpawnData)
        {
            if (carsSpawnData == null) {
                logger.LogError("Cars spawn data is null");
                return;
            }
            
            foreach (var spawnPointData in carsSpawnData) {
                var carSpawnPosition = tilemapPositionConverter.CellToWorld(spawnPointData.pos);
                var car = carsFactory.Create(carSpawnPosition, spawnPointData.direction, spawnPointData.carType);

                cars.Add(car);
            }
        }
    }
}
using System;
using Common.Tilemaps;
using Game.Gameplay.Cars.Core;
using Game.Gameplay.Logistic;

namespace Game.Gameplay.Playing
{
    public class PlayRunningService : IPlayRunningService
    {
        public event Action LevelFailed;
        public event Action LevelPassed;

        private readonly ICarsService carsService;
        private readonly ILogisticService logisticService;
        private readonly ITilemapPositionConverter tilemapPositionConverter;

        private int carsCount;
        private int finishedCarsCount;

        public bool IsPlaying { get; private set; }

        public PlayRunningService(ICarsService carsService, ILogisticService logisticService,
            ITilemapPositionConverter tilemapPositionConverter)
        {
            this.carsService = carsService;
            this.logisticService = logisticService;
            this.tilemapPositionConverter = tilemapPositionConverter;
        }

        public void Play()
        {
            carsCount = carsService.Cars.Count;
            finishedCarsCount = 0;

            foreach (var car in carsService.Cars) {
                var carTilePos = tilemapPositionConverter.WorldToCell(car.Position);
                var carPath = logisticService.GetPathForCar(carTilePos, car.TeamColor);

                car.Crashed += OnCarCrashed;
                car.Finished += OnCarFinished;
                car.PlayPath(carPath);
            }

            IsPlaying = true;
        }

        public void ResetPlay()
        {
            foreach (var car in carsService.Cars) {
                car.Crashed -= OnCarCrashed;
                car.Finished -= OnCarFinished;
            }

            carsService.ResetCars();
            IsPlaying = false;
        }

        public void CancelPlay()
        {
            ResetPlay();
        }

        private void OnCarCrashed()
        {
            LevelFailed?.Invoke();
        }

        private void OnCarFinished()
        {
            finishedCarsCount++;

            if (finishedCarsCount == carsCount) {
                LevelPassed?.Invoke();
                // PlayEnded?.Invoke();
            }
        }
    }
}
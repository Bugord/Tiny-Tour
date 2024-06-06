using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cars;
using Level;
using Level.Data;
using Pathfinding;
using Tiles;
using Tiles.Editing;
using Unity.Mathematics;
using UnityEngine;

namespace Core
{
    public class GameSession : MonoBehaviour, ILevelLoader
    {
        [SerializeField]
        private PathfindingController pathfindingController;

        [SerializeField]
        private InGameTilemapEditor inGameTilemapEditor;

        [SerializeField]
        private CarLibrary carLibrary;

        [SerializeField]
        private Car carPrefab;

        private LevelData currentLevelData;

        private List<Car> cars;

        private int finishedCarsCount;

        public void LoadLevel(LevelData levelData)
        {
            currentLevelData = levelData;

            inGameTilemapEditor.LoadLevel(levelData);
            pathfindingController.Update();

            SpawnCars();
        }

        public void SetupEditor(TilemapEditorUI tilemapEditorUI)
        {
            inGameTilemapEditor.Setup(tilemapEditorUI);
        }

        public void Play()
        {
            finishedCarsCount = 0;
            pathfindingController.Update();

            foreach (var targetData in currentLevelData.logisticData.targetsData) {
                var carsToPlay = cars.Where(car => car.Team == targetData.team);
                foreach (var car in carsToPlay) {
                    var cellPath = pathfindingController.FindPath(car.SpawnPosition, targetData.pos);
                    var worldPath = cellPath.Select(p => inGameTilemapEditor.CellToWorldPos(p)).ToArray();
                    car.Reset();
                    car.PlayPath(worldPath);
                }
            }
        }

        public void ResetCars()
        {
            foreach (var car in cars) {
                car.transform.position = inGameTilemapEditor.CellToWorldPos(car.SpawnPosition);
                car.Reset();
            }
        }

        private void SpawnCars()
        {
            cars = new List<Car>();

            foreach (var spawnPointData in currentLevelData.logisticData.spawnPointsData) {
                var carSpawnPosition = inGameTilemapEditor.CellToWorldPos(spawnPointData.pos);
                var car = Instantiate(carPrefab, carSpawnPosition, quaternion.identity);

                car.SetData(spawnPointData.pos,
                    carLibrary.GetCarData(spawnPointData.carType),
                    spawnPointData.team,
                    spawnPointData.direction);

                car.transform.parent = transform;

                car.Finished += OnCarFinished;
                car.Crashed += OnCrashed;
                cars.Add(car);
            }
        }

        private void OnCrashed()
        {
            finishedCarsCount++;

            if (finishedCarsCount == cars.Count) {
                StartCoroutine(ResetWithDelay());
            }
        }

        private IEnumerator ResetWithDelay()
        {
            yield return new WaitForSeconds(1);
            ResetCars();
        }

        private void OnCarFinished()
        {
            finishedCarsCount++;

            if (finishedCarsCount == cars.Count) {
                StartCoroutine(ResetWithDelay());
            }
        }
    }
}
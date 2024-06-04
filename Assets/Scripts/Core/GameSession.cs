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
        private Car carPrefab;

        private LevelData currentLevelData;

        private List<Car> cars;

        private PathData carPathData;

        public void LoadLevel(LevelData levelData)
        {
            currentLevelData = levelData;

            inGameTilemapEditor.gameObject.SetActive(true);
            inGameTilemapEditor.Setup();
            inGameTilemapEditor.LoadLevel(levelData);
            pathfindingController.Update();

            SpawnCars();
        }

        [ContextMenu("Play")]
        public void Play()
        {
            pathfindingController.Update();
            
            var path = pathfindingController.FindPath(
                (Vector2Int)currentLevelData.pathsData.First(x => x.type == LogisticTileType.SpawnPoint).position,
                (Vector2Int)currentLevelData.pathsData.First(x => x.type == LogisticTileType.Target).position);
            
            cars[0].PlayPath(path.Select(p => inGameTilemapEditor.CellToWorldPos((Vector3Int)p)).ToArray());
        }

        private void SpawnCars()
        {
            cars = new List<Car>();

            foreach (var pathData in currentLevelData.pathsData) {
                if (pathData.type == LogisticTileType.SpawnPoint) {
                    var carSpawnPosition = inGameTilemapEditor.CellToWorldPos(pathData.position);
                    var car = Instantiate(carPrefab, carSpawnPosition, quaternion.identity);
                    cars.Add(car);
                }
            }
        }
    }
}
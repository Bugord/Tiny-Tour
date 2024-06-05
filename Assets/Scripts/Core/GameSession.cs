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

            inGameTilemapEditor.LoadLevel(levelData);
            pathfindingController.Update();

            SpawnCars();
        }

        public void SetupEditor(TilemapEditorUI tilemapEditorUI)
        {
            inGameTilemapEditor.Setup(tilemapEditorUI);
        }

        [ContextMenu("Play")]
        public void Play()
        {
            pathfindingController.Update();

            for (var i = 0; i < currentLevelData.pathsData.Length; i++) {
                var pathData = currentLevelData.pathsData[i];
                var path = pathfindingController.FindPath(pathData.spawnPosition, pathData.targetPosition);
                cars[i].PlayPath(path.Select(p => inGameTilemapEditor.CellToWorldPos((Vector3Int)p)).ToArray());
            }
        }

        private void SpawnCars()
        {
            cars = new List<Car>();

            foreach (var pathData in currentLevelData.pathsData) {
                var carSpawnPosition = inGameTilemapEditor.CellToWorldPos(pathData.spawnPosition);
                var car = Instantiate(carPrefab, carSpawnPosition, quaternion.identity);
                car.transform.parent = transform;
                cars.Add(car);
            }
        }
    }
}
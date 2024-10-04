using System.Collections.Generic;
using System.Linq;
using Cars;
using Common.Tilemaps;
using Core;
using Core.Logging;
using Level;
using Level.Data;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility;

namespace LevelEditing.Editing.Editors
{
    public class SpawnPointEditor : ISpawnPointEditor
    {
        private readonly ILogger<SpawnPointEditor> logger;
        private readonly Tilemap logisticTilemap;
        private readonly ITileLibrary tileLibrary;
        private readonly Dictionary<Vector2Int, CarSpawnData> initialCarSpawnPoints;
        private readonly Dictionary<Vector2Int, CarSpawnData> carSpawnPoints;

        private const int SpawnPointLayer = 1;

        public SpawnPointEditor(ILogger<SpawnPointEditor> logger, ITilemapsProvider tilemapsProvider,
            ITileLibrary tileLibrary)
        {
            this.logger = logger;
            this.tileLibrary = tileLibrary;
            logisticTilemap = tilemapsProvider.LogisticTilemap;

            initialCarSpawnPoints = new Dictionary<Vector2Int, CarSpawnData>();
            carSpawnPoints = new Dictionary<Vector2Int, CarSpawnData>();
        }

        public void Load(CarSpawnData[] carsSpawnData)
        {
            Clear();

            if (carsSpawnData == null) {
                return;
            }
                
            foreach (var carSpawnData in carsSpawnData) {
                if (initialCarSpawnPoints.ContainsKey(carSpawnData.position)) {
                    logger.LogWarning(
                        $"Trying to load more that 1 spawnpoint at same position {carSpawnData.position}");
                    continue;
                }

                initialCarSpawnPoints.Add(carSpawnData.position, carSpawnData);
                SetCarSpawnPoint(carSpawnData.position, carSpawnData.carType, carSpawnData.teamColor,
                    carSpawnData.direction);
            }
        }

        public void SetCarSpawnPoint(Vector2Int position, CarType carType, TeamColor teamColor, Direction direction)
        {
            if (carSpawnPoints.TryGetValue(position, out var existingCarSpawnData)) {
                carSpawnPoints.Remove(existingCarSpawnData.position);
            }

            var carSpawnData = new CarSpawnData(position, carType, teamColor, direction);
            carSpawnPoints.Add(carSpawnData.position, carSpawnData);
            SetTile(carSpawnData);
        }

        public bool HasSpawnPointWithColor(Vector2Int position, TeamColor color)
        {
            if (carSpawnPoints.TryGetValue(position, out var carSpawnData)) {
                return carSpawnData.teamColor == color;
            }

            return false;
        }

        public void RotateSpawnPoint(Vector2Int position)
        {
            if (carSpawnPoints.TryGetValue(position, out var carSpawnData)) {
                carSpawnData.direction = carSpawnData.direction.GetNextValue();
                carSpawnPoints[position] = carSpawnData;
                SetTile(carSpawnData);
            }
            else {
                logger.LogWarning("Trying to rotate spawn point that is not exist");
            }
        }

        public void EraseTile(Vector2Int position)
        {
            carSpawnPoints.Remove(position);
            var tilePosition = GetTilePosition(position);
            logisticTilemap.SetTile(tilePosition, null);
        }

        public void Reset()
        {
            ClearTilemap();
            carSpawnPoints.Clear();

            foreach (var initialCarSpawnPoint in initialCarSpawnPoints.Values) {
                SetCarSpawnPoint(initialCarSpawnPoint.position,
                    initialCarSpawnPoint.carType,
                    initialCarSpawnPoint.teamColor,
                    initialCarSpawnPoint.direction);
            }
        }

        public void Clear()
        {
            ClearTilemap();
            carSpawnPoints.Clear();
            initialCarSpawnPoints.Clear();
        }

        public CarSpawnData[] GetCarsSpawnData()
        {
            return carSpawnPoints.Values.ToArray();
        }

        private void SetTile(CarSpawnData carSpawnData)
        {
            var tile = tileLibrary.GetSpawnPointTile(carSpawnData.carType, carSpawnData.teamColor,
                carSpawnData.direction);
            var tilePosition = GetTilePosition(carSpawnData.position);
            logisticTilemap.SetTile(tilePosition, tile);
        }

        public bool HasTile(Vector2Int position)
        {
            return carSpawnPoints.ContainsKey(position);
        }
        
        private void ClearTilemap()
        {
            foreach (var carSpawnPoint in carSpawnPoints) {
                var tilePos = GetTilePosition(carSpawnPoint.Key);
                logisticTilemap.SetTile(tilePos, null);
            }
        }

        private static Vector3Int GetTilePosition(Vector2Int position)
        {
            return new Vector3Int(position.x, position.y, SpawnPointLayer);
        }
    }
}
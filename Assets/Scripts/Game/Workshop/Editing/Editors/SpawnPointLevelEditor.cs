using System.Collections.Generic;
using System.Linq;
using Cars;
using Common.Tilemaps;
using Core;
using Game.Common.Level.Data;
using Game.Workshop.Editing.Editors;
using Level;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility;

namespace Game.Workshop.LevelEditor.Editors
{
    public class SpawnPointLevelEditor : ISpawnPointLevelEditor
    {
        private readonly Tilemap logisticTilemap;
        private readonly ITileLibrary tileLibrary;

        private readonly Dictionary<Vector2Int, CarSpawnData> carsSpawnData;
        private CarSpawnData[] cachedCarsSpawnData;

        private const int SpawnPointLayer = 1;

        public SpawnPointLevelEditor(ITilemapsProvider tilemapsProvider, ITileLibrary tileLibrary)
        {
            this.tileLibrary = tileLibrary;
            logisticTilemap = tilemapsProvider.LogisticTilemap;

            carsSpawnData = new Dictionary<Vector2Int, CarSpawnData>();
        }

        public void SetTile(CarSpawnData carSpawnData)
        {
            var newCarSpawnData = new CarSpawnData {
                position = carSpawnData.position,
                direction = carSpawnData.direction,
                carType = carSpawnData.carType,
                teamColor = carSpawnData.teamColor
            };

            carsSpawnData[carSpawnData.position] = carSpawnData;
            SetTilemapTile(newCarSpawnData);
        }

        public bool HasTile(Vector2Int position)
        {
            return carsSpawnData.ContainsKey(position);
        }

        public void SetCarSpawnPoint(Vector2Int position, CarType carType, TeamColor teamColor, Direction direction)
        {
            if (carsSpawnData.TryGetValue(position, out var existingCarSpawnData)) {
                carsSpawnData.Remove(existingCarSpawnData.position);
            }

            var newCarSpawnData = new CarSpawnData {
                position = position,
                carType = carType,
                teamColor = teamColor,
                direction = direction
            };
            
            SetTile(newCarSpawnData);
        }

        public void Load(CarSpawnData[] carsSpawnData)
        {
            cachedCarsSpawnData = carsSpawnData;

            foreach (var carSpawnData in carsSpawnData) {
                SetTile(carSpawnData);
            }
        }

        public bool HasSpawnPointWithColor(Vector2Int position, TeamColor color)
        {
            if (carsSpawnData.TryGetValue(position, out var carSpawnData)) {
                return carSpawnData.teamColor == color;
            }

            return false;
        }

        public void RotateSpawnPoint(Vector2Int position)
        {
            if (carsSpawnData.TryGetValue(position, out var carSpawnData)) {
                carSpawnData.direction = carSpawnData.direction.GetNextValue();
                carsSpawnData[position] = carSpawnData;
                SetTile(carSpawnData);
            }
        }

        public void EraseTile(Vector2Int position)
        {
            carsSpawnData.Remove(position);
            var tilePosition = GetTilePosition(position);
            logisticTilemap.SetTile(tilePosition, null);
        }

        public void Reset()
        {
            Clear();

            foreach (var cachedCarSpawnData in cachedCarsSpawnData) {
                SetTile(cachedCarSpawnData);
            }
        }

        public CarSpawnData[] GetTilesData()
        {
            return carsSpawnData.Values.ToArray();
        }

        public void Clear()
        {
            ClearTilemap();
            carsSpawnData.Clear();
        }

        public CarSpawnData[] GetCarsSpawnData()
        {
            return carsSpawnData.Values.ToArray();
        }

        private void ClearTilemap()
        {
            foreach (var carSpawnPoint in carsSpawnData) {
                var tilePos = GetTilePosition(carSpawnPoint.Key);
                logisticTilemap.SetTile(tilePos, null);
            }
        }

        private static Vector3Int GetTilePosition(Vector2Int position)
        {
            return new Vector3Int(position.x, position.y, SpawnPointLayer);
        }

        private void EraseTilemapTile(Vector2Int position)
        {
            logisticTilemap.SetTile(GetTilePosition(position), null);
        }

        private void SetTilemapTile(CarSpawnData carSpawnData)
        {
            var tile = tileLibrary.GetSpawnPointTile(CarType.Regular, carSpawnData.teamColor, carSpawnData.direction);
            logisticTilemap.SetTile(GetTilePosition(carSpawnData.position), tile);
        }
    }
}
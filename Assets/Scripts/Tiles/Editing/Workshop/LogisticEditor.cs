using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Level;
using Level.Data;
using Tiles.Editing.Options;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility;

namespace Tiles.Editing.Workshop
{
    public class LogisticEditor : ITileEditor
    {
        private readonly Tilemap uiTilemap;
        private readonly ITileLibrary tileLibrary;

        private LogisticTileType selectedLogisticTileType;
        private Team selectedTeam;

        private readonly List<LogisticTileData> logisticTiles;

        public LogisticEditor(Tilemap uiTilemap, ITileLibrary tileLibrary)
        {
            this.uiTilemap = uiTilemap;
            this.tileLibrary = tileLibrary;
            logisticTiles = new List<LogisticTileData>();
        }

        public void OnTileDown(Vector3Int pos)
        {
            SetLogisticTile(pos);
        }

        public void OnTileUp()
        {
        }

        public void OnTileMove(Vector3Int pos)
        {
        }

        public void OnTileEraseDown(Vector3Int pos)
        {
            EraseLogisticTile(pos);
        }

        public void OnTileEraseMove(Vector3Int pos)
        {
            EraseLogisticTile(pos);
        }

        public List<BaseEditorOption> GetOptions()
        {
            var teams = EnumExtensions.GetAllEnums<Team>();
            var options = new List<BaseEditorOption>();
            foreach (var team in teams) {
                options.Add(new LogisticEditorOption {
                    LogisticTileType = LogisticTileType.Target,
                    Team = team,
                    TileEditor = this,
                    Icon = tileLibrary.GetLogisticTile(LogisticTileType.Target, team).Sprite
                });
                options.Add(new LogisticEditorOption {
                    LogisticTileType = LogisticTileType.SpawnPoint,
                    Team = team,
                    TileEditor = this,
                    Icon = tileLibrary.GetLogisticTile(LogisticTileType.SpawnPoint, team).Sprite
                });
            }

            return options;
        }

        public void SetOption(BaseEditorOption option)
        {
            var logisticEditorOption = (LogisticEditorOption)option;
            selectedLogisticTileType = logisticEditorOption.LogisticTileType;
            selectedTeam = logisticEditorOption.Team;
        }

        public void Load(PathData[] pathsData)
        {
            uiTilemap.ClearAllTiles();
            logisticTiles.Clear();

            foreach (var pathData in pathsData) {
                logisticTiles.Add(new LogisticTileData {
                    Team = pathData.team,
                    Position = pathData.targetPosition,
                    Type = LogisticTileType.Target
                });

                logisticTiles.Add(new LogisticTileData {
                    Team = pathData.team,
                    Position = pathData.spawnPosition,
                    Type = LogisticTileType.SpawnPoint
                });

                uiTilemap.SetTile(pathData.spawnPosition,
                    tileLibrary.GetLogisticTile(LogisticTileType.SpawnPoint, pathData.team));
                uiTilemap.SetTile(pathData.targetPosition,
                    tileLibrary.GetLogisticTile(LogisticTileType.Target, pathData.team));
            }

            if (!Validate()) {
                return;
            }
        }

        public PathData[] Save()
        {
            var pathsData = new List<PathData>();
            foreach (var spawnPoint in logisticTiles.Where(tile => tile.Type == LogisticTileType.SpawnPoint)) {
                var target = logisticTiles.FirstOrDefault(tile =>
                    tile.Type == LogisticTileType.Target && tile.Team == spawnPoint.Team);

                if (target != null) {
                    pathsData.Add(new PathData {
                        team = spawnPoint.Team,
                        spawnPosition = spawnPoint.Position,
                        targetPosition = target.Position
                    });
                }
            }

            return pathsData.ToArray();
        }

        private bool Validate()
        {
            return true;
        }

        private void SetLogisticTile(Vector3Int pos)
        {
            uiTilemap.SetTile(pos, tileLibrary.GetLogisticTile(selectedLogisticTileType, selectedTeam));
            SetPathTile(pos);
        }

        private void SetPathTile(Vector3Int pos)
        {
            var path = logisticTiles.FirstOrDefault(path =>
                path.Team == selectedTeam && path.Type == selectedLogisticTileType);

            if (path != null) {
                uiTilemap.SetTile(path.Position, null);
                path.Position = pos;
                return;
            }

            path = new LogisticTileData {
                Team = selectedTeam,
                Type = selectedLogisticTileType,
                Position = pos
            };
            logisticTiles.Add(path);
        }

        private void EraseLogisticTile(Vector3Int pos)
        {
            uiTilemap.SetTile(pos, null);
            ErasePath(pos);
        }

        private void ErasePath(Vector3Int pos)
        {
            var path = logisticTiles.FirstOrDefault(path => path.Position == pos);

            if (path == null) {
                return;
            }

            logisticTiles.Remove(path);
        }
    }
}
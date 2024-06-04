using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Level;
using Level.Data;
using Tiles.Editing.Options;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiles.Editing.Workshop
{
    public class LogisticEditor : ITileEditor
    {
        private readonly Tilemap uiTilemap;
        private readonly ITileLibrary tileLibrary;

        private LogisticTileType selectedLogisticTileType;
        private Team selectedTeam;

        private List<PathData> pathsData;

        public LogisticEditor(Tilemap uiTilemap, ITileLibrary tileLibrary)
        {
            this.uiTilemap = uiTilemap;
            this.tileLibrary = tileLibrary;
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
            return new List<BaseEditorOption> {
                new LogisticEditorOption {
                    LogisticTileType = LogisticTileType.Target,
                    TileEditor = this,
                    Icon = tileLibrary.GetLogisticTile(LogisticTileType.Target, Team.Grey).sprite
                },
                new LogisticEditorOption {
                    LogisticTileType = LogisticTileType.SpawnPoint,
                    TileEditor = this,
                    Icon = tileLibrary.GetLogisticTile(LogisticTileType.SpawnPoint, Team.Grey).sprite
                }
            };
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

            this.pathsData = pathsData.ToList();
            if (!Validate()) {
                return;
            }

            foreach (var pathData in pathsData) {
                uiTilemap.SetTile(pathData.position, tileLibrary.GetLogisticTile(pathData.type, pathData.team));
            }
        }

        public PathData[] Save()
        {
            return Validate() ? pathsData.ToArray() : Array.Empty<PathData>();
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
            var path = pathsData.FirstOrDefault(path =>
                path.team == selectedTeam && path.type == selectedLogisticTileType);

            if (path != null) {
                uiTilemap.SetTile(path.position, null);
                path.position = pos;
                return;
            }

            path = new PathData {
                team = selectedTeam,
                type = selectedLogisticTileType,
                position = pos
            };
            pathsData.Add(path);
        }

        private void EraseLogisticTile(Vector3Int pos)
        {
            uiTilemap.SetTile(pos, null);
            ErasePath(pos);
        }

        private void ErasePath(Vector3Int pos)
        {
            var path = pathsData.FirstOrDefault(path => path.position == pos);

            if (path == null) {
                return;
            }

            pathsData.Remove(path);
        }
    }
}
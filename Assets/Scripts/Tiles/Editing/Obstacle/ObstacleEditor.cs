using System.Collections.Generic;
using System.Linq;
using Level;
using Level.Data;
using Tiles.Editing.Options;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiles.Editing
{
    public class ObstacleEditor : ITileEditor
    {
        private readonly Tilemap obstacleTilemap;
        private readonly ITileLibrary tileLibrary;
        private readonly List<ObstacleData> obstaclesData;

        private ObstacleOption selectedOption;

        public ObstacleEditor(Tilemap obstacleTilemap, ITileLibrary tileLibrary)
        {
            this.obstacleTilemap = obstacleTilemap;
            this.tileLibrary = tileLibrary;
            
            obstaclesData = new List<ObstacleData>();
        }

        public void OnTileDown(Vector3Int pos)
        {
            SetObstacleTile(selectedOption.Id, pos);
        }

        public void OnTileUp()
        {
        }

        public void OnTileMove(Vector3Int pos)
        {
            SetObstacleTile(selectedOption.Id, pos);
        }

        public void OnTileEraseDown(Vector3Int pos)
        {
            EraseObstacleTile(pos);
        }

        public void OnTileEraseMove(Vector3Int pos)
        {
            EraseObstacleTile(pos);
        }

        public List<BaseEditorOption> GetOptions()
        {
            var options = new List<BaseEditorOption>();

            var obstacleTiles = tileLibrary.GetObstacleTiles();
            for (var index = 0; index < obstacleTiles.Length; index++) {
                var obstacleTile = obstacleTiles[index];
                options.Add(new ObstacleOption {
                    TileEditor = this,
                    Icon = obstacleTile.sprite,
                    Id = index
                });
            }

            return options;
        }

        public void SetOption(BaseEditorOption option)
        {
            selectedOption = (ObstacleOption)option;
        }

        public ObstacleData[] Save()
        {
            return obstaclesData.ToArray();
        }

        public void Load(ObstacleData[] obstaclesData)
        {
            if (obstaclesData == null) {
                return;
            }

            foreach (var obstacleData in obstaclesData) {
                SetObstacleTile(obstacleData.id, obstacleData.pos);
            }
        }

        private void SetObstacleTile(int id, Vector3Int pos)
        {
            var obstacle = obstaclesData.FirstOrDefault(obstacle => obstacle.pos == pos);

            if (obstacle == null) {
                obstacle = new ObstacleData {
                    pos = pos,
                    id = id
                };

                obstaclesData.Add(obstacle);
            }
            else {
                obstacle.id = id;
            }

            var tile = tileLibrary.GetObstacleTile(id);
            obstacleTilemap.SetTile(pos, tile);
        }

        private void EraseObstacleTile(Vector3Int pos)
        {
            var obstacle = obstaclesData.FirstOrDefault(obstacle => obstacle.pos == pos);
            if (obstacle == null) {
                return;
            }

            obstacleTilemap.SetTile(pos, null);
            obstaclesData.Remove(obstacle);
        }
    }
}
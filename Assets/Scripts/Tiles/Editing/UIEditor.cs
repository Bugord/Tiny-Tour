using System.Collections.Generic;
using Level;
using Tiles.Options;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiles
{
    public class UIEditor : ITileEditor
    {
        private readonly Tilemap uiTilemap;
        private readonly ITileLibrary tileLibrary;
        
        private UITileType uiTileType;
        
        public UIEditor(Tilemap uiTilemap, ITileLibrary tileLibrary)
        {
            this.uiTilemap = uiTilemap;
            this.tileLibrary = tileLibrary;
        }
        
        public void OnTileDown(Vector3Int pos)
        {
            SetTerrainTile(pos);
        }

        public void OnTileUp()
        {
        }

        public void OnTileMove(Vector3Int pos)
        {
            SetTerrainTile(pos);
        }

        public void OnTileEraseDown(Vector3Int pos)
        {
            EraseTile(pos);
        }

        public void OnTileEraseMove(Vector3Int pos)
        {
            EraseTile(pos);
        }

        public List<BaseEditorOption> GetOptions()
        {
            return new List<BaseEditorOption> {
                new UIEditorOption {
                    UITileType = UITileType.Target,
                    TileEditor = this,
                    Icon = tileLibrary.GetUIType(UITileType.Target).sprite
                },
                new UIEditorOption {
                    UITileType = UITileType.SpawnPoint,
                    TileEditor = this,
                    Icon = tileLibrary.GetUIType(UITileType.SpawnPoint).sprite
                }
            };
        }

        public void SetOption(BaseEditorOption option)
        {
            uiTileType = ((UIEditorOption)option).UITileType;
        }

        public void Load(UITileData[] tilesData)
        {
            uiTilemap.ClearAllTiles();

            foreach (var tileData in tilesData) {
                uiTilemap.SetTile(tileData.position, tileLibrary.GetUIType(tileData.tileType));
            }
        }

        public UITileData[] Save()
        {
            return GetUITilesData();
        }

        private void SetTerrainTile(Vector3Int pos)
        {
            uiTilemap.SetTile(pos, tileLibrary.GetUIType(uiTileType));
        }

        private void EraseTile(Vector3Int pos)
        {
            uiTilemap.SetTile(pos, null);
        }
        
        private UITileData[] GetUITilesData()
        {
            var uiTilesData = new List<UITileData>();
            foreach (var pos in uiTilemap.cellBounds.allPositionsWithin) {
                var uiTile = uiTilemap.GetTile<UITile>(pos);
                if (!uiTile) {
                    continue;
                }

                var terrainTileData = new UITileData() {
                    position = pos,
                    tileType = uiTile.type,
                    team = uiTile.team
                };

                uiTilesData.Add(terrainTileData);
            }

            return uiTilesData.ToArray();
        }
    }
}
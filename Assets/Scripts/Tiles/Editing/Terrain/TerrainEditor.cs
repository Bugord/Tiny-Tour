using System.Collections.Generic;
using Level;
using Tiles.Editing.Options;
using Tiles.Ground;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiles.Editing
{
    public class TerrainEditor : ITileEditor
    {
        private readonly Tilemap terrainTilemap;
        private readonly ITileLibrary tileLibrary;
        
        private TerrainType terrainType;
        
        public TerrainEditor(Tilemap terrainTilemap, ITileLibrary tileLibrary)
        {
            this.terrainTilemap = terrainTilemap;
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
                new TerrainEditorOption {
                    TerrainType = TerrainType.Ground,
                    TileEditor = this,
                    Icon = tileLibrary.GetTerrainTileByType(TerrainType.Ground).m_DefaultSprite
                },
                new TerrainEditorOption {
                    TerrainType = TerrainType.BridgeBase,
                    TileEditor = this,
                    Icon = tileLibrary.GetTerrainTileByType(TerrainType.BridgeBase).m_DefaultSprite
                }
            };
        }

        public void SetOption(BaseEditorOption option)
        {
            terrainType = ((TerrainEditorOption)option).TerrainType;
        }

        public TerrainTileData[] Save()
        {
            return GetTerrainTilesData();
        }

        public void Load(TerrainTileData[] terrainTilesData)
        {
            terrainTilemap.ClearAllTiles();
            foreach (var terrainTileData in terrainTilesData) {
                terrainTilemap.SetTile(terrainTileData.position, tileLibrary.GetTerrainTileByType(terrainTileData.terrainType));
            }
        }

        private void SetTerrainTile(Vector3Int pos)
        {
            terrainTilemap.SetTile(pos, tileLibrary.GetTerrainTileByType(terrainType));
        }

        private void EraseTile(Vector3Int pos)
        {
            terrainTilemap.SetTile(pos, tileLibrary.GetTerrainTileByType(TerrainType.Water));
        }
        
        private TerrainTileData[] GetTerrainTilesData()
        {
            var terrainTilesData = new List<TerrainTileData>();
            foreach (var pos in terrainTilemap.cellBounds.allPositionsWithin) {
                var terrainTile = terrainTilemap.GetTile<TerrainTile>(pos);
                if (!terrainTile) {
                    continue;
                }

                var terrainTileData = new TerrainTileData {
                    position = pos,
                    terrainType = terrainTile.terrainType
                };

                terrainTilesData.Add(terrainTileData);
            }

            return terrainTilesData.ToArray();
        }
    }
}
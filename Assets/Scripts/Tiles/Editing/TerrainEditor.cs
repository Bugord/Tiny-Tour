using Level;
using Tiles.Ground;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiles
{
    public class TerrainEditor : ITileEditor
    {
        private readonly Tilemap terrainTilemap;
        private readonly ITileLibrary tileLibrary;
        private readonly TerrainType terrainType;
        
        public TerrainEditor(Tilemap terrainTilemap, ITileLibrary tileLibrary, TerrainType terrainType)
        {
            this.terrainTilemap = terrainTilemap;
            this.tileLibrary = tileLibrary;
            this.terrainType = terrainType;
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

        private void SetTerrainTile(Vector3Int pos)
        {
            terrainTilemap.SetTile(pos, tileLibrary.GetTerrainTileByType(terrainType));
        }

        private void EraseTile(Vector3Int pos)
        {
            terrainTilemap.SetTile(pos, tileLibrary.GetTerrainTileByType(TerrainType.Water));
        }
    }
}
using System;
using Core;
using Level;
using Tiles.Ground;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiles
{
    [CreateAssetMenu]
    public class TileLibraryData : ScriptableObject, ITileLibrary
    {
        [SerializeField]
        private TerrainTile groundTile;
        
        [SerializeField]
        private TerrainTile waterTile;
        
        [SerializeField]
        private TerrainTile bridgeBaseTile;
        
        [SerializeField]
        private RoadTile roadTile;
        
        public RoadTile GetRoadTile()
        {
            return roadTile;
        }

        public TerrainTile GetTerrainTileByType(TerrainType terrainType) => terrainType switch {
            TerrainType.Ground => groundTile,
            TerrainType.Water => waterTile,
            TerrainType.BridgeBase => bridgeBaseTile,
            _ => null
        };
    }
}
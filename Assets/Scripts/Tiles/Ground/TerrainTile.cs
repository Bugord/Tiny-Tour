using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiles.Ground
{
    [CreateAssetMenu]
    public class TerrainTile : RuleTile<TerrainTile.Neighbor>
    {
        public TerrainType terrainType;

        public enum TerrainType
        {
            Ground,
            Water,
            BridgeBase
        }

        public class Neighbor : RuleTile.TilingRule.Neighbor
        {
            public const int Ground = 4;
            public const int Water = 5;
            public const int BridgeBase = 6;
            public const int WaterOrBridgeBase = 7;
            public const int GroundOrBridgeBase = 8;
        }

        public override bool RuleMatch(int neighbor, TileBase other)
        {
            var otherTerrainTile = other as TerrainTile;

            return neighbor switch {
                Neighbor.Ground => otherTerrainTile && otherTerrainTile.terrainType is TerrainType.Ground,
                Neighbor.Water => otherTerrainTile && otherTerrainTile.terrainType is TerrainType.Water,
                Neighbor.BridgeBase => otherTerrainTile && otherTerrainTile.terrainType is TerrainType.BridgeBase,
                Neighbor.WaterOrBridgeBase => otherTerrainTile && otherTerrainTile.terrainType is TerrainType.Water or TerrainType.BridgeBase,
                Neighbor.GroundOrBridgeBase => otherTerrainTile && otherTerrainTile.terrainType is TerrainType.Ground or TerrainType.BridgeBase,
                _ => base.RuleMatch(neighbor, other)
            };
        }
    }
}
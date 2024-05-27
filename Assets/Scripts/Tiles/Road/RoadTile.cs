using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiles
{
    [Serializable]
    [CreateAssetMenu]
    public class RoadTile : TileBase
    {
        public Sprite defaultSprite;
        public Tile.ColliderType defaultColliderType = Tile.ColliderType.Sprite;
        public ConnectionDirection connectionDirection;

        public List<RoadRules> roadRules;

        public override void GetTileData(Vector3Int position, ITilemap tileMap, ref TileData tileData)
        {
            tileData.sprite = defaultSprite;
            tileData.colliderType = defaultColliderType;
            tileData.flags = TileFlags.LockTransform;
            tileData.transform = Matrix4x4.identity;
            
            var roadRule = roadRules.FirstOrDefault(x => x.connectionDirection == connectionDirection);
            if (roadRule != null) {
                tileData.sprite = roadRule.sprite;
            }
        }

        [Serializable]
        public class RoadRules
        {
            public ConnectionDirection connectionDirection;
            public Sprite sprite;
        }

        private void OnValidate()
        {
            foreach (var roadRule in roadRules) {
                if (roadRule.connectionDirection != ConnectionDirection.None &&
                    (roadRule.connectionDirection | ConnectionDirection.All) == roadRule.connectionDirection) {
                    roadRule.connectionDirection = ConnectionDirection.All;
                }
            }
        }
    }
}
using Core;
using Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Level
{
    [CreateAssetMenu]
    public class UITile : TileBase
    {
        public Sprite sprite;
        public UITileType type;
        public Team team;
        
        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            tileData.sprite = sprite;
        }
    }
}
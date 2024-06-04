using Core;
using Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Level
{
    [CreateAssetMenu]
    public class LogisticTile : TileBase
    {
        public Sprite sprite;
        public LogisticTileType type;
        public Team team;
        
        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            tileData.sprite = sprite;
        }
    }
}
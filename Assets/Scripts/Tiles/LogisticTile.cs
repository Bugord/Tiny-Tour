using AYellowpaper.SerializedCollections;
using Core;
using Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Level
{
    [CreateAssetMenu]
    public class LogisticTile : TileBase
    {
        public SerializedDictionary<Team, Sprite> sprites;
        public LogisticTileType type;
        public Team team;
        
        public Sprite Sprite => sprites[team];
        
        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            tileData.sprite = Sprite;
        }
    }
}
using AYellowpaper.SerializedCollections;
using Core;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiles.Logistic
{
    [CreateAssetMenu]
    public class TargetTile : LogisticTile
    {
        public Team team;
        public SerializedDictionary<Team, Sprite> sprites;

        public Sprite Sprite => sprites[team];

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            tileData.sprite = sprites[team];
        }
    }
}
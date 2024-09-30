using AYellowpaper.SerializedCollections;
using Core;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace Tiles.Logistic
{
    [CreateAssetMenu]
    public class TargetTile : LogisticTile
    {
        [FormerlySerializedAs("carColor")]
        [FormerlySerializedAs("team")]
        public TeamColor teamColor;
        public SerializedDictionary<TeamColor, Sprite> sprites;

        public Sprite Sprite => sprites[teamColor];

        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            tileData.sprite = sprites[teamColor];
        }
    }
}
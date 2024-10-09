using UnityEngine.Tilemaps;

namespace Common.Tilemaps
{
    public interface ITilemapsProvider
    {
        Tilemap TerrainTilemap { get; }
        Tilemap RoadTilemap { get; }
        Tilemap LogisticTilemap { get; }
        Tilemap ObstacleTilemap { get; }
    }
}
using System;
using Core;
using Tiles;
using UnityEngine.Serialization;

namespace Level
{
    [Serializable]
    public class LogisticTileData : BaseTileData
    {
        public LogisticTileType tileType;
        [FormerlySerializedAs("carColor")]
        [FormerlySerializedAs("team")]
        public TeamColor teamColor;
    }
}
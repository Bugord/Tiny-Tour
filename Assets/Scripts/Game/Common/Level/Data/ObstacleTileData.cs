using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Level.Data
{
    [Serializable]
    public class ObstacleTileData
    {
        [FormerlySerializedAs("pos")]
        public Vector3Int position;
        public int id;
    }
}
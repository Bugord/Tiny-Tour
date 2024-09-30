using System;
using Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Level.Data
{
    [Serializable]
    public class IntermediatePointData
    {
        public Vector3Int pos;
        [FormerlySerializedAs("carColor")]
        [FormerlySerializedAs("team")]
        public TeamColor teamColor;
    }
}
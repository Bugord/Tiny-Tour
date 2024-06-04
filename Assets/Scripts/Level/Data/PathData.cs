using System;
using Core;
using UnityEngine;

namespace Level.Data
{
    [Serializable]
    public class PathData
    {
        public Vector3Int targetPosition;
        public Vector3Int spawnPosition;
        public Team team;
    }
}
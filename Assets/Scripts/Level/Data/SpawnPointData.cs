using System;
using Core;
using UnityEngine;

namespace Level
{
    [Serializable]
    public class SpawnPointData
    {
        public Vector3Int position;
        public Team team;
    }
}
using System;
using Core;
using Tiles;
using UnityEngine;

namespace Level.Data
{
    [Serializable]
    public class PathData
    {
        public Vector3Int position;
        public Team team;
        public LogisticTileType type;
    }
}
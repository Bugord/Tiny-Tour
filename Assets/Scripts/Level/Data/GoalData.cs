using System;
using Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace Level.Data
{
    [Serializable]
    public class GoalData
    {
        public Vector3Int pos;
        public TeamColor teamColor;
    }
}
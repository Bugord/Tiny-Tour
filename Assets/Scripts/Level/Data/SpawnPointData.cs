﻿using System;
using Cars;
using Core;
using UnityEngine;

namespace Level.Data
{
    [Serializable]
    public class SpawnPointData
    {
        public Vector3Int pos;
        public CarType carType;
        public Team team;
        public Direction direction;
    }
}
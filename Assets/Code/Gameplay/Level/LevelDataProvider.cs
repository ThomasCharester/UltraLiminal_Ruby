using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Level
{
    public class LevelDataProvider : ILevelDataProvider
    {
        public Vector3 PlayerStart { get; private set; }

        public void SetPlayerStart(Vector3 playerStart)
        {
            PlayerStart = playerStart;
        }

        public List<Transform> ItemSpawnPoints { get; set; }
    }
}
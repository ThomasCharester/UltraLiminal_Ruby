using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Level
{
    public interface ILevelDataProvider
    {
        Vector3 PlayerStart { get; }
        void SetPlayerStart(Vector3 playerStart);
        List<Transform> ItemSpawnPoints { get; set; }
    }
}
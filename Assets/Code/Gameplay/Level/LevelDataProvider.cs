using System.Collections.Generic;
using Code.Gameplay.Features.Items;
using UnityEngine;

namespace Code.Gameplay.Level
{
    public class LevelDataProvider : ILevelDataProvider
    {
        public Vector3 PlayerStart { get; private set; }
        public InventoryID LevelInventoryID { get; private set; }
        public void SetPlayerStart(Vector3 playerStart) => PlayerStart = playerStart;
        public void SetLevelInventoryID(InventoryID inventoryID) => LevelInventoryID = inventoryID;
        public List<Transform> ItemSpawnPoints { get; set; } // to do почистить
    }
}
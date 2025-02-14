using System.Collections.Generic;
using Code.Gameplay.Features.Items;
using UnityEngine;

namespace Code.Gameplay.Level
{
    public interface ILevelDataProvider
    {
        Vector3 PlayerStart { get; }
        InventoryID LevelInventoryID { get;}
        void SetPlayerStart(Vector3 playerStart);
        void SetLevelInventoryID(InventoryID inventoryID);
        List<Transform> ItemSpawnPoints { get; set; }
    }
}
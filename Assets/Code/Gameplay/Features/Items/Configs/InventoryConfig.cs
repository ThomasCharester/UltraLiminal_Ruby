using System.Collections.Generic;
using Code.Infrastructure.View;
using UnityEngine;

namespace Code.Gameplay.Features.Items.Configs
{
    [CreateAssetMenu(menuName = "Inventory", fileName = "inventoryConfig")]
    public class InventoryConfig: ScriptableObject
    {
        public InventoryID inventoryID;
        public List<ItemID> startWorldItems;
        public List<ItemID> startPlayerItems;
    }
}
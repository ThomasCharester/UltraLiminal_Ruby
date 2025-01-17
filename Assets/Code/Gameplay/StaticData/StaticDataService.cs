using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.Items;
using Code.Gameplay.Features.Items.Configs;
using UnityEngine;

namespace Code.Gameplay.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<ItemID, ItemConfig> _itemsbyID;

        public void LoadAll()
        {
            LoadItems();
        }

        public ItemConfig GetItemConfig(ItemID itemID)
        {
            return _itemsbyID.TryGetValue(itemID, out var value) ? value : null;
        } 
        public void LoadItems()
        {
            _itemsbyID = Resources
                .LoadAll<ItemConfig>("Configs/Items")
                .ToDictionary(x => x.itemID, x => x);
        }
    }
}
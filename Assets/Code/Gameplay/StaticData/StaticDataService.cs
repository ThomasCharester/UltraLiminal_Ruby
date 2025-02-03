using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.Items;
using Code.Gameplay.Features.Items.Configs;
using Code.Gameplay.Features.NPC;
using Code.Gameplay.Features.NPC.Configs;
using Code.Gameplay.Features.Player.Configs;
using UnityEngine;

namespace Code.Gameplay.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private Dictionary<ItemID, ItemConfig> _itemsbyID;
        private Dictionary<NPCID, NPCConfig> _NPCsbyID;
        private PlayerConfig _playerConfig;
        public PlayerConfig PlayerConfig => _playerConfig;
        public void LoadAll()
        {
            LoadPlayer();
            LoadItems();
            LoadNPCs();
        }

        public ItemConfig GetItemConfig(ItemID itemID)
        {
            return _itemsbyID.TryGetValue(itemID, out var value) ? value : null;
        } 
        public NPCConfig GetNPCConfig(NPCID npcID)
        {
            return _NPCsbyID.TryGetValue(npcID, out var value) ? value : null;
        } 
        public void LoadItems()
        {
            _itemsbyID = Resources
                .LoadAll<ItemConfig>("Configs/Items")
                .ToDictionary(x => x.itemID, x => x);
        }
        public void LoadNPCs()
        {
            _NPCsbyID = Resources
                .LoadAll<NPCConfig>("Configs/NPCs")
                .ToDictionary(x => x.npcId, x => x);
        }

        public void LoadPlayer()
        {
            _playerConfig = Resources.Load<PlayerConfig>("Configs/Player/Player");
        }
    }
}
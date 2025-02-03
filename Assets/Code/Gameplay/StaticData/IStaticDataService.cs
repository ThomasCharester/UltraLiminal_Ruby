using Code.Gameplay.Features.Items;
using Code.Gameplay.Features.Items.Configs;
using Code.Gameplay.Features.NPC;
using Code.Gameplay.Features.NPC.Configs;
using Code.Gameplay.Features.Player.Configs;

namespace Code.Gameplay.StaticData
{
    public interface IStaticDataService
    {
        void LoadAll();
        ItemConfig GetItemConfig(ItemID itemID);
        NPCConfig GetNPCConfig(NPCID npcID);
        void LoadItems();
        void LoadNPCs();
        void LoadPlayer();

        public PlayerConfig PlayerConfig { get; }
    }
}
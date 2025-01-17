using Code.Gameplay.Features.Items;
using Code.Gameplay.Features.Items.Configs;

namespace Code.Gameplay.StaticData
{
    public interface IStaticDataService
    {
        void LoadAll();
        ItemConfig GetItemConfig(ItemID itemID);
        void LoadItems();
    }
}
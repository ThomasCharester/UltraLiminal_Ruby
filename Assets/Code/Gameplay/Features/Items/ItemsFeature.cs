using Code.Gameplay.Features.Items.Systems;
using Code.Gameplay.Features.Items.Systems.MiniGames;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Items
{
    public class ItemsFeature : Feature
    {
        public ItemsFeature(ISystemFactory systems)
        {
            Add(systems.Create<InitializeWorldInventorySystem>());
            //Add(systems.Create<FillWorldInventoryItemsSystem>());
            Add(systems.Create<ProcessWorldInventoryItemsSystem>());
            
            Add(systems.Create<DebugKeyMiniGameSystem>());
            Add(systems.Create<DebugDoorMiniGameSystem>());
        }
    }
}
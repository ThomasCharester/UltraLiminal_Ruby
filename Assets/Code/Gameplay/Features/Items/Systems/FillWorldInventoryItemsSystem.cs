using Code.Gameplay.Features.Items.Factories;
using Entitas;

namespace Code.Gameplay.Features.Items.Systems
{
    public class FillWorldInventoryItemsSystem : IInitializeSystem
    {
        private readonly IGroup<GameEntity> _worldInventories;

        public FillWorldInventoryItemsSystem(GameContext game)
        {
            _worldInventories = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Inventory,
                GameMatcher.WorldItemList
            ));
        }

        public void Initialize()
        {
            foreach (var inventory in _worldInventories)
            {
                inventory.WorldItemList.Add(ItemID.DebugKey);
            }
        }
    }
}
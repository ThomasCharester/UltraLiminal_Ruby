using Code.Gameplay.Features.Items.Factories;
using Entitas;

namespace Code.Gameplay.Features.Items.Systems
{
    public class InitializeWorldInventorySystem : IInitializeSystem
    {
        private readonly IInventoryFactory _inventoryFactory;
        private readonly IGroup<GameEntity> _worldInventories;

        public InitializeWorldInventorySystem(IInventoryFactory inventoryFactory)
        {
            _inventoryFactory = inventoryFactory;
        }

        public void Initialize()
        {
            _inventoryFactory.CreateWorldInventory();
            
        }
    }
}
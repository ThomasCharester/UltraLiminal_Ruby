using Code.Gameplay.Features.Items.Factories;
using Code.Gameplay.Level;
using Entitas;

namespace Code.Gameplay.Features.Player.Systems
{
    public class InitializePlayerInventorySystem : IInitializeSystem
    {
        private readonly IInventoryFactory _inventoryFactory;

        public InitializePlayerInventorySystem(IInventoryFactory inventoryFactory)
        {
            _inventoryFactory = inventoryFactory;
        }

        public void Initialize()
        {
            _inventoryFactory.CreatePlayerInventory();
        }
    }
}
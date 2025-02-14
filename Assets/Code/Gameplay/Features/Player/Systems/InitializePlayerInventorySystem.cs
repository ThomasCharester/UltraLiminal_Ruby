using Code.Gameplay.Features.Items.Factories;
using Code.Gameplay.Level;
using Entitas;

namespace Code.Gameplay.Features.Player.Systems
{
    public class InitializePlayerInventorySystem : IInitializeSystem
    {
        private readonly IInventoryFactory _inventoryFactory;
        private readonly ILevelDataProvider _levelDataProvider;

        public InitializePlayerInventorySystem(IInventoryFactory inventoryFactory, ILevelDataProvider levelDataProvider)
        {
            _inventoryFactory = inventoryFactory;
            _levelDataProvider = levelDataProvider;
        }

        public void Initialize()
        {
            _inventoryFactory.CreatePlayerInventory(_levelDataProvider.LevelInventoryID);
        }
    }
}
using Code.Gameplay.Features.Items.Factories;
using Code.Gameplay.Level;
using Entitas;

namespace Code.Gameplay.Features.Items.Systems
{
    public class InitializeWorldInventorySystem : IInitializeSystem
    {
        private readonly IInventoryFactory _inventoryFactory;
        private readonly ILevelDataProvider _levelDataProvider;

        public InitializeWorldInventorySystem(IInventoryFactory inventoryFactory, ILevelDataProvider levelDataProvider)
        {
            _inventoryFactory = inventoryFactory;
            _levelDataProvider = levelDataProvider;
        }

        public void Initialize()
        {
            _inventoryFactory.CreateWorldInventory(_levelDataProvider.LevelInventoryID);
            
        }
    }
}
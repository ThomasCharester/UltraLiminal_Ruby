namespace Code.Gameplay.Features.Items.Factories
{
    public interface IInventoryFactory
    {
        GameEntity CreateWorldInventory(InventoryID inventoryID);
        GameEntity CreatePlayerInventory(InventoryID inventoryID);
    }
}
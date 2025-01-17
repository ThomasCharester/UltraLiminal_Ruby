namespace Code.Gameplay.Features.Items.Factories
{
    public interface IInventoryFactory
    {
        GameEntity CreateWorldInventory();
        GameEntity CreatePlayerInventory();
    }
}
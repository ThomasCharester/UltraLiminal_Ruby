using UnityEngine;

namespace Code.Gameplay.Features.Items.Factories
{
    public interface IItemFactory
    {
        GameEntity CreateItem(ItemID itemID, Vector3 spawnPoint);
    }
}
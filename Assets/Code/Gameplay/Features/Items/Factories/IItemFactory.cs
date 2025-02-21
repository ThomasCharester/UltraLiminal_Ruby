using UnityEngine;

namespace Code.Gameplay.Features.Items.Factories
{
    public interface IItemFactory
    {
        GameEntity CreateItem(ItemID itemID, in Vector3 spawnPoint);
        
    }
}
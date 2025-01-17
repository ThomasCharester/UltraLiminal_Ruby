using System;
using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Features.Items.Factories;
using Entitas;
using UnityEngine;

namespace Code.Gameplay.Features.Items.Systems
{
    public class ProcessWorldInventoryItemsSystem : IExecuteSystem
    {
        private readonly IItemFactory _itemFactory;
        private readonly IGroup<GameEntity> _worldInventories;
        private readonly IGroup<GameEntity> _items;
        private List<GameEntity> _buffer = new(32);

        public ProcessWorldInventoryItemsSystem(GameContext game, IItemFactory itemFactory)
        {
            _itemFactory = itemFactory;
            _worldInventories = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Inventory,
                GameMatcher.WorldItemList
            ));
            _items = game.GetGroup(GameMatcher.AllOf(
                GameMatcher.Item,
                GameMatcher.ItemID
            ));
        }

        public void Execute()
        {
            foreach (var worldInventory in _worldInventories.GetEntities())
            {
                _items.GetEntities(_buffer);
                
                List<GameEntity> itemsToDestroy = _buffer;
                if (_buffer.Count > 0) 
                    itemsToDestroy = _buffer
                    .Where(c => !worldInventory.WorldItemList.Contains(c.ItemID)).ToList();
                
                var itemsToSpawn = worldInventory.WorldItemList
                    .Where(c => !_buffer
                    .Select(fc => fc.ItemID)
                    .Contains(c));
                
                foreach (var item in itemsToDestroy)
                {
                    item.isDestructed = true;
                }

                foreach (var item in itemsToSpawn)
                {
                    _itemFactory.CreateItem(item, Vector3.zero);
                }
            }
        }
    }
}
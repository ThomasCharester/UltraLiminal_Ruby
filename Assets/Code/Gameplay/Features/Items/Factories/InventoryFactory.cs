using System.Collections.Generic;
using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.StaticData;
using Code.Infrastructure.Indentifiers;
using UnityEngine;

namespace Code.Gameplay.Features.Items.Factories
{
    public class InventoryFactory : IInventoryFactory
    {
        private readonly IIdentifierService _identifierService;
        private readonly IStaticDataService _staticDataService;

        public InventoryFactory(IIdentifierService identifierService, IStaticDataService staticDataService)
        {
            _identifierService = identifierService;
            _staticDataService = staticDataService;
        }

        public GameEntity CreateWorldInventory(InventoryID inventoryID)
        {
            return CreateEntity.Empty()
                .AddId(_identifierService.NextId())
                .With(x => x.isInventory = true)
                .AddWorldItemList(new List<ItemID>(_staticDataService.GetInventoryConfig(inventoryID).startWorldItems))
                .With(x => x.isActiveOnScene = true);;
        }
        public GameEntity CreatePlayerInventory(InventoryID inventoryID)
        {
            return CreateEntity.Empty()
                .AddId(_identifierService.NextId())
                .With(x => x.isInventory = true)
                .AddPlayerItemList(new List<ItemID>(_staticDataService.GetInventoryConfig(inventoryID).startPlayerItems))
                .With(x => x.isActiveOnScene = true);;
        }
    }
}
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

        public InventoryFactory(IIdentifierService identifierService)
        {
            _identifierService = identifierService;
        }

        public GameEntity CreateWorldInventory()
        {
            return CreateEntity.Empty()
                .AddId(_identifierService.NextId())
                .With(x => x.isInventory = true)
                .AddWorldItemList(new(0));
        }
        public GameEntity CreatePlayerInventory()
        {
            return CreateEntity.Empty()
                .AddId(_identifierService.NextId())
                .With(x => x.isInventory = true)
                .AddPlayerItemList(new(0));
        }
    }
}
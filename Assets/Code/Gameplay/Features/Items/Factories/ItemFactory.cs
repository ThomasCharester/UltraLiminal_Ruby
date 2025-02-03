using Code.Common.Entity;
using Code.Common.Extensions;
using Code.Gameplay.StaticData;
using Code.Infrastructure.Indentifiers;
using UnityEngine;

namespace Code.Gameplay.Features.Items.Factories
{
    public class ItemFactory : IItemFactory
    {
        private readonly IIdentifierService _identifierService;
        private readonly IStaticDataService _staticDataService;

        public ItemFactory(IIdentifierService identifierService, IStaticDataService staticDataService)
        {
            _identifierService = identifierService;
            _staticDataService = staticDataService;
        }

        public GameEntity CreateItem(ItemID itemID, Vector3 spawnPoint)
        {
            GameEntity item = CreateEntity.Empty();
            if (spawnPoint != Vector3.zero)
                item.AddItemID(itemID)
                    .AddId(_identifierService.NextId())
                    .AddViewPrefab(_staticDataService.GetItemConfig(itemID).itemPrefab)
                    .AddVectorSpawnPoint(spawnPoint)
                    .With(x => x.isItem = true);
            else
                item.AddItemID(itemID)
                    .AddId(_identifierService.NextId())
                    .AddViewPrefab(_staticDataService.GetItemConfig(itemID).itemPrefab)
                    .AddTransformSpawnPoint(_staticDataService.GetItemConfig(itemID).transform)
                    .With(x => x.isItem = true);

            AssignMiniGameActivator(item, itemID);
            return item;
        }

        private GameEntity AssignMiniGameActivator(GameEntity item, ItemID itemID)
        {
            switch (itemID)
            {
                case ItemID.None:
                    item.With(x => x.isUseless = true);
                    break;
                case ItemID.DebugKey:
                    item.With(x => x.isDebugKeyMiniGameActivator = true);
                    break;
                case ItemID.DebugDoor:
                    item.With(x => x.isDebugDoorMiniGameActivator = true);
                    break;
            }

            return item;
        }
    }
}
using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Features.Items.Systems.MiniGames
{
    public class DebugKeyMiniGameSystem : ReactiveSystem<GameEntity>
    {
        private readonly IGroup<GameEntity> _playerInventories;
        private readonly IGroup<GameEntity> _worldInventories;

        public DebugKeyMiniGameSystem(Contexts contexts) : base(contexts.game)
        {
            _playerInventories = contexts.game.GetGroup(GameMatcher.PlayerItemList);
            _worldInventories = contexts.game.GetGroup(GameMatcher.WorldItemList);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Triggered));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isTriggered && entity.isDebugKeyMiniGameActivator;
        }

        protected override void Execute(List<GameEntity> items)
        {
            foreach (var worldInventory in _worldInventories)
            foreach (var playerInventory in _playerInventories)
            foreach (var item in items)
            {
                worldInventory.WorldItemList.Remove(item.ItemID);
                playerInventory.PlayerItemList.Add(item.ItemID);
            }
        }
    }
}
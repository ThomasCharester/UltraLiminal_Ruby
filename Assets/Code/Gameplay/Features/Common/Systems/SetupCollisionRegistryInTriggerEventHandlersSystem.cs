using System.Collections.Generic;
using Code.Gameplay.Common.Collisions;
using Entitas;
using Zenject;

namespace Code.Gameplay.Features.Common.Systems
{
    public class SetupCollisionRegistryInTriggerEventHandlersSystem: ReactiveSystem<GameEntity>
    {
        private readonly ICollisionRegistry _collisionRegistry;

        [Inject]
        public SetupCollisionRegistryInTriggerEventHandlersSystem(Contexts contexts,ICollisionRegistry collisionRegistry) : base(contexts.game)
        {
            _collisionRegistry = collisionRegistry;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AllOf(GameMatcher.TriggerEventService));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasTriggerEventService;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.TriggerEventService.SetCollisionRegistry(_collisionRegistry);
            }
        }
    }
}
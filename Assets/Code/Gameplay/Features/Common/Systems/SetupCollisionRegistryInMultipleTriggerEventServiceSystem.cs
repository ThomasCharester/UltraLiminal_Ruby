using System.Collections.Generic;
using Code.Gameplay.Common.Collisions;
using Entitas;
using Zenject;

namespace Code.Gameplay.Features.Common.Systems
{
    public class SetupCollisionRegistryInMultipleTriggerEventServiceSystem: ReactiveSystem<GameEntity>
    {
        private readonly ICollisionRegistry _collisionRegistry;

        [Inject]
        public SetupCollisionRegistryInMultipleTriggerEventServiceSystem(Contexts contexts,ICollisionRegistry collisionRegistry) : base(contexts.game)
        {
            _collisionRegistry = collisionRegistry;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AllOf(GameMatcher.MultipleTriggerEventService));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasMultipleTriggerEventService;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.MultipleTriggerEventService.SetCollisionRegistry(_collisionRegistry);
            }
        }
    }
}
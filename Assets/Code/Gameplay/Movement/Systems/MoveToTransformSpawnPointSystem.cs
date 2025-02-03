using System.Collections.Generic;
using Code.Gameplay.Common;
using Entitas;

namespace Code.Gameplay.Movement.Systems
{
    public class MoveToTransformSpawnPointSystem : ReactiveSystem<GameEntity>
    {
        public MoveToTransformSpawnPointSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Transform));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasTransformSpawnPoint && entity.hasTransform;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.hasCharacterController)
                    entity.CharacterController.SetPosition(entity.Transform.position); // to do CC transform setup

                else
                    entity.Transform.SetPositionAndRotation(entity.TransformSpawnPoint.position,
                        entity.TransformSpawnPoint.rotation);

                entity.RemoveTransformSpawnPoint();
            }
        }
    }
}
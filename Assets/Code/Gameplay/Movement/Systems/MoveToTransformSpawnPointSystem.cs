using System.Collections.Generic;
using Code.Gameplay.Common;
using Entitas;

namespace Code.Gameplay.Movement.Systems
{
    public class MoveToTransformSpawnPointSystem : ReactiveSystem<GameEntity>
    {
        public MoveToTransformSpawnPointSystem(Contexts contexts) : base(contexts.game)
        { }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AllOf(GameMatcher.VectorSpawnPoint, GameMatcher.RotationSpawnPoint));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasVectorSpawnPoint && entity.hasRotationSpawnPoint && entity.hasTransform;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.hasCharacterController)
                    entity.CharacterController.SetPositionAndRotation(entity.VectorSpawnPoint, entity.RotationSpawnPoint);

                else
                    entity.Transform.SetPositionAndRotation(entity.VectorSpawnPoint, entity.RotationSpawnPoint);

                entity.RemoveVectorSpawnPoint();
                entity.RemoveRotationSpawnPoint();
            }
        }
    }
}
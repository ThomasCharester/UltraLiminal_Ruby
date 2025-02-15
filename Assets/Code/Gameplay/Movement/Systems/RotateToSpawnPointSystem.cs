using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Movement.Systems
{
    public class RotateToSpawnPointSystem: ReactiveSystem<GameEntity>
    {
        public RotateToSpawnPointSystem(Contexts contexts) : base(contexts.game)
        { }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AllOf(GameMatcher.RotationSpawnPoint));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasRotationSpawnPoint;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.hasCharacterController)
                    entity.CharacterController.SetRotation(entity.RotationSpawnPoint);
                else if(entity.hasTransform) 
                    entity.Transform.rotation = entity.RotationSpawnPoint;
                entity.RemoveRotationSpawnPoint();
            }
        }
    }
}
using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Movement.Systems
{
    public class MoveToVectorSpawnPointSystem : ReactiveSystem<GameEntity>
    {
        public MoveToVectorSpawnPointSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AllOf(GameMatcher.VectorSpawnPoint));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasVectorSpawnPoint;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.hasCharacterController)
                    entity.CharacterController.SetPosition(entity.VectorSpawnPoint);
                else if(entity.hasTransform) 
                    entity.Transform.position = entity.VectorSpawnPoint;
                entity.RemoveVectorSpawnPoint();
            }
        }
    }
}
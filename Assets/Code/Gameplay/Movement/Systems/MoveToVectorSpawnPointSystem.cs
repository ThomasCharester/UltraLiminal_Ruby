using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.Movement.Systems
{
    public class MoveToVectorSpawnPointSystem : ReactiveSystem<GameEntity>
    {
        public MoveToVectorSpawnPointSystem(Contexts contexts) : base(contexts.game)
        { }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AllOf(GameMatcher.CharacterController));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasVectorSpawnPoint && entity.hasCharacterController;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.CharacterController.SetPosition(entity.VectorSpawnPoint);
                entity.RemoveVectorSpawnPoint();
            }
        }
    }
}
using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.StateMachine.Systems
{
    public class InitializeAndStartDoorInteractionStateMachineInRunTimeSystem: ReactiveSystem<GameEntity>
    {
        public InitializeAndStartDoorInteractionStateMachineInRunTimeSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AllOf(GameMatcher.DoorInteractionStateManager));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasDoorInteractionStateManager;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.DoorInteractionStateManager.InitializeStateMachine();
                entity.DoorInteractionStateManager.StartStateMachine();
            }
        }
    }
}
using System.Collections.Generic;
using Entitas;

namespace Code.Gameplay.StateMachine.Systems
{
    public class InitializeAndStartEnvironmentInteractionStateMachineInRunTimeSystem: ReactiveSystem<GameEntity>
    {
        public InitializeAndStartEnvironmentInteractionStateMachineInRunTimeSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AllOf(GameMatcher.EnvironmentInteractionStateManager));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasEnvironmentInteractionStateManager;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.EnvironmentInteractionStateManager.InitializeStateMachine();
                entity.EnvironmentInteractionStateManager.StartStateMachine();
            }
        }
    }
}
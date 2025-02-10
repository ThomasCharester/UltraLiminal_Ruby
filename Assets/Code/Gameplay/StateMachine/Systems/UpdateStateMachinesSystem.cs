using Entitas;

namespace Code.Gameplay.StateMachine.Systems
{
    public class UpdateStateMachinesSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _stateMachinesLists;

        public UpdateStateMachinesSystem(GameContext game)
        {
            _stateMachinesLists = game.GetGroup(GameMatcher.StateManagers);
        }

        public void Execute()
        {
            foreach (var stateMachines in _stateMachinesLists)
            {
                foreach (var stateMachine in stateMachines.StateManagers)
                {
                    stateMachine.Tick();
                }
            }
        }
    }
}
using Code.Gameplay.Movement.Systems;
using Code.Gameplay.StateMachine.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.StateMachine
{
    public class StateMachineFeature : Feature
    {
        public StateMachineFeature(ISystemFactory systems)
        {
            Add(systems.Create<InitializeAndStartEnvironmentInteractionStateMachineInRunTimeSystem>());
            
            Add(systems.Create<UpdateStateMachinesSystem>());
        }
    }
}
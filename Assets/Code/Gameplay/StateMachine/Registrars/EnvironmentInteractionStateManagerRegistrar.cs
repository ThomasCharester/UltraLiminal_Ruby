using System.Collections.Generic;
using Code.Gameplay.Features.EnvironmentInteractionFeature.StateMachine;
using Code.Infrastructure.View.Registrar;

namespace Code.Gameplay.StateMachine.Registrars
{
    public class EnvironmentInteractionStateManagerRegistrar : EntityComponentRegistrar
    {
        public EnvironmentInteractionStateMachine EnvironmentInteractionStateMachine;

        public override void RegisterComponents() //
        {
            if (!Entity.hasStateManagers)
                Entity.AddStateManagers(new List<IStateManager>());
            
            Entity.AddEnvironmentInteractionStateManager(EnvironmentInteractionStateMachine);
            Entity.StateManagers.Add(EnvironmentInteractionStateMachine);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasEnvironmentInteractionStateManager)
                Entity.RemoveEnvironmentInteractionStateManager();
            if (Entity.hasStateManagers) //
                Entity.RemoveStateManagers();
        }
    }
}
using System.Collections.Generic;
using Code.Gameplay.Features.EnvironmentInteractionFeature.StateMachine;
using Code.Gameplay.StateMachine;
using Code.Infrastructure.View.Registrar;
using UnityEngine.Serialization;

namespace Code.Gameplay.Features.DoorInteractionFeature.Registrars
{
    public class DoorInteractionStateMachineRegistrar: EntityComponentRegistrar
    {
        public DoorInteractionStateMachine DoorInteractionStateMachine;

        public override void RegisterComponents() //
        {
            if (!Entity.hasStateManagers)
                Entity.AddStateManagers(new List<IStateManager>());
            
            Entity.AddDoorInteractionStateManager(DoorInteractionStateMachine);
            Entity.StateManagers.Add(DoorInteractionStateMachine);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasDoorInteractionStateManager)
                Entity.RemoveDoorInteractionStateManager();
            if (Entity.hasStateManagers) //
                Entity.RemoveStateManagers();
        }
    }
}
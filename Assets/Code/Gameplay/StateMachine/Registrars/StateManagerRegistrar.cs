using System.Collections.Generic;
using Code.Infrastructure.View.Registrar;

namespace Code.Gameplay.StateMachine.Registrars
{
    public class StateManagerRegistrar : EntityComponentRegistrar
    {
        public IStateManager StateManager;

        public override void RegisterComponents()
        {
            if (!Entity.hasStateManagers)
                Entity.AddStateManagers(new List<IStateManager>());
            Entity.StateManagers.Add(StateManager);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasStateManagers)
                Entity.RemoveStateManagers();
        }
    }
}
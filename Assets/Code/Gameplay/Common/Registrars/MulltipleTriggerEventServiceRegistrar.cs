using Code.Gameplay.Common.Physics;
using Code.Infrastructure.View.Registrar;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Gameplay.Common.Registrars
{
    public class MulltipleTriggerEventServiceRegistrar: EntityComponentRegistrar
    {
        [SerializeField] private MultipleTriggerEventService _multipleTriggerEventService;
        public override void RegisterComponents()
        {
            Entity.AddMultipleTriggerEventService(_multipleTriggerEventService);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasMultipleTriggerEventService)
                Entity.RemoveMultipleTriggerEventService();
        }
    }
}
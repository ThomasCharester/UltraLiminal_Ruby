using Code.Gameplay.Common.Physics;
using Code.Infrastructure.View.Registrar;
using UnityEngine;

namespace Code.Gameplay.Common.Registrars
{
    public class TriggerEventServiceRegistrar : EntityComponentRegistrar
    {
        [SerializeField] private TriggerEventService _triggerEventService;
        public override void RegisterComponents()
        {
            Entity.AddTriggerEventService(_triggerEventService);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasTriggerEventService)
                Entity.RemoveTriggerEventService();
        }
    }
}
using Code.Gameplay.Common.Physics;
using Code.Infrastructure.View.Registrar;
using UnityEngine;

namespace Code.Gameplay.Common.Registrars
{
    public class ColliderEventServiceRegistrar : EntityComponentRegistrar
    {
        [SerializeField] private IColliderEventService _colliderEventService;
        public override void RegisterComponents()
        {
            Entity.AddColliderEventService(_colliderEventService);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasColliderEventService)
                Entity.RemoveColliderEventService();
        }
    }
}
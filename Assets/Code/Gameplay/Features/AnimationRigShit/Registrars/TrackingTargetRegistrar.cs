using Code.Infrastructure.View.Registrar;
using UnityEngine;

namespace Code.Gameplay.Features.AnimationRigShit.Registrars
{
    public class TrackingTargetRegistrar : EntityComponentRegistrar
    {
        [SerializeField] private Transform trackingTarget;
        public override void RegisterComponents()
        {
            Entity.AddTrackingTarget(trackingTarget);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasTrackingTarget)
                Entity.RemoveTrackingTarget();
        }
    }
}
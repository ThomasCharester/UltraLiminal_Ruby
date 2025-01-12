using Code.Gameplay.Features.Player.Animator;
using Code.Infrastructure.View.Registrar;
using UnityEngine;

namespace Code.Gameplay.Features.Player.Registrars
{
    public class TrackingTargetRegistrar : EntityComponentRegistrar
    {
        public Transform target;
        
        public override void RegisterComponents()
        {
            Entity.AddCameraTrackingTarget(target);
        }
        public override void UnregisterComponents()
        {
            Entity.RemoveCameraTrackingTarget();
        }
    }
}
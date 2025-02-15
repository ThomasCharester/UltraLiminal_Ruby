using Code.Gameplay.Features.Player.Animator;
using Code.Infrastructure.View.Registrar;
using UnityEngine;

namespace Code.Gameplay.Features.LocationFeature.Registrars
{
    public class HingeJointRegistrar : EntityComponentRegistrar
    {
        public HingeJoint joint;

        public override void RegisterComponents()
        {
            Entity.AddHingeJoint(joint);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasHingeJoint)
                Entity.RemoveHingeJoint();
        }
    }
}
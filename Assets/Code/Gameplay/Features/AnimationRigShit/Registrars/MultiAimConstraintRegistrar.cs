using Code.Infrastructure.View.Registrar;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Code.Gameplay.Features.AnimationRigShit.Registrars
{
    public class MultiAimConstraintRegistrar: EntityComponentRegistrar
    {
        [SerializeField] private MultiAimConstraint multiAimConstraint; 
        public override void RegisterComponents()
        {
            Entity.AddMultiAimConstraint(multiAimConstraint);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasMultiAimConstraint)
                Entity.RemoveMultiAimConstraint();
        }
    }
}
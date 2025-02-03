using Code.Infrastructure.View.Registrar;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Code.Gameplay.Features.AnimationRigShit.Registrars
{
    public class RigBuilderRegistrar: EntityComponentRegistrar
    {
        [SerializeField] private RigBuilder rigBuilder; 
        public override void RegisterComponents()
        {
            Entity.AddRigBuilder(rigBuilder);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasRigBuilder)
                Entity.RemoveRigBuilder();
        }
    }
}
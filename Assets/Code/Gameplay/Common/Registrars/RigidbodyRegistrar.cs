using Code.Gameplay.Movement.Controller;
using Code.Infrastructure.View.Registrar;
using UnityEngine;

namespace Code.Gameplay.Common.Registrars
{
    public class RigidbodyRegistrar : EntityComponentRegistrar
    {
        public Rigidbody myfuckingrigidbody;

        public override void RegisterComponents()
        {
            Entity.AddRigidbody(myfuckingrigidbody);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasRigidbody)
                Entity.RemoveRigidbody();
        }
    }
}
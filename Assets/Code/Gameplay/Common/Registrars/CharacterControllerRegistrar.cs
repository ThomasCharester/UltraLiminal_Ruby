using Code.Gameplay.Movement.Controller;
using Code.Infrastructure.View.Registrar;

namespace Code.Gameplay.Common.Registrars
{
    public class CharacterControllerRegistrar: EntityComponentRegistrar
    {
        public StandaloneCharacterController controller;
        public override void RegisterComponents()
        {
            Entity.AddCharacterController(controller);
        }

        public override void UnregisterComponents()
        { 
            Entity.RemoveCharacterController();
        }
    }
}
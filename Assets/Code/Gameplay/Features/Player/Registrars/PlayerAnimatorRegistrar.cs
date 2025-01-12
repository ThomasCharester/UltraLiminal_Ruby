using Code.Gameplay.Features.Player.Animator;
using Code.Infrastructure.View.Registrar;

namespace Code.Gameplay.Features.Player.Registrars
{
    public class PlayerAnimatorRegistrar : EntityComponentRegistrar
    {
        public PlayerAnimator playerAnimator;
        public override void RegisterComponents()
        {
            Entity.AddPlayerAnimator(playerAnimator);
        }
        public override void UnregisterComponents()
        {
            Entity.RemovePlayerAnimator();
        }
    }
}
using Code.Infrastructure.View.Registrar;

namespace Code.Gameplay.Features.LocationFeature.Registrars
{
    public class StairwellRegistrar : EntityComponentRegistrar
    {
        public override void RegisterComponents()
        {
            Entity.isStairwell = true;
        }

        public override void UnregisterComponents()
        {
            if (Entity.isStairwell)
                Entity.isStairwell = false;
        }
    }
}
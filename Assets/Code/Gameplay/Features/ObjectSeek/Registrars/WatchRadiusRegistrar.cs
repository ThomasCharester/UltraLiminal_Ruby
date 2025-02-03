using Code.Infrastructure.View.Registrar;
using UnityEngine;

namespace Code.Gameplay.Features.ObjectSeek.Registrars
{
    public class WatchRadiusRegistrar: EntityComponentRegistrar
    {
        [SerializeField] private float radius;
        public override void RegisterComponents()
        {
            Entity.AddWatchRadius(radius);
        }

        public override void UnregisterComponents()
        {
            if (Entity.hasWatchRadius)
                Entity.RemoveWatchRadius();
        }
    }
}
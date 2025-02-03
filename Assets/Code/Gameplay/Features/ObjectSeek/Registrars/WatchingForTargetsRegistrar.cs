using Code.Infrastructure.View.Registrar;
using UnityEngine;

namespace Code.Gameplay.Features.ObjectSeek.Registrars
{
    public class WatchingForTargetsRegistrar: EntityComponentRegistrar
    {
        public override void RegisterComponents()
        {
            Entity.isWatchingForTargets = true;
        }

        public override void UnregisterComponents()
        {
            Entity.isWatchingForTargets = false;
        }
    }
}
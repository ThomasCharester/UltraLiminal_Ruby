using Code.Gameplay.Features.Common.Systems;
using Code.Gameplay.Features.LocationFeature.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.Common
{
    public class CommonFeature: Feature
    {
        public CommonFeature(ISystemFactory systems)
        {
            Add(systems.Create<SetupCollisionRegistryInTriggerEventHandlersSystem>());
        }
    }
}
using Code.Gameplay.Features.ObjectSeek.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.ObjectSeek
{
    public class ObjectSeekFeature : Feature
    {
        public ObjectSeekFeature(ISystemFactory systems)
        {
            Add(systems.Create<WatchForTargetsSystem>());
        }
    }
}
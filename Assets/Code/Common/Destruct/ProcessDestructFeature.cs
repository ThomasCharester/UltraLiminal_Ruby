using Code.Common.Destruct.Systems;
using Code.Infrastructure.Systems;

namespace Code.Common.Destruct
{
    public class ProcessDestructFeature : Feature
    {
        public ProcessDestructFeature(ISystemFactory systems)
        {
            Add(systems.Create<SelfDestructTimerSystem>());
            
            Add(systems.Create<CleanupDestructedViewSystem>());
            Add(systems.Create<CleanupDestructedSystem>());
        }
    }
}
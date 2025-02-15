using Code.Gameplay.Features.NPC.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.NPC
{
    public class NPCFeature: Feature
    {
        public NPCFeature(ISystemFactory systems)
        {
            //Add(systems.Create<SpawnTestNPCSystem>());
        }
    }
}
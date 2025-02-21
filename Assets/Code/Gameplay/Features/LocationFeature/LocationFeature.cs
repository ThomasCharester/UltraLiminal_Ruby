using Code.Gameplay.Features.LocationFeature.Systems;
using Code.Infrastructure.Systems;

namespace Code.Gameplay.Features.LocationFeature
{
    public class LocationFeature : Feature
    {
        public LocationFeature(ISystemFactory systems)
        {
            Add(systems.Create<InitializeLocationSystem>());
            Add(systems.Create<TurnOnDoorSystem>());
            Add(systems.Create<CheckNeedToSpawnLocationSegmentSystem>());
            Add(systems.Create<RandomSpawnLocationSegmentSystem>());
            
            Add(systems.Create<SpawnLowerStairwellSectionSystem>());
            Add(systems.Create<SpawnUpperStairwellSectionSystem>());
            
            Add(systems.Create<SetupDoorsAfterSpawnSystem>());
            
            Add(systems.Create<DeleteStairwellSegmentsOnExitSystem>());
            Add(systems.Create<DeleteLocationSegmentOnExitSystem>());
            
            Add(systems.Create<CleanUselessDoorsSystem>());
        }
    }
}
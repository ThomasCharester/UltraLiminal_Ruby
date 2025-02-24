using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Common.Pooler;
using Code.Gameplay.Features.LocationFeature.Factories;
using Code.Gameplay.StaticData;
using Entitas;
using ModestTree;
using UnityEngine;

namespace Code.Gameplay.Features.LocationFeature.Systems
{
    public class SpawnLowerStairwellSectionSystem : IExecuteSystem
    {
        private readonly ILocationSegmentPoolerService _locationSegmentPoolerService;
        private readonly IDoorFactory _doorFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly IGroup<GameEntity> _stairwellSections;
        private List<GameEntity> buff = new(8);


        public SpawnLowerStairwellSectionSystem(GameContext game, IDoorFactory doorFactory,
            IStaticDataService staticDataService, ILocationSegmentPoolerService locationSegmentPoolerService)
        {
            _locationSegmentPoolerService = locationSegmentPoolerService;
            _doorFactory = doorFactory;
            _staticDataService = staticDataService;
            _stairwellSections = game.GetGroup(GameMatcher.AllOf(
                    GameMatcher.TriggerEventService,
                    GameMatcher.Stairwell,
                    GameMatcher.Transform)
                .NoneOf(GameMatcher.LowerStairwellID));
        }

        public void Execute()
        {
            foreach (var section in _stairwellSections.GetEntities(buff))
            {
                if (!section.TriggerEventService.StayingEntities.Any(x => x.isPlayer)) continue;

                Vector3 lowerSectionOrigin = section.Transform.position;
                lowerSectionOrigin.y -= _staticDataService.GameplayConstantsConfig._stairwellSectionVerticalOffset;
 
                var lowerSection = _locationSegmentPoolerService.GetPool(LocationSegmentID.Stairwell).Get();
                lowerSection.AddVectorSpawnPoint(lowerSectionOrigin);
                lowerSection.AddRotationSpawnPoint(section.Transform.rotation);
                
                _doorFactory.SpawnDoors(lowerSectionOrigin, section.Transform.rotation, lowerSection.LocationSegment,
                    lowerSection.Id);

                section.AddLowerStairwellID(lowerSection.Id);

                lowerSection.AddUpperStairwellID(section.Id);
 
            }
        }
    }
}
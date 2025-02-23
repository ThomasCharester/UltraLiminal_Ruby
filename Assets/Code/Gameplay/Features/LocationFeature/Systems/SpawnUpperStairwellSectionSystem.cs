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
    public class SpawnUpperStairwellSectionSystem : IExecuteSystem
    {
        private readonly IDoorFactory _doorFactory;
        private readonly IStaticDataService _staticDataService;
        private readonly IGroup<GameEntity> _stairwellSections;
        private List<GameEntity> buff = new(8);
        private readonly ILocationSegmentPoolerService _locationSegmentPoolerService;

        public SpawnUpperStairwellSectionSystem(GameContext game, IDoorFactory doorFactory
            , IStaticDataService staticDataService, ILocationSegmentPoolerService locationSegmentPoolerService)
        {
            _locationSegmentPoolerService = locationSegmentPoolerService;
            _doorFactory = doorFactory;
            _staticDataService = staticDataService;
            _stairwellSections = game.GetGroup(GameMatcher.AllOf(
                    GameMatcher.TriggerEventService,
                    GameMatcher.Stairwell,
                    GameMatcher.Transform)
                .NoneOf(GameMatcher.UpperStairwellID));
        }

        public void Execute()
        {
            foreach (var section in _stairwellSections.GetEntities(buff))
            {
                if (!section.TriggerEventService.StayingEntities.Any(x => x.isPlayer)) continue;

                Vector3 upperSectionOrigin = section.Transform.position;
                upperSectionOrigin.y += _staticDataService.GameplayConstantsConfig._stairwellSectionVerticalOffset;
                
                // var upperSection = _locationSegmentFactory.CreateLocationSegment(LocationSegmentID.Stairwell,
                //     upperSectionOrigin, section.Transform.rotation);
                var upperSection = _locationSegmentPoolerService.GetPool(LocationSegmentID.Stairwell).Get();
                upperSection.AddVectorSpawnPoint(upperSectionOrigin);
                upperSection.AddRotationSpawnPoint(section.Transform.rotation);

                _doorFactory.SpawnDoors(upperSectionOrigin, section.Transform.rotation, upperSection.LocationSegment, upperSection.Id);
                
                section.AddUpperStairwellID(upperSection.Id);

                upperSection.AddLowerStairwellID(section.Id);
                
                // Попробую не чистить, авось сэкономлю на спичках
                // section.TriggerEventService.StayingEntities.Clear(); 
                
                //Debug.Log("Spawned upper segment " + upperSection.Id);
            }
        }
    }
}